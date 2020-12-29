using System;
using System.Linq;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        // In order to pass a requirement:
        // 1. a student must have gained enough credits from the list of required course
        // 2. AND each of those courses must have been passed with the specified minimum mark
        private bool CoursesMeetRequirement(int requirementId, Course[] courses)
        {
            var requirement = Repository.GetRequirement(requirementId);
            var requirementCredits = 0;
            foreach (var courseId in requirement.Courses)
            {
                var course = courses.FirstOrDefault(c => c.Id == courseId);
                if (course != null && course.Mark >= requirement.MinimumMark)
                {
                    requirementCredits += course.Credits;
                }
            }
            return requirementCredits >= requirement.Credits;
        }

        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            // Ensure that all requirements are met
            var meetsRequirements = diploma.Requirements.All(rId => CoursesMeetRequirement(rId, student.Courses));

            // Calculate average grade and total credits
            var credits = 0;
            var average = 0;
            foreach (var course in student.Courses)
            {
                // Only passed courses count toward total credits
                if (course.Mark >= 50)
                {
                    credits += course.Credits;
                }
                // All courses count toward average
                average += course.Mark;
            }
            average = average / student.Courses.Length;

            var standing = STANDING.None;
            if (average >= 100)
            {
                standing = STANDING.SumaCumLaude;
            }
            else if (average >= 80)
            {
                standing = STANDING.MagnaCumLaude;
            }
            else if (average >= 50)
            {
                standing = STANDING.Average;
            }
            else
            {
                standing = STANDING.Remedial;
            }

            // In order to graduate a student must:
            // 1. have the minimum average grade (>=50)
            // 2. AND have met all of the specified requirements
            // 3. AND have earned the minimum number of credits
            var graduated = (
                standing != STANDING.Remedial
                && meetsRequirements
                && credits >= diploma.Credits
            );

            return new Tuple<bool, STANDING>(graduated, standing);
        }
    }
}
