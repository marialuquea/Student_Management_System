/*Object Oriented Software Development
 * Author: Maria Luque Anguita - 40280156
 * Date last modified: 20/10/2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This class provides the main user interface window of the program.
    /// </summary>
    public partial class MainWindow : Window
    {
        //create a new object "store" of type ModuleList to access students
        private ModuleList store = new ModuleList();
        
        //initialize variables for later use
        int counter = 10000;

        //Initialise window
        public MainWindow() 
        {
            InitializeComponent();
        }
        
        //This method reads input values, assigns them to a student, and clears textboxes for new input 
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //create a new student object
            Student aStudent = new Student();
            
            try
            {
                //read input values from user in the textboxes
                //and assign them to the student object values
                aStudent.FirstName = txtBoxName.Text;
                aStudent.Surname = txtBoxSurname.Text;
                aStudent.CWMark = Convert.ToInt16(txtBoxCwMark.Text);
                aStudent.ExamMark = Convert.ToInt16(txtBoxExamMark.Text);
                aStudent.dateOfBirth = DoBPicker.SelectedDate.Value;
                
            }
            catch (Exception except) //if any of these values are wrong, the program will not crash
            {
                MessageBox.Show(except.Message);

                return;
            }

            //increment counter by 1 and assign to student
            counter++;
            aStudent.Matric = counter;

            //Store student in ModuleList.cs
            store.add(aStudent);

            //add matric number to listbox on the right
            listBoxAll.Items.Add(aStudent.Matric);

            //clear all boxes for new input
            txtBoxName.Clear();
            txtBoxSurname.Clear();
            txtBoxCwMark.Clear();
            txtBoxExamMark.Clear();
            DoBPicker.SelectedDate = null;

        }
        
        //This method searches the student by matric number and displays its info
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            //if the Find student textbox is not empty...
            if (txtBoxFind.Text != "")
            {
                //check if the student exists
                if (store.find(Convert.ToInt16(txtBoxFind.Text)) != null)
                {
                    //create a new student with the matric no. that was input
                    Student findStudent = store.find(Convert.ToInt16(txtBoxFind.Text));

                    //display values
                    lblName3.Content = findStudent.FirstName;
                    lblSurname3.Content = findStudent.Surname;
                    lblCW3.Content = findStudent.CWMark;
                    lblExam3.Content = findStudent.ExamMark;
                    lblTotal3.Content = findStudent.getMark() + "%";

                    //clear the "Find" textbox
                    txtBoxFind.Clear();
                }
                else //display error message if matric is not found
                {
                    MessageBox.Show("The matric number does not exist.");
                }
            }
            else //display error message if the textbox is empty
            {
                MessageBox.Show("Please enter a matric number to find.");
            }

            
        }

        //Method to search for wanted student and delete it 
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxFind.Text != "") //if text box is not empty...
            {
                //create a variable to make the code after this shorter
                int matricsearch = Convert.ToInt32(txtBoxFind.Text);

                //if the matric number is a student, delete it
                if (store.find(matricsearch) != null)
                    { store.delete(matricsearch); }
                else //display message if student was not found
                    { MessageBox.Show("The matric number does not exist."); }

                //delete item from the listbox and text from textbox
                listBoxAll.Items.Remove(matricsearch);
                txtBoxFind.Clear();
            }
            else //display error message if the textbox is empty
            {
                MessageBox.Show("Please enter a matric number to delete.");
            }
        }

        //This method creates a new window and adds and displays the details of all the students in a listbox
        private void lblViewAll_Click(object sender, RoutedEventArgs e)
        {
            //open a new window called ViewAll
            ViewAll newWin = new ViewAll();
            newWin.Show();

            //loops each matric number for the students
            foreach(var matricList in store.matrics)
            {
                //making a new listboxitem called viewAll
                var viewAll = new ListBoxItem
                {
                    /*The content of each listbox item is the 
                    ViewAllStudents method I created to display all 
                    the values of a student*/
                    Content = store.find(matricList).ViewAllStudents()
                };

                //Add variable "viewAll" (the ToString method) to listbox
                newWin.listBoxNewWindow.Items.Add(viewAll);
            }
        }

        //This method displays the details of the student selected from the listbox.
        private void listBoxAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if the listbox is not empty...
            if (listBoxAll.SelectedItem != null)
            {
                //copy the matric number selected into the textbox
                txtBoxFind.Text = Convert.ToString(listBoxAll.SelectedItem);

                //create a new student with the matric no. that was input
                Student findStudent2 = store.find(Convert.ToInt16(listBoxAll.SelectedItem));

                //Display student's info
                lblName3.Content = findStudent2.FirstName;
                lblSurname3.Content = findStudent2.Surname;
                lblCW3.Content = findStudent2.CWMark;
                lblExam3.Content = findStudent2.ExamMark;
                lblTotal3.Content = findStudent2.getMark() + "%";
            }
            else //display error message if user tries to select an item and the listbox is empty
            {
                MessageBox.Show("Don't be cheeky.");
            }
           
        }
    }
}