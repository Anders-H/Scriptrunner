using System.Windows.Forms;

namespace Scriptrunner
{
    public class MessageShower : IMessageShower
    {
        public bool Ask(IWin32Window owner, string prompt, string title) =>
            MessageBox.Show(owner, prompt, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

        public void Tell(IWin32Window owner, string prompt, string title) =>
            MessageBox.Show(owner, prompt, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}