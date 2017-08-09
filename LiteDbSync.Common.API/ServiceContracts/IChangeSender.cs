namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeSender
    {
        void SendLatestId(long latestId);
    }
}
