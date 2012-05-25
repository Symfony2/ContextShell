using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Bashkarma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {




        
        public List<Item> items;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
            EventManager.RegisterClassHandler(typeof(ListBoxItem),
            ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(this.MouseLeftButtonDownClassHandler));

            items = new List<Item>();
            items.Add(new Item(){Name = "User0", PictureID = 1});
            items.Add(new Item() { Name = "User1", PictureID = 1 });
            items.Add(new Item() { Name = "User2", PictureID = 1 });
            items.Add(new Item() { Name = "User3", PictureID = 1 });
            listBox1.ItemsSource = items;


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Password password = new Password();
            password.ShowDialog();
        }



        private void MouseLeftButtonDownClassHandler(object sender, RoutedEventArgs e)
        {
            
            ListBoxItem item = (ListBoxItem) sender;
            Item currentNode = (Item) item.Content;
            //MessageBox.Show(currentNode.Name);
            Item CurrItem = items.First(nod => nod.Name.Equals(currentNode.Name));
            items.First(nod => nod.Name.Equals(currentNode.Name)).PictureID = CurrItem.PictureID == 0 ? 1:0;
            items.First(nod => nod.Name.Equals(currentNode.Name)).PictureString = "new";
            //listBox1.Items.Refresh();

            //item.IsSelected = true;
        }
        
    }

    
}
