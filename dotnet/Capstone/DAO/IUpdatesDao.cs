using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUpdatesDao
    {
        bool ProcessApprovedRequests(string table, int requestId);
        bool FinalizeRequest(string table, int requestId, string status);
    }
}