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

namespace Contact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowData();
        }
        private AppDbContext ADC = new AppDbContext();

        private void ShowData()
        {
            list.ItemsSource = ADC.MyContacts.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ADC.Add(new MyContacts
            {
                Fname = fnameTB.Text,
                Lname = lnameTB.Text,
                PhoneNumber = int.Parse(PnumTB.Text),
                email = emailTB.Text,
                gender = genderCB.Text,
            });
            ADC.SaveChanges();

            fnameTB.Clear();
            lnameTB.Clear();
            PnumTB.Clear();
            emailTB.Clear();

            ShowData();
            MessageBox.Show("ITEMS ADDED!", "CONFIRMATION", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}