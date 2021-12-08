using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ICampDao
    {
        bool AddCamper(Camper camper);
        int AddFamily(Family family);
        List<Camper> FetchAllCampers();
    }
}