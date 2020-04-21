using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CourseraTDD.Gamification
{
    public class StorageDisk : IStorage
    {
        #region Cons

        private const string RootFolder = "storage";

        #endregion

        #region Props

        public string FolderPath { get; }

        #endregion

        #region Ctor

        public StorageDisk(string path)
        {
            FolderPath = Path.Combine(path, RootFolder);
        }

        #endregion

        #region Public

        public void Save(string user, int points, string pointType)
        {
            var userFolderPath = CreateUserFolder(user);
            var pointTypePath = GetPointFilePath(userFolderPath, pointType);

            points += ReadCurrentPoints(pointTypePath);

            File.WriteAllText(pointTypePath, points.ToString());
        }

        public int GetTotalPoints(string user, string pointType)
        {
            var userFolderPath = GetUserPath(user);

            var pointTypePath = GetPointFilePath(userFolderPath, pointType);

            if (File.Exists(pointTypePath))
                return int.Parse(File.ReadAllText(pointTypePath));

            return 0;
        }

        public IEnumerable<string> GetAllUserWithPoints()
        {
            var result = new List<string>();

            if (!Directory.Exists(FolderPath))
                return result;

            foreach (var userFolder in Directory.GetDirectories(FolderPath))
            {
                if (Directory.GetFiles(userFolder).Length > 0)
                {
                    result.Add(Path.GetFileName(userFolder));
                }
            }

            return result;
        }

        public List<string> GetPointsType(string user)
        {
            var userPath = GetUserPath(user);

            if (!Directory.Exists(userPath))
                return new List<string>();

            return Directory.GetFiles(userPath).Select(f => Path.GetFileName(f)).ToList();
        }

        public void Clear()
        {
            if (Directory.Exists(FolderPath))
            {
                Directory.Delete(FolderPath, true);
            }
        }

        #endregion

        #region Private

        private string CreateUserFolder(string user)
        {
            return Directory.CreateDirectory(GetUserPath(user)).FullName;
        }

        private string GetUserPath(string user)
        {
            return Path.Combine(FolderPath, user);
        }

        private string GetPointFilePath(string userPath, string pointType)
        {
            return Path.Combine(userPath, pointType);
        }

        private int ReadCurrentPoints(string pointTypePath)
        {
            if (File.Exists(pointTypePath))
            {
                var currentPoints = File.ReadAllText(pointTypePath);
                if (int.TryParse(currentPoints, out int parsedPoints))
                {
                    return parsedPoints;
                }
            }

            return 0;
        }

        #endregion

    }
}