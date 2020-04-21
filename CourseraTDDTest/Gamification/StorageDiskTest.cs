using CourseraTDD.Gamification;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace CourseraTDDTEST.Gamification
{
    [TestFixture]
    public class StorageDiskTest
    {
        private string FolderPath => Directory.GetCurrentDirectory();

        private readonly string userZeca = "zeca";
        private readonly string userJuju = "juju";

        private readonly int points = 10;

        private readonly string starType = "star";
        private readonly string medalType = "medal";
        private readonly string coinType = "coin";

        private StorageDisk storage;

        [OneTimeSetUp]
        public void Init()
        {
            storage = new StorageDisk(FolderPath);
        }


        [Test]
        public void SaveUserSingle()
        {
            storage.Save(userZeca, points, starType);

            var result = storage.GetTotalPoints(userZeca, starType);

            Assert.AreEqual(points, result);
        }

        [Test]
        public void SaveTheSameUserTwoTimes()
        {
            storage.Save(userZeca, points, starType);
            storage.Save(userZeca, points, starType);

            var result = storage.GetTotalPoints(userZeca, starType);

            Assert.AreEqual(points * 2, result);
        }

        [Test]
        public void GetAllUserThatReceivedPoints()
        {
            var expected = new List<string> { userZeca, userJuju };

            storage.Save(userZeca, points, starType);
            storage.Save(userJuju, points, medalType);

            var result = storage.GetAllUserWithPoints();

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void GetPointsTypeReceivedFromUser()
        {
            var expected = new List<string> { starType, medalType, coinType };

            storage.Save(userZeca, points, starType);
            storage.Save(userZeca, points, medalType);
            storage.Save(userZeca, points, coinType);

            var result = storage.GetPointsType(userZeca);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TearDown]
        public void Cleanup()
        {
            storage.Clear();
        }
    }
}