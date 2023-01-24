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
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            var rankList = Students.OrderBy(student => student.AverageGrade).Select(student => student.AverageGrade).ToList();
            rankList.Reverse();
            int index = Students.Count / 5; 
            switch (averageGrade)
            {
                case var d when d > rankList[index]:
                    return 'A';

                case var d when d > rankList[index += index]:
                    return 'B';

                case var d when d > rankList[index += index]:
                    return 'C';
               
                case var d when d > rankList[index += index]:
                    return 'C';

                default:
                    return 'F';
            }
        }
    }
}
