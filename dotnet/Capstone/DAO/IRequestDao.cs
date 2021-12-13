using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IRequestDao
    {
        int AddNewCamperUpdateRequest(Update update);
        int AddNewFamilyUpdateRequest(Update update);
        List<Update> GetCamperUpdatesByCamperCode(int camperCode);
        List<Update> GetCamperUpdateList(bool isCamperUpdate);
        List<Update> GetFamilyUpdatesByFamilyId(int familyId);
    }
}