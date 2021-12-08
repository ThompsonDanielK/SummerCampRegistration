using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ICampDao
    {
        bool AddCamper(Camper camper);
        int AddFamily(Family family);
        List<Camper> FetchAllCampers();
        List<Family> FetchAllFamilies();
        Camper FetchCamper(int camperCode);
        Family FetchFamily(int familyId);
    }
}