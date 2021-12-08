using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUpdatesDao
    {
        int AddNewCamperUpdateRequest(int userId, Camper newCamperData);
        int AddNewFamilyUpdateRequest(int userId, Family newFamilyData);
        bool ProcessApprovedRequests(int requestId);
    }
}