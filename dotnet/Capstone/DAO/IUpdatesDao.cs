using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUpdatesDao
    {
        bool ProcessApprovedRequests(string table, int requestId);
    }
}