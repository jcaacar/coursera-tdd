using CourseraTDD.Gamification;
using System.Collections.Generic;

namespace CourseraTDDTEST.Gamification
{
    internal class StorageMock : MockBase, IStorage
    {
        public const string MethodSave = nameof(Save);
        public const string MethodGetPointsType = nameof(GetPointsType);
        public const string MethodGetTotalPoints = nameof(GetTotalPoints);
        public const string MethodGetAllUserWithPoints = nameof(GetAllUserWithPoints);

        private int[] getTotalPointsReturn;
        private List<string> getPointsTypeReturn;
        private List<string> getAllUserWithPointsReturn;

        public StorageMock()
        {
        }

        public void Save(string user, int points, string pointType)
        {
            AddMethodCalled(MethodSave, new object[] { user, points, pointType });
        }

        public int GetTotalPoints(string user, string pointType)
        {
            var result = getTotalPointsReturn[GetMethodCalledCount()];
            AddMethodCalled(MethodGetTotalPoints, new object[] { user, pointType }, result);

            return result;
        }

        public List<string> GetPointsType(string user)
        {
            AddMethodCalled(MethodGetPointsType, new object[] { user }, getPointsTypeReturn);

            return getPointsTypeReturn;
        }

        public IEnumerable<string> GetAllUserWithPoints()
        {
            AddMethodCalled(MethodGetAllUserWithPoints, null, getAllUserWithPointsReturn);

            return getAllUserWithPointsReturn;
        }

        public void Clear()
        {
            getTotalPointsReturn = null;
            getPointsTypeReturn = null;
            ClearCalledMethods();
        }

        public void SetGetPointsTypeReturn(List<string> list)
        {
            getPointsTypeReturn = list;
        }

        public void SetTotalPointsReturn(int[] points)
        {
            getTotalPointsReturn = points;
        }

        public void SetGetAllUserWithPointsReturn(List<string> list)
        {
            getAllUserWithPointsReturn = list;
        }

    }
}