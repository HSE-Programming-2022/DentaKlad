using DentaKlad.Core;
using DentaKlad.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Initial();
        }

        private void Initial()
        {
            using (ApplicationContext context = new ApplicationContext()) //Создание подключения
            {
                Doctor doctor1 = new Doctor("Кладничкин Игорь Дмитриевич");
                Doctor doctor2 = new Doctor("Асланян Ашот Гришаевич");
                Doctor doctor3 = new Doctor("Бекреев Валерий Валентинович");
                Doctor doctor4 = new Doctor("Журавлева Марина Александровна");
                Doctor doctor5 = new Doctor("Клюкина Наталья Викторовна");
                Doctor doctor6 = new Doctor("Целинская Анастасия Тимофеевна");

                context.Doctors.Add(doctor1);
                context.Doctors.Add(doctor2);
                context.Doctors.Add(doctor3);
                context.Doctors.Add(doctor4);
                context.Doctors.Add(doctor5);
                context.Doctors.Add(doctor6);

                context.SaveChanges();

                Client client1 = new Client("Белоусова Зоя Григорьевна", "89155675670", "email0@gmail.com", "4650 394891", "Россия, г. Волгоград, Мичурина ул., д. 18 кв.112");
                Client client2 = new Client("Абрамова Полина Руслановна", "89155675671", "email1@gmail.com", "4446 896998", "Россия, г. Петропавловск-Камчатский, Пионерская ул., д. 9 кв.113");
                Client client3 = new Client("Фокина Алиса Саввична", "89155675672", "email2@gmail.com", "4973 814282", "Россия, г. Каспийск, Гагарина ул., д. 22 кв.11");
                Client client4 = new Client("Волков Михаил Янович", "89155675673", "email3@gmail.com", "4953 498740", "Россия, г. Архангельск, Коммунистическая ул., д. 25 кв.124");
                Client client5 = new Client("Воробьев Дмитрий Демидович", "89155675674", "email4@gmail.com", "4287 211421", "Россия, г. Балаково, Радужная ул., д. 20 кв.190");
                Client client6 = new Client("Сидорова Эмилия Даниловна", "89155675675", "email5@gmail.com", "4859 363626", "Россия, г. Новый Уренгой, Первомайская ул., д. 8 кв.59");
                Client client7 = new Client("Матвеев Николай Васильевич", "89155675676", "email6@gmail.com", "4662 719624", "Россия, г. Грозный, Севернаяул., д. 1 кв.103");
                Client client8 = new Client("Овсянникова Алёна Викторовна", "89155675677", "email7@gmail.com", "4139 844716", "Россия, г. Сочи, Железнодорожная ул., д. 17 кв.205");
                Client client9 = new Client("Исаева Анастасия Данииловна", "89155675678", "email8@gmail.com", "4329 540733", "Россия, г. Самара, Юбилейная ул., д. 6 кв.78");
                Client client10 = new Client("Меркулов Иван Миронович", "89155675676", "email9@gmail.com", "4890 927841", "Россия, г. Подольск, Коммунистическая ул., д. 8 кв.121");

                context.Clients.Add(client1);
                context.Clients.Add(client2);
                context.Clients.Add(client3);
                context.Clients.Add(client4);
                context.Clients.Add(client5);
                context.Clients.Add(client6);
                context.Clients.Add(client7);
                context.Clients.Add(client8);
                context.Clients.Add(client9);
                context.Clients.Add(client10);

                context.SaveChanges();

                Department department1 = new Department("Хирургическое отделение");
                Department department2 = new Department("Ортопедическое отделение");
                Department department3 = new Department("Ортодонтическое отделение");
                Department department4 = new Department("Терапевтическое отделение");

                context.Departments.Add(department1);
                context.Departments.Add(department2);
                context.Departments.Add(department3);
                context.Departments.Add(department4);

                context.SaveChanges();

                Service service1 = new Service("Комплексная диагностика патологии прикуса", 18000, department3);
                Service service2 = new Service("Удаление зуба мудрости", 4000, department1);
                Service service3 = new Service("Восстановление зуба пломбировочными материалами с использованием анкерных штифтов", 6000, department4);
                Service service4 = new Service("Восстановление зуба виниром", 4000, department2);

                context.Services.Add(service1);
                context.Services.Add(service2);
                context.Services.Add(service3);
                context.Services.Add(service4);

                context.SaveChanges();

                Appointment appointment1 = new Appointment(client9, service4, doctor6, "2022-07-09 12:30");
                Appointment appointment2 = new Appointment(client4, service3, doctor5, "2022-07-09 12:30");
                Appointment appointment3 = new Appointment(client5, service2, doctor3, "2022-07-09 12:30");
                Appointment appointment4 = new Appointment(client10, service1, doctor1, "2022-07-09 12:30");
                Appointment appointment5 = new Appointment(client1, service4, doctor2, "2022-07-09 12:30");

                context.Appointments.Add(appointment1);
                context.Appointments.Add(appointment2);
                context.Appointments.Add(appointment3);
                context.Appointments.Add(appointment4);
                context.Appointments.Add(appointment5);

                context.SaveChanges();

                MessageBox.Show("Data saved.");
            }

        }

        private void Button_Click_Depts(object sender, RoutedEventArgs e)
        {
            //Departments departments = new Departments();
            //departments.Show();
        }

        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            Clients clients = new Clients();
            clients.Show();
        }

        private void Button_Click_Services(object sender, RoutedEventArgs e)
        {
            //Services services = new Services();
            //services.Show();
        }

        private void Button_Click_Doctors(object sender, RoutedEventArgs e)
        {
            //Doctor doctors = new Doctors();
            //doctors.Show();
        }

        private void Button_Click_Appointments(object sender, RoutedEventArgs e)
        {
            //Appointments appointment = new Appointments();
            //appointment.Show();
        }
    }
}
