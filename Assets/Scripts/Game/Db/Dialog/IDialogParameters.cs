using Utils;

namespace Game.Db.Dialog
{
    public interface IDialogParameters
    {
        string ChangeAvatarCommandName { get; }
        string WinCommandName { get; }
        string LoseCommandName { get; }

        EWinEnding GetEndingByName(string winEndingName);
    }
}