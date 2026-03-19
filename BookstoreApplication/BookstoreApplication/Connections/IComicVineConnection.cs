namespace BookstoreApplication.Connections
{
    public interface IComicVineConnection
    {
        Task<string> Get(string url);
    }
}
