using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name)
        {
            Type = GradeBookType.Ranked;
            IsWeighted = isWeighted;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            var grades = Students.OrderByDescending(student => student.AverageGrade).Select(student => student.AverageGrade).ToList();
            int index = (int) Math.Ceiling(Students.Count * 0.2); 
            switch (averageGrade)
            {
                case var g when g >=  grades[index-1]:
                    return 'A';

                case var g when g >= grades[(index * 2) -1]:
                    return 'B';

                case var g when g >= grades[(index * 3) - 1]:
                    return 'C';
               
                case var g when g >= grades[(index * 4) - 1]:
                    return 'D';

                default:
                    return 'F';
            }
        }
        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
