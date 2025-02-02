using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Session2.Model;

namespace Session2.View
{
    /// <summary>
    /// Логика взаимодействия для PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        private RoadOfRussiaContext db;
        private bool IsEditEnabled = false;
        public Employee Employee { get; set; }
        private VertexControl selectedVertex;
        public string Surname
        {
            get { return SurName.Text; }
            set { SurName.Text = value; }
        }
        public string Firstname
        {
            get { return FirstName.Text; }
            set { FirstName.Text = value; }
        }
        public string Secondname
        {
            get { return SecondName.Text; }
            set { SecondName.Text = value; }
        }
        public string Position
        {
            get { return Position_.Text; }
            set { Position_.Text = value; }
        }
        public string PhoneWork
        {
            get { return Phonework_.Text; }
            set { Phonework_.Text = value; }
        }
        public string Phone
        {
            get { return Phone_.Text; }
            set { Phone_.Text = value; }
        }
        public string Cabinet
        {
            get { return Cabinet_.Text; }
            set { Cabinet_.Text = value; }
        }
        public string Email
        {
            get { return Email_.Text; }
            set { Email_.Text = value; }
        }
      
        public string Other
        {
            get { return Other_.Text; }
            set { Other_.Text = value; }
        }

        public DateOnly? BirthDay
        {
            get 
            {
                if (BirthDay_.SelectedDate != null)
                {
                    return DateOnly.FromDateTime((DateTime)BirthDay_.SelectedDate);
                }
                else return new DateOnly();

            }
            set
            {
                BirthDay_.SelectedDate = DateTime.Parse(value.ToString());
            }
        }

        public int? BossId
        {
            get 
            { 
                if (Boss_.SelectedValue != null) return (int)Boss_.SelectedValue; 
                else return null;
            }
            set { Boss_.SelectedValue = value; }
        }
        public int? HelperId
        {
            get 
            {
                if (Helper_.SelectedValue != null) return (int)Helper_.SelectedValue;
                else return null;
            }
            set { Helper_.SelectedValue = value; }
        }
        public PersonWindow(Employee emp, VertexControl vertex)
        {
            InitializeComponent();
            db = new RoadOfRussiaContext();
            Employee = emp;
            if (vertex != null) {
                selectedVertex = vertex;
                Department_.Text = selectedVertex.NameDepartment;
                List<Employee> list = db.Employees.Where(p => p.IdDepartment == selectedVertex!.Department).ToList();

                Helper_.ItemsSource = list;
                Helper_.SelectedValuePath = "IdEmployee";
                Helper_.DisplayMemberPath = "Surname";
                Boss_.ItemsSource = list;
                Boss_.SelectedValuePath = "IdEmployee";
                Boss_.DisplayMemberPath = "Surname";
                Boss_.ItemsSource = list;
            }
            if (emp.IdEmployee != 0)
            {
                Surname = emp.Surname;
                Firstname = emp.FirstName;
                Secondname = emp.SecondName;
                Position = emp.Position;
                PhoneWork = emp.PhoneWork;
                Phone = emp.Phone;
                Cabinet = emp.Cabinet;
                Email = emp.Email;
                Other = emp.Other;
                BossId = emp.IdBoss;
                HelperId = emp.IdHelper;
                BirthDay = emp.BirthDay;
                //доступность элементов
                SurName.IsEnabled = false;
                FirstName.IsEnabled = false;
                SecondName.IsEnabled = false;
                Position_.IsEnabled = false;
                Phonework_.IsEnabled = false;
                Phone_.IsEnabled = false;
                Cabinet_.IsEnabled = false;
                Email_.IsEnabled = false;
                Other_.IsEnabled = false;
                BirthDay_.IsEnabled = false;
                Helper_.IsEnabled = false;
                Boss_.IsEnabled = false;
                IsEditEnabled = false;
            }
            else
            {
                IsEditEnabled = true;
                Button_Edit.Visibility = Visibility.Hidden;
            }
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(IsEditEnabled) DialogResult=true;
            else DialogResult=false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult =false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if (IsEditEnabled=!IsEditEnabled)
            {
                SurName.IsEnabled = true;
                FirstName.IsEnabled = true;
                SecondName.IsEnabled = true;
                Position_.IsEnabled = true;
                Phonework_.IsEnabled = true;
                Phone_.IsEnabled = true;
                Cabinet_.IsEnabled = true;
                Email_.IsEnabled = true;
                Other_.IsEnabled = true;
                BirthDay_.IsEnabled = true;
                Helper_.IsEnabled = true;
                Boss_.IsEnabled = true;
            }
            else
            {
                SurName.IsEnabled = false;
                FirstName.IsEnabled = false;
                SecondName.IsEnabled = false;
                Position_.IsEnabled = false;
                Phonework_.IsEnabled = false;
                Phone_.IsEnabled = false;
                Cabinet_.IsEnabled = false;
                Email_.IsEnabled = false;
                Other_.IsEnabled = false;
                BirthDay_.IsEnabled = false;
                Helper_.IsEnabled = false;
                Boss_.IsEnabled = false;
            }
        }
    }
}
