using AS1.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;

namespace AS1
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        static Boolean delete=false;
        static HttpClient httpClient;
        public Admin()
        {
            InitializeComponent();
            _ = utilities.AS1.showAsync(grid);
            httpClient = utilities.API.conn();
        }

        private void clean()
        {
            producName.Text = "";
            Amount.Text = "";
            Price.Text = "";
            ProductId.Text = "";
        }

        private async void createBtn_Click_1Async(object sender, RoutedEventArgs e)
        {
            Products product = new Products();
            product.Name =producName.Text;
            product.Amount = Convert.ToDecimal( Amount.Text);
            product.Price = Convert.ToDecimal( Price.Text); 

            var response = await httpClient.PostAsJsonAsync<Products>("InsertProduct", product); 
            MessageBox.Show(response.ToString());
            _ = utilities.AS1.showAsync(grid);
            clean();
        }

        private async void EditBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (ProductId.Text == "")
            {
                MessageBox.Show("You must select and Product from the data grid");
            }
            else
            {
                Products product = new Products();
                product.Id=Convert.ToInt32(ProductId.Text);
                product.Name = producName.Text;
                product.Amount = Convert.ToDecimal(Amount.Text);
                product.Price = Convert.ToDecimal(Price.Text);

                var response = await httpClient.PutAsJsonAsync<Products>("UpdateProduct", product);
                MessageBox.Show(response.ToString());
                _ = utilities.AS1.showAsync(grid);
                clean();
            }
        }

        private async void DeleteBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (delete == false)
            {
                MessageBox.Show("You must select a Product to delete from the data grid");
            }
            else
            {
                var response = await httpClient.DeleteAsync("DeleteProduct/" + int.Parse(ProductId.Text)); 
                MessageBox.Show(response.ToString()); 
                
                delete = false;
            } 
            _ = utilities.AS1.showAsync(grid);
            clean();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Products selected_row = grid.SelectedItem as Products;
            if (selected_row != null)
            {
                utilities.AS1.copyToTextBox(this,selected_row);
                delete = true;
            }
           
        }

        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CalendarButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           utilities.tools.numberValidation(e);
        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            utilities.tools.numberValidation(e);
        }
    }
}
