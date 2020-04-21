using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTDD.Gamification
{
    public class Scoreboard
    {
        private readonly IStorage storage;

        public Scoreboard(IStorage storage)
        {
            this.storage = storage;
        }


        public void RegisterUser(string user, int points, string pointsType)
        {
            storage.Save(user, points, pointsType);
        }

        public IEnumerable<string> GetAllPointsByType(string user)
        {
            var pointsType = storage.GetPointsType(user);

            return pointsType.Select(p =>
            {
                var points = storage.GetTotalPoints(user, p);
                return $"{points} points of {p}";
            });
        }

        public IEnumerable<string> getRanking(string pointType)
        {
            var users = storage.GetAllUserWithPoints();
            var userPointsMap = users.Select(u => ( points: storage.GetTotalPoints(u, pointType), user: u) );

            return userPointsMap.OrderByDescending(i => i.points).Select(i => $"{i.user} - {i.points} points");
        }
    }
}