using System.Windows.Forms;

namespace Unity_Dark_Theme_Patcher
{
    class Log
    {
        private static ListBox debugBox;
        public static void InitializeLog(ListBox debugBox)
        {
            Log.debugBox = debugBox;
        }

        public static void Info(object message)
        {
            debugBox.Items.Add(message);
        }
    }
}
