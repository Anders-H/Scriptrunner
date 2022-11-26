using System.Windows.Forms;

namespace Scriptrunner
{
    public interface IMessageShower
    {
        bool Ask(IWin32Window owner, string prompt, string title);
        void Tell(IWin32Window owner, string prompt, string title);
    }
}