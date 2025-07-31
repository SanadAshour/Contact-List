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
        private int selectedId = 0;

        private void ShowData()
        {
            list.ItemsSource = ADC.MyContacts.ToList();
        }

        private void ClearData()
        {
            fnameTB.Clear();
            lnameTB.Clear();
            PnumTB.Clear();
            emailTB.Clear();
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
            ClearData();
            ShowData();
            MessageBox.Show("ITEMS ADDED!", "CONFIRMATION", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem is MyContacts item)
            {
                fnameTB.Text = item.Fname;
                lnameTB.Text = item.Lname;
                PnumTB.Text = item.PhoneNumber?.ToString() ?? "";
                emailTB.Text = item.email;
                genderCB.Text = item.gender; 
                selectedId = item.id;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId != 0)
            {
                var item = ADC.MyContacts.Find(selectedId);
                item.Fname = fnameTB.Text;
                item.Lname = lnameTB.Text;
                item.PhoneNumber = int.Parse(PnumTB.Text);
                item.email = emailTB.Text;
                item.gender = genderCB.Text;
                ADC.MyContacts.Update(item);
                ADC.SaveChanges();
                ClearData();
                MessageBox.Show("ITEM EDITED!", "CONFIRMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                ShowData();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId != 0)
            {
                var item = ADC.MyContacts.Find(selectedId);
                ADC.MyContacts.Remove(item);
                ADC.SaveChanges();
                ClearData();
                MessageBox.Show("ITEM DELETED!", "CONFIRMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                ShowData();
            }
        }
    }
}
