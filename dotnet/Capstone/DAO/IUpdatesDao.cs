using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUpdatesDao
    {
        bool ProcessApprovedRequests(int requestId);
    }
}