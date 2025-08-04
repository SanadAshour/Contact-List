using Microsoft.EntityFrameworkCore;
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
            FillCategoryCB();
            ShowData();
        }
        private AppDbContext ADC = new AppDbContext();
        private int selectedId = 0;

        private void ShowData()
        {
            list.ItemsSource = ADC.MyContacts.Include(c => c.Category).ToList();
        }

        private void ClearData()
        {
            fnameTB.Clear();
            lnameTB.Clear();
            PnumTB.Clear();
            emailTB.Clear();
            genderCB.SelectedItem = null;
            CategoryCB.SelectedItem = null;
        }

        private void FillCategoryCB()
        {
            CategoryCB.ItemsSource = ADC.Categories.ToList();
        }

        private bool ValidateData()
        {
            bool valid = true;
            if(fnameTB.Text == "")
            {
                valid = false;
                MessageBox.Show("PLEASE ENTER YOUR FIRST NAME!","ERROR!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            if (lnameTB.Text == "")
            {
                valid = false;
                MessageBox.Show("PLEASE ENTER YOUR LAST NAME!", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (PnumTB.Text == "")
            {
                valid = false;
                MessageBox.Show("PLEASE ENTER YOUR NUMBER!", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (emailTB.Text == "")
            {
                valid = false;
                MessageBox.Show("PLEASE ENTER YOUR E-MAIL ADDRESS!", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (genderCB.Text == "")
            {
                valid = false;
                MessageBox.Show("PLEASE SELECT YOUR GENDER!", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return valid;
        }

        private void EditMode()
        {
            Add.IsEnabled = false;
            Edit.IsEnabled = true;
            Delete.IsEnabled = true;
        }

        private void NewMode()
        {
            Add.IsEnabled = true;
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

            ADC.Add(new MyContacts
            {
                Fname = fnameTB.Text,
                Lname = lnameTB.Text,
                PhoneNumber = int.Parse(PnumTB.Text),
                email = emailTB.Text,
                gender = genderCB.Text,
                CategoryId = (int)CategoryCB.SelectedValue
            });

            ADC.SaveChanges();
            ClearData();
            NewMode();
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
                CategoryCB.SelectedValue = item.CategoryId;
                EditMode();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

            if (selectedId != 0)
            {
                var item = ADC.MyContacts.Find(selectedId);
                item.Fname = fnameTB.Text;
                item.Lname = lnameTB.Text;
                item.PhoneNumber = int.Parse(PnumTB.Text);
                item.email = emailTB.Text;
                item.gender = genderCB.Text;
                item.CategoryId = (int)CategoryCB.SelectedValue;
                ADC.MyContacts.Update(item);
                ADC.SaveChanges();
                ClearData();
                NewMode();
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            NewMode();
        }

        private void Manage_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow c = new CategoryWindow();
            c.ShowDialog();
        }

        
    }
}
