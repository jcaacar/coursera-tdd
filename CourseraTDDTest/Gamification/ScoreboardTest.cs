using CourseraTDD.Gamification;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace CourseraTDDTEST.Gamification
{
    [TestFixture]
    public class ScoreboardTest
    {
        private string FolderPath => Directory.GetCurrentDirectory();

        private readonly string userZeca = "zeca";
        private readonly string userGlau = "glau";
        private readonly string userJuju = "juju";

        private readonly int points = 10;

        private readonly string starType = "star";
        private readonly string medalType = "medal";
        private readonly string coinType = "coin";

        private StorageDisk storage;
        private Scoreboard scoreboard;

        private StorageMock storageMock;
        private Scoreboard scoreboardMocked;

        [OneTimeSetUp]
        public void Init()
        {
            storage = new StorageDisk(FolderPath);
            scoreboard = new Scoreboard(storage);

            storageMock = new StorageMock();
            scoreboardMocked = new Scoreboard(storageMock);
        }

        [Test]
        public void RegisterUserPoints()
        {
            scoreboardMocked.RegisterUser(userZeca, points, starType);

            storageMock.VerifyCalledMethod(StorageMock.MethodSave, new object[] { userZeca, points, starType });
        }

        [Test]
        public void RegisterUserPointsIntegration()
        {
            scoreboard.RegisterUser(userZeca, points, starType);

            var result = storage.GetTotalPoints(userZeca, starType);
            Assert.AreEqual(points, result);
        }

        [Test]
        public void GetAllPointsFromUser()
        {
            scoreboardMocked.RegisterUser(userZeca, points, starType);
            scoreboardMocked.RegisterUser(userZeca, points, medalType);
            scoreboardMocked.RegisterUser(userZeca, points, coinType);

            scoreboardMocked.RegisterUser(userJuju, points, medalType);

            var getPointsTypeExpected = new List<string> { starType, medalType, coinType };
            storageMock.SetGetPointsTypeReturn(getPointsTypeExpected);
            storageMock.SetTotalPointsReturn(new int[] { points, points, points });

            var result = scoreboardMocked.GetAllPointsByType(userZeca);

            storageMock.VerifyCalledMethod(StorageMock.MethodGetPointsType, new object[] { userZeca }, getPointsTypeExpected);
            storageMock.VerifyCalledMethod(StorageMock.MethodGetTotalPoints, new object[] { userZeca }, points, getPointsTypeExpected.Count);

            var expected = new List<string> {
                $"{points} points of {starType}",
                $"{points} points of {medalType}",
                $"{points} points of {coinType}"
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void GetAllPointsFromUserIntegration()
        {
            scoreboard.RegisterUser(userZeca, points, starType);
            scoreboard.RegisterUser(userZeca, points, medalType);

            scoreboard.RegisterUser(userJuju, points, medalType);

            var result = scoreboard.GetAllPointsByType(userZeca);

            var expected = new List<string> { $"{points} points of {starType}", $"{points} points of {medalType}" };
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void GetRankingOfUsersByPointType()
        {
            scoreboardMocked.RegisterUser(userZeca, points*2, starType);
            scoreboardMocked.RegisterUser(userGlau, points, starType);
            scoreboardMocked.RegisterUser(userJuju, points/2, starType);

            var expected = new List<string> { 
                $"{userZeca} - {points*2} points",
                $"{userGlau} - {points} points",
                $"{userJuju} - {points/2} points"
            };

            var expectedTotalPoints = new int[] { points * 2, points, points / 2 };
            storageMock.SetTotalPointsReturn(expectedTotalPoints);

            var expectedAllUserWithPoints = new List<string> { userZeca, userGlau, userJuju };
            storageMock.SetGetAllUserWithPointsReturn(expectedAllUserWithPoints);

            var result = scoreboardMocked.getRanking(starType);

            storageMock.VerifyCalledMethod(StorageMock.MethodGetAllUserWithPoints, null, expectedAllUserWithPoints);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void GetRankingOfUsersByPointTypeIntegration()
        {
            scoreboard.RegisterUser(userGlau, points, starType);
            scoreboard.RegisterUser(userJuju, points / 2, starType);
            scoreboard.RegisterUser(userZeca, points * 2, starType);

            var result = scoreboard.getRanking(starType);

            var expected = new List<string> {
                $"{userZeca} - {points*2} points",
                $"{userGlau} - {points} points",
                $"{userJuju} - {points/2} points"
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TearDown]
        public void Cleanup()
        {
            storage.Clear();
            storageMock.Clear();
        }
    }
}
