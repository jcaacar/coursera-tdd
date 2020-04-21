using System;
using System.Collections.Generic;
using System.Text;

namespace CourseraTDD.Gamification
{
    public interface IStorage
    {
        void Save(string user, int points, string pointType);

        int GetTotalPoints(string user, string pointType);

        IEnumerable<string> GetAllUserWithPoints();

        List<string> GetPointsType(string user);

        void Clear();

    }
}
