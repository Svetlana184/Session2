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

        public DateTime? BirthDay
        {
            get 
            {
                if (BirthDay_.SelectedDate != null)
                {
                    return ((DateTime)BirthDay_.SelectedDate!);
                }
                else throw new  Exception("обязательное поле");

            }
            set
            {
                BirthDay_.SelectedDate = value;
            }
        }

        public int? BossId
        {
            get { return (int)Boss_.SelectedValue; }
            set { Boss_.DisplayMemberPath = db.Employees.FirstOrDefault(p => p.IdEmployee == value).Surname; }
        }
        public int? HelperId
        {
            get { return (int)Helper_.SelectedValue; }
            set { Helper_.DisplayMemberPath = db.Employees.FirstOrDefault(p => p.IdEmployee == value).Surname; }
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
                //BirthDay = ()emp.BirthDay;


            }
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult=true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult =false;
        }
    }
}
