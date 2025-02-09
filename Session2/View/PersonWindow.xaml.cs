using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;

using Session2.Model;

namespace Session2.View
{
    /// <summary>
    /// Логика взаимодействия для PersonWindow.xaml
    /// </summary>
    /// 
    public partial class PersonWindow : Window
    {
        private RoadOfRussiaContext db;
        private bool IsEditEnabled = false;
        public Employee Employee { get; set; }
        public Calendar_ Calendar_ { get; set; }
        private bool ActivateLast = false;
        private bool ActivatePresent = true;
        private bool ActivateFuture = true;
        private VertexControl selectedVertex;
        private string name;
        
        //поля для карточки сотрудника
        public string Surname
        {
            get 
            {
                return SurName.Text; 
            }
            set 
            { 
                SurName.Text = value;
                
            }
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

        //поля для календаря сотрудника
        public int IdCalendar
        {
            get;
            set;
        }
        public string TypeOfEvent
        {
            get { return TypeOfEvent_.Text;}
            set { TypeOfEvent_.Text = value;}
        }
        public int? NameOfStudy
        {
            get
            {
                if (NameOfStudy_.SelectedValue != null) return (int)NameOfStudy_.SelectedValue;
                else return null;
            }
            set {NameOfStudy_.SelectedValue = value; }
        }
        public string Description
        {
            get { return Description_.Text; }
            set { Description_.Text = value; }
        }
        public int? IdAlternate
        {
            get
            {
                if (IdAlternate_.SelectedValue != null) return (int)IdAlternate_.SelectedValue;
                else return null;
            }
            set { IdAlternate_.SelectedValue = value; }
        }
        public DateOnly DateStart
        {
            get
            {
                if (DateStart_.SelectedDate != null)
                {
                    return DateOnly.FromDateTime((DateTime)DateStart_.SelectedDate);
                }
                else return new DateOnly();

            }
            set
            {
                DateStart_.SelectedDate = DateTime.Parse(value.ToString());
            }
        }
        public DateOnly DateFinish
        {
            get
            {
                if (DateFinish_.SelectedDate != null)
                {
                    return DateOnly.FromDateTime((DateTime)DateFinish_.SelectedDate);
                }
                else return new DateOnly();

            }
            set
            {
                DateFinish_.SelectedDate = DateTime.Parse(value.ToString());
            }
        }


        

        public PersonWindow(Employee emp, VertexControl vertex)
        {
 
            InitializeComponent();
            db = new RoadOfRussiaContext();
            Employee = emp;
            if (vertex != null) {
                selectedVertex = vertex;
                Department_.Text = selectedVertex.NameDepartment;
                List<Employee> list = db.Employees.Where(p => p.IdDepartment == selectedVertex!.Department && p.IdEmployee != emp.IdEmployee).ToList();

                Helper_.ItemsSource = list;
                Helper_.SelectedValuePath = "IdEmployee";
                Helper_.DisplayMemberPath = "Surname";
                Boss_.ItemsSource = list;
                Boss_.SelectedValuePath = "IdEmployee";
                Boss_.DisplayMemberPath = "Surname";
                Boss_.ItemsSource = list;


                List<string> strings = new List<string>() {"Обучение", "Временное отсутствие", "Отпуск"};
                List<Event> events = db.Events.Where(p => p.DateOfEvent >= DateTime.Now).ToList();

                TypeOfEvent_.ItemsSource = strings;
                IdAlternate_.ItemsSource = list;
                IdAlternate_.SelectedValuePath = "IdEmployee";
                IdAlternate_.DisplayMemberPath = "Surname";
                NameOfStudy_.ItemsSource = events;
                NameOfStudy_.SelectedValuePath = "IdEvent";
                NameOfStudy_.DisplayMemberPath = "EventName";

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
                Department_.IsEnabled = false;

            }

            else
            {
                IsEditEnabled = true;
                Button_Edit.Visibility = Visibility.Hidden;
            }

            //вывод мероприятий
            UpdateEvents();
        }
       

        //обновление списка мероприятий
        private void UpdateEvents()
        {
            List<Calendar_> temp = db.Calendars.Where(p => p.IdEmployee == Employee.IdEmployee).ToList();

            List<Calendar_> listAll = new();
            if (ActivateLast)
            {
                foreach (var item in temp)
                {
                    if (item.DateFinish < DateOnly.FromDateTime(DateTime.Now))
                    {
                        listAll.Add(item);
                    }
                }
            }
            if (ActivatePresent)
            {
                foreach (var item in temp)
                {
                    if (item.DateStart == DateOnly.FromDateTime(DateTime.Now) || item.DateFinish == DateOnly.FromDateTime(DateTime.Now))
                    {
                        listAll.Add(item);
                    }
                }
            }
            if (ActivateFuture)
            {
                foreach (var item in temp)
                {
                    if (item.DateStart > DateOnly.FromDateTime(DateTime.Now))
                    {
                        listAll.Add(item);
                    }
                }
            }

            listAll.Sort();


            var listStudy = (from c in listAll
                             where c.TypeOfEvent == "Обучение"
                             from e in db.Events
                             where e.IdEvent == c.IdEvent
                             select new
                             {
                                 Id = c.IdCalendar,
                                 Dates = c.DateStart + " - " + c.DateFinish,
                                 EvName = e.EventName,
                                 DescriptionStudy = e.EventDescription
                             }).ToList();
            var listSkip = (from c in listAll
                            where c.TypeOfEvent == "Временное отсутствие"
                            select new
                            {
                                Id = c.IdCalendar,
                                Dates = c.DateStart + " - " + c.DateFinish,
                                TypeOfAbsence_ = c.TypeOfAbsense,
                                Alternate_ = "Замена: " + db.Employees.FirstOrDefault(p => p.IdEmployee == c.IdAlternate).Surname +
                                 " " + db.Employees.FirstOrDefault(p => p.IdEmployee == c.IdAlternate).FirstName 

                            }).ToList();
            var listVacation = (from c in listAll
                                where c.TypeOfEvent == "Отпуск"
                                select new
                                {
                                    Id = c.IdCalendar,
                                    Dates = c.DateStart + " - " + c.DateFinish

                                }).ToList();
           
            
            StudyList.ItemsSource = listStudy;
           
            SkipList.ItemsSource = listSkip;
            
            VacationList.ItemsSource = listVacation;

        
        }

        //добавление мероприятий в календаре сотрудника

        private void Button_EventSave(object sender, RoutedEventArgs e)
        {
            Calendar_ calendar_ = new Calendar_
            {
                IdEmployee = Employee.IdEmployee,
                TypeOfEvent = TypeOfEvent,
                IdEvent = NameOfStudy,
                DateStart = DateStart,
                DateFinish = DateStart,
                IdAlternate = IdAlternate,
                TypeOfAbsense = Description
            };
            db.Calendars.Add(calendar_);
            db.SaveChanges();
            UpdateEvents();
            ClearAddEv(); 
        }

        private void Button_EventNotSave(object sender, RoutedEventArgs e)
        {
            ClearAddEv();
        }


        private void ClearAddEv()
        {
            TypeOfEvent_.Text = "";
            NameOfStudy_.Text = "";
            IdAlternate_.Text = "";
            Description_.Text = "";
            DateFinish_.Text = "";
            DateStart_.Text = "";
        }
        //кнопки ок/отмена редактирования формы
        private void Button_Ok(object sender, RoutedEventArgs e)
        {
            if(IsEditEnabled) DialogResult=true;
            else DialogResult=false;
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        //кнопка редактирования формы
        private void Button_EditF(object sender, RoutedEventArgs e)
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
                Department_.IsEnabled = true;
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
                Department_.IsEnabled = false;
            }
        }



        //кнопки-фильтры мероприятий
        private void Button_Last(object sender, RoutedEventArgs e)
        {
            if (ActivateLast)
            {
                LastButton.Background = new SolidColorBrush(Colors.LightGreen);
                ActivateLast = false;
                UpdateEvents();
            }
            else
            {
                LastButton.Background = new SolidColorBrush(Colors.Green);
                ActivateLast = true;
                UpdateEvents();
            }
        }

        private void Button_Present(object sender, RoutedEventArgs e)
        {
            if (ActivatePresent)
            {
                PresentButton.Background = new SolidColorBrush(Colors.LightGreen);
                ActivatePresent = false;
                UpdateEvents();
            }
            else
            {
                PresentButton.Background = new SolidColorBrush(Colors.Green);
                ActivatePresent = true;
                UpdateEvents();
            }
        }

        private void Button_Future(object sender, RoutedEventArgs e)
        {
            if (ActivateFuture)
            {
                FutureButton.Background = new SolidColorBrush(Colors.LightGreen);
                ActivateFuture = false;
                UpdateEvents();
            }
            else
            {
                FutureButton.Background = new SolidColorBrush(Colors.Green);
                ActivateFuture = true;
                UpdateEvents();
            }
        }


        //кнопка увольнения сотрудника
        private void Button_Fire(object sender, RoutedEventArgs e)
        {
            var list = from c in db.Calendars
                       where c.IdEmployee == Employee.IdEmployee && c.TypeOfEvent == "Обучение"
                       select c;
            if (list.Count() == 0)
            {
                var result = MessageBox.Show(
                "Вы уверены, что хотите уволить данного сотрудника?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning

                );
                if (result == MessageBoxResult.Yes)
                {
                    
                    
                    Employee emp = db.Employees.FirstOrDefault(p => p.IdEmployee == Employee.IdEmployee)!;
                    emp.IsFired = DateTime.Now;
                    db.Employees.Update(emp);
                    db.Calendars.Where(p => p.IdEmployee == emp.IdEmployee).ExecuteDeleteAsync();
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show(
                        "Вы не можете уволить данного сотрудника из-за запланированного обучения",
                        "Подтверждение",
                        MessageBoxButton.OKCancel
                    );
            }
            
        }


        //кнопка удаления мероприятия
        private void Button_DelEvent(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить данную запись?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
                );
            if (result == MessageBoxResult.Yes)
            {
               
            }
        }
        
        private void Button_DelSkip(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить данную запись?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
                );
            if (result == MessageBoxResult.Yes)
            {
                var x = StudyList.SelectedItem;
                var y = 1;
                
            }
        }
        private void Button_DelVac(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить данную запись?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
                );
            if (result == MessageBoxResult.Yes)
            {
                
            }
        }



        private void Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Space || e.Key == Key.OemQuotes || e.Key == Key.OemSemicolon || e.Key == Key.OemCloseBrackets || e.Key == Key.OemTilde || e.Key == Key.OemOpenBrackets || e.Key == Key.OemPeriod || e.Key == Key.OemComma) return;
            e.Handled = true;
        }
        private void Cabinet_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Space || e.Key >= Key.D0 && e.Key <= Key.D9)) return;
            e.Handled = true;
        }
        private void Phone_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Space || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 &&  e.Key <= Key.NumPad9 || e.Key == Key.OemPlus  || e.Key == Key.OemMinus) return;
            e.Handled = true;
        }
        private void Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) return;
            e.Handled = true;
        }
    }
}
