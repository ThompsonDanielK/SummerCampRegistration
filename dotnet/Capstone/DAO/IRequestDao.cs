using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IRequestDao
    {
        int AddNewCamperUpdateRequest(int userId, Camper newCamperData, Camper currentCamperData);
        int AddNewFamilyUpdateRequest(int userId, Family newFamilyData, Family currentFamilyData, string username);
        List<Update> GetCamperUpdatesByCamperCode(int camperCode);
        List<Update> GetCamperUpdateList(bool isCamperUpdate);
        List<Update> GetFamilyUpdatesByFamilyId(int familyId);
    }
}