/*Object Oriented Software Development
 * Author: Maria Luque Anguita - 40280156
 * Last date modified: 20/10/2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    /// <summary>
    /// This class allows me to create student objects to set and get values from.
    /// It also contains the getMark() method to calculate the overall grade
    /// and the ViewAllStudents() to print all the student details into the listbox.
    /// </summary>

    public class Student
    {
        //Initialise variables
        private string _firstName;
        private string _surname;
        private int _cwMark;
        private int _examMark;
        private DateTime _doB;
        private int _matricNo;
        
        //Getters and Setters for all the variables
        public int Matric
        {
            get { return _matricNo; }
            set { _matricNo = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                //Exception handling for validation - make sure it is not null
                if (value == "")
                    throw new ArgumentException("Please complete name");
                _firstName = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                //Exception handling for validation - not null
                if (value == "")
                    throw new ArgumentException("Please complete surname");
                _surname = value;
            }
        }

        public int CWMark
        {
            get { return _cwMark; }
            set
            {
                //Exception handling for validation - 0 to 20
                if (value < 0 || value > 20)
                    throw new ArgumentException("Coursework Mark out of range.");
                _cwMark = value;
            } 
        }

        public int ExamMark
        {
            get { return _examMark; }
            set
            {
                //Exception handling for validation - 0 to 40
                if (value < 0 || value > 40)
                    throw new ArgumentException("Exam Mark out of range.");
                _examMark = value;
            }
        }

        public DateTime dateOfBirth
        {
            get { return _doB; }
            set { _doB = value; }
        }

        //Method to generate an overall mark out of 100 from CW and Exam mark
        public double getMark()
        {
            double overallGrade = (CWMark * 2.5) + (ExamMark * 1.25);
            return overallGrade;
        }

        //Method to print all the values from a student into the ListBox in VIewAll
        public string ViewAllStudents()
        {
            string everything = Matric + ", " + FirstName + " " + Surname + ", Exam Mark: " + ExamMark + ", CW Mark: " + CWMark +", Total grade: " + getMark() + "%, DOB: " + dateOfBirth.ToString("d");
            return everything;
        }
    }
}