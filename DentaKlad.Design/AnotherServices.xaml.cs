using DentaKlad.Core;
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
using System.Windows.Shapes;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для AnotherServices.xaml
    /// </summary>
    public partial class AnotherServices : Window
    {
        Context context;
        public AnotherServices()
        {
            context = new Context();
            InitializeComponent();
            ServiceTable.DataContext = context.Services.ToList();
        }

    }
}
