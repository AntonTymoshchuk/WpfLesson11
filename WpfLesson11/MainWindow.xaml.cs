using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfLesson11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Fruit> fruits;
        private ICollectionView view;

        public MainWindow()
        {
            InitializeComponent();
            fruits = new ObservableCollection<Fruit>()
            {
                new Fruit() { Name="Apple", Price=10 },
                new Fruit() { Name="Apple", Price=10 },
                new Fruit() { Name="Apple", Price=10 },
                new Fruit() { Name="Pineapple", Price=15 },
                new Fruit() { Name="Watermelon", Price=25 },
                new Fruit() { Name="Grapes", Price=12 }
            };
            fruitsListView.ItemsSource = fruits;

            view = CollectionViewSource.GetDefaultView(fruitsListView.ItemsSource);

            view.SortDescriptions.Add(new SortDescription("Icon", ListSortDirection.Descending));
            view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));

            view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));

            view.Filter = FilterMethod;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (sender as TextBox);
            int i;
            Int32.TryParse(tb.Tag.ToString(), out i);
            fruits.FirstOrDefault(f => f.Id == i).Name = tb.Text;
        }

        private bool FilterMethod(object obj)
        {
            Fruit fruit = obj as Fruit;
            if (fruit == null)
                return true;
            if (fruit.Name.Contains(searchTextBox.Text))
                return true;
            return false;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            view.Refresh();
        }

        //private void fruitsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ind = fruitsListView.SelectedIndex;
        //    ICollectionView view = CollectionViewSource.GetDefaultView(fruitsListView.ItemsSource);
        //    view.GroupDescriptions.Clear();
        //    view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        //}
    }
}
