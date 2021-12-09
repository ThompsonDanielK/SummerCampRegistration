using Capstone.Models;

namespace Capstone.DAO
{
    public interface IRequestDao
    {
        int AddNewCamperUpdateRequest(int userId, Camper newCamperData, Camper currentCamperData);
        int AddNewFamilyUpdateRequest(int userId, Family newFamilyData, Family currentFamilyData);
    }
}