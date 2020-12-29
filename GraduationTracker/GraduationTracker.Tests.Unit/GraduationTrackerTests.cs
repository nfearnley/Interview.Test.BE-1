using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        public void TestPassed()
        {
            // Credits are met
            // Requirement are met
            // Standing is met
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=50, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=50, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var hasGraduated = result.Item1;

            Assert.IsTrue(hasGraduated);
        }

        [TestMethod]
        public void TestPassedNoRequirements()
        {
            // Requirements are non-existant, therefore met
            // Credits are met
            // Standing is met
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=50, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=50, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var hasGraduated = result.Item1;

            Assert.IsTrue(hasGraduated);
        }

        [TestMethod]
        public void TestFailedPoorStanding()
        {
            // Requirements are met
            // Credits are met
            // Standing is not met
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=50, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=50, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50, Credits=1 },
                    new Course{Id = 5, Name = "Course I regret taking", Mark=0, Credits=1 },
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var hasGraduated = result.Item1;

            Assert.IsFalse(hasGraduated);
        }

        [TestMethod]
        public void TestFailedMissedRequirements()
        {
            // Total credits are met
            // Standing is met
            // Requirements are not met
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=49, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=49, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=49, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=49, Credits=1 },
                    new Course{Id = 5, Name = "Bird identification", Mark=100, Credits=1 },
                    new Course{Id = 6, Name = "Underwater basketweaving", Mark=100, Credits=1 },
                    new Course{Id = 7, Name = "Sitting", Mark=100, Credits=1 },
                    new Course{Id = 8, Name = "Naptime", Mark=100, Credits=1 },
                    new Course{Id = 9, Name = "Spacing out", Mark=100, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var hasGraduated = result.Item1;

            Assert.IsFalse(hasGraduated);
        }

        [TestMethod]
        public void TestFailedMissingCredits()
        {
            // Credits are not met
            // Requirement are met
            // Standing is met
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 40,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=50, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=50, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var hasGraduated = result.Item1;

            Assert.IsFalse(hasGraduated);
        }

        [TestMethod]
        public void TestSumaCumLaude()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=100, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=100, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=100, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var standing = result.Item2;

            Assert.AreEqual(standing, STANDING.SumaCumLaude);
        }

        [TestMethod]
        public void TestMagnaCumLaude()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=80, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=80, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=80, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=80, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var standing = result.Item2;

            Assert.AreEqual(standing, STANDING.MagnaCumLaude);
        }

        [TestMethod]
        public void TestAverage()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=50, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=50, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var standing = result.Item2;

            Assert.AreEqual(standing, STANDING.Average);
        }

        [TestMethod]
        public void TestRemedial()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=49, Credits=1 },
                    new Course{Id = 2, Name = "Science", Mark=49, Credits=1 },
                    new Course{Id = 3, Name = "Literature", Mark=49, Credits=1 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=49, Credits=1 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            var standing = result.Item2;

            Assert.AreEqual(standing, STANDING.Remedial);
        }
    }
}
