using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;



namespace Unity_Dark_Theme_Patcher
{
    public partial class Main : Form
    {
        public CommonOpenFileDialog folderDialog = new CommonOpenFileDialog() { IsFolderPicker = true, Multiselect = false };
        public static string unityFolderPathString;

        public static bool requiresAdministratorRights = false;

        public static UInt32 getSkinIdxOffset = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (IsAdministrator()) 
            {
                this.Text = "[ADMINISTRATOR] Unity Dark Theme Patcher";
            }


            Log.InitializeLog(logBox);
            Log.Info("Looking for installed versions of Unity...");

            string windrive = Path.GetPathRoot(Environment.SystemDirectory); // C:\

            List<string> possibleInstallPaths = new List<string>();

            possibleInstallPaths.Add(Path.Combine(windrive, @"Program Files(x86)\Unity\Editor"));
            possibleInstallPaths.Add(Path.Combine(windrive, @"Program Files\Unity\Editor"));

            foreach (string path in possibleInstallPaths) 
            {
                if (CheckForUnityInstallation(path) == UnityInstallState.Both)
                {
                    unityFolderPath.Items.Add(path);
                }
            }
        }

        private void pickFolderButton_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok) //If a directory was selected
            {
                unityFolderPathString = folderDialog.FileName;
                unityFolderPath.Text = folderDialog.FileName;
                CheckForUnityInstallation(folderDialog.FileName);
            }
        }

        private void unityFolderPath_TextChanged(object sender, EventArgs e)
        {
            CheckForUnityInstallation(unityFolderPath.Text);
        }

        private void applyPatchButton_Click(object sender, EventArgs e)
        {
            if (!requiresAdministratorRights || (requiresAdministratorRights && IsAdministrator()))
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Log.Info("Started searching the PDB file for GetSkinIdx@");
                    SearchForSkinIndexInPDB();
                    Log.Info("Done searching the PDB file for GetSkinIdx@");

                    Log.Info("Started searching the EXE file for GetSkinIdx@");
                    //If the search returned an address
                    if (getSkinIdxOffset != 0) PatchExeFile();
                    else Log.Info("A previous step failed. Restart the program or select another unity installation folder.");
                }
                else
                {
                    Log.Info("Patching canceled by user");
                }
            }
            else
            {
                if (MessageBox.Show("The selected Unity installation is in the system drive. Most likely this means that it requires administrator rights. Restart with administrator rights?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) 
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = Application.ExecutablePath;
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.Verb = "runas";
                    proc.Start();

                    Application.Exit();
                }
            }
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(unityFolderPathString, "Unity.exe.old")) && pdbCheckBox.Checked && exeCheckBox.Checked)
            {
                File.Copy(Path.Combine(unityFolderPathString, "Unity.exe.old"), Path.Combine(unityFolderPathString, "Unity.exe"), true);

                MessageBox.Show("Successfully restored the last version", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Selected folder does not contain a Unity installation or the Unity.exe.old file...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        enum UnityInstallState { None, EXE, PDB, Both }
        private UnityInstallState CheckForUnityInstallation(string path)
        {
            logBox.Items.Clear();

            if (Directory.Exists(path))
            {
                applyPatchButton.Enabled = true;
                restoreButton.Enabled = true;
            }
            else
            {
                applyPatchButton.Enabled = false;
                restoreButton.Enabled = false;
                return UnityInstallState.None;
            }

            if (Path.GetPathRoot(path) == Path.GetPathRoot(Environment.SystemDirectory) && unityFolderPath.Text != "")
            {
                applyPatchButton.UseElevationIcon = true;
                restoreButton.UseElevationIcon = true;
                requiresAdministratorRights = true;
            }
            else 
            {
                applyPatchButton.UseElevationIcon = false;
                restoreButton.UseElevationIcon = false;
                requiresAdministratorRights = false;
            }


            pdbCheckBox.Checked = false; exeCheckBox.Checked = false;
            int installState = 0;

            if (File.Exists(Path.Combine(path, "unity_x64.pdb")))
            {
                pdbCheckBox.Checked = true;
                Log.Info("Found PDB file!");
                installState += 2;

            }
            if (File.Exists(Path.Combine(path, "Unity.exe")))
            {
                exeCheckBox.Checked = true;
                Log.Info("Found EXE file!");
                installState++;

                unityVersionLabel.Text = $"Unity version: {FileVersionInfo.GetVersionInfo(Path.Combine(path, "Unity.exe")).FileVersion}";
            }
            else 
            {
                unityVersionLabel.Text = $"Unity version: null";
            }

            return (UnityInstallState)installState;
        }



        private static void PatchExeFile()
        {

            //Backups the original exe file
            Log.Info("Making a backup of the original EXE file");
            File.Copy(Path.Combine(unityFolderPathString, "Unity.exe"), Path.Combine(unityFolderPathString, "Unity.exe.old"), true);
            Log.Info("Done making a backup of the original EXE file");

            //Opens the file in read-write mode
            using (var exeStream = new FileStream(Path.Combine(unityFolderPathString, "Unity.exe"), FileMode.Open, FileAccess.ReadWrite))
            {
                BoyerMoore b = new BoyerMoore(); //Search

                byte[] searchPattern = Encoding.ASCII.GetBytes(".text");

                b.SetPattern(searchPattern); //Find the .text section

                int searchResult = b.Search(exeStream.ToByteArray());

                Log.Info($"Found .info section at offset {searchResult:X}");

                int textSectionOffset = searchResult + 20; //Add 20 (0x14) to the offset of the .text section

                Log.Info($"Found .info section offset at offset {textSectionOffset:X}");

                exeStream.Position = textSectionOffset; //Set the stream position to the beginning of the .text section so we can get its offset (???)

                byte[] positionBytes = ReadNextNBytes(exeStream, 4); 

                UInt32 offsetFromTextSection = BitConverter.ToUInt32(positionBytes, 0); //Get the UInt32 representation of those bytes

                Log.Info($"Position to look at in EXE is {offsetFromTextSection:X}");

                exeStream.Position = offsetFromTextSection + getSkinIdxOffset; //Go to the beginning of the GetSkinIdx function 

                Log.Info($"GetSkinIdx@ function is at {exeStream.Position:X}");

                byte[] instructions = new byte[] { 0x31, 0xC0, 0xFE, 0xC0, 0xC3 }; //Sets the patch bytes

                Log.Info("Preparing to patch EXE file...");

                for (int i = 0; i < instructions.Length; i++) //Writes the patch bytes to the file
                {
                    exeStream.WriteByte(instructions[i]);
                }

                Log.Info("Done patching EXE file");

                MessageBox.Show("Done patching EXE file", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } //The stream should close by itself
        }

        private static void SearchForSkinIndexInPDB()
        {
            using (var pdbStream = new FileStream(Path.Combine(unityFolderPathString, "unity_x64.pdb"), FileMode.Open, FileAccess.Read))
            {
                BoyerMoore b = new BoyerMoore(); //Search in a stream

                byte[] searchPattern = Encoding.ASCII.GetBytes("GetSkinIdx@"); //What to seach for
                b.SetPattern(searchPattern);

                int searchResult = b.Search(pdbStream.ToByteArray());
                if (searchResult != -1) //If the search found something
                {
                    Log.Info($"Found GetSkinIdx@ at offset {searchResult:X}"); //GetSkinIdx@ is here

                    int codeOffset = searchResult - 7;

                    Log.Info($"Found GetSkinIdx@ code at offset {codeOffset:X}"); //The code associated with that function is here

                    pdbStream.Position = codeOffset; //Set the stream position to the proper offset so we can read bytes

                    byte[] positionBytes = ReadNextNBytes(pdbStream, 4); //Read next 4 bytes from the stream

                    UInt32 offsetFromTextSection = BitConverter.ToUInt32(positionBytes, 0); //Get the UInt32 representation of the 4 bytes

                    Log.Info($"Position to look at in EXE is {offsetFromTextSection:X}");
                    getSkinIdxOffset = offsetFromTextSection; //Save the calculated data for use in patching the Unity executable
                }
                else
                {
                    Log.Info("PDB file does not contain a definition for GetSkinIdx@"); //This should not happen but it;s here just in case...
                }



                pdbStream.Dispose();
                pdbStream.Close();
                GC.Collect();
            }
        }

        private static byte[] ReadNextNBytes(FileStream pdbStream, int n)
        {
            byte[] positionBytes = new byte[n];
            for (int i = 0; i < positionBytes.Length; i++)
            {
                positionBytes[i] = (byte)pdbStream.ReadByte();
            }

            return positionBytes;
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    public static class Extensions
    {
        public static byte[] ToByteArray(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static bool ContainsString(this Stream vStream, string search)
        {
            byte[] streamBytes = new byte[vStream.Length];

            int pos = 0;
            int len = (int)vStream.Length;
            while (pos < len)
            {
                int n = vStream.Read(streamBytes, pos, len - pos);
                pos += n;
            }

            //byte[] streamBytes = vStream.ToByteArray();

            string stringOfStream = Encoding.UTF32.GetString(streamBytes);
            if (stringOfStream.Contains(search))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
