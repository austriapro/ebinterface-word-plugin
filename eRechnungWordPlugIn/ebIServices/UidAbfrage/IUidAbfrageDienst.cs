namespace ebIServices.UidAbfrage
{
    public interface IUidAbfrageDienst
    {
        string Name { get; }
        string[] Adrz { get; }
        bool IsCorrect { get; }
        string Message { get; }
        bool Login(string pin, string teilNehmerId, string benutzerId);
        void Logout();
        bool UidAbfrage(string uid2Verify, string billerUid);
    }
}