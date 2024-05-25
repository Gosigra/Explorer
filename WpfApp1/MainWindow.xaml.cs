using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;



namespace FileExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string link;
        public string openAppLink;
        public string copyLink;
        public string copyObjectName;

        public MainWindow()
        {
            InitializeComponent();

            TreeViewStart();
        }

        public void TreeViewStart ()
        {
            #region TrView 
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = drive.Name;               
                TreeView1.Items.Add(item);

                if (drive.IsReady)
                {
                    DirectoryInfo rootDirectory = drive.RootDirectory;
                    if (rootDirectory.Exists)
                    {
                        foreach (DirectoryInfo directory in rootDirectory.GetDirectories())
                        {
                            item.Items.Add(null);
                        }
                    }
                }
            }
            #endregion
        }

        private void ListBoxItemDoubleClick(object sender, MouseButtonEventArgs args)
        {
            try
            {
                if (args.ChangedButton == MouseButton.Left &&
                                args.LeftButton == MouseButtonState.Pressed &&
                                args.ClickCount == 1)
                {

                    ListBoxItem lbItem = (ListBoxItem)sender;
                    if (Directory.Exists(link + lbItem.Content))
                    {
                        link = link + lbItem.Content + @"\";

                        openDirectory(link);

                        if (link != @"d:\")
                        {
                            backButton.IsEnabled = true;
                        }
                    }
                    else
                    {
                        openAppLink = link + lbItem.Content;
                        Process.Start(openAppLink);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                link = link.Substring(0, link.LastIndexOf(@"\"));
                link = link.Substring(0, link.LastIndexOf(@"\") + 1);
                openDirectory(link);
            }
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e) 
        {
            TreeViewItem tvItem = (TreeViewItem)sender;
            tvItem = e.OriginalSource as TreeViewItem;
            string strTvItem = tvItem.Header.ToString();
            string path = null;

            if (strTvItem.IndexOf(":") == 1)
            {
                path = tvItem.Header + "";
            }
            else
            {
                path = tvItem.Tag + "";
            }
            tvItem.Items.Clear();

            foreach (var dir in Directory.EnumerateDirectories(path))
            {    
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                
                string attrb = directoryInfo.Attributes.ToString();
                string[] attrbArray = attrb.Split(',');
                if(Array.Exists(attrbArray, word => word == "Hidden") && checkBox.IsChecked == false)
                {
                    continue;
                }

                TreeViewItem subitem = new TreeViewItem();
                subitem.Header = Path.GetFileName(dir);
                subitem.Tag = dir;
                try
                {
                    string[] subFolders = Directory.GetDirectories(dir);
                    if (subFolders.Length > 0)
                    {
                        subitem.Items.Add(null);
                    }
                }
                catch (Exception)
                {                    
                    continue;
                }
                tvItem.Items.Add(subitem);
                tvItem.MouseLeftButtonUp += TreeViewItemClick;
            }
        }        

        public void TreeViewItemClick(object sender, MouseButtonEventArgs args)
        {            
            TreeViewItem tvItem = args.Source as TreeViewItem;

            if (tvItem is TreeViewItem)
            {
                if (tvItem.IsSelected)
                {
                    string strTvItem = tvItem.Header.ToString();
                    if (strTvItem.IndexOf(":") == -1)
                    {
                        link = tvItem.Tag + @"\";                        
                    }
                    else
                    {
                        link = tvItem.Header + "";
                        backButton.IsEnabled = false;
                    }

                    openDirectory(link);
                }
                else
                {
                    return;
                }


            }
        }
        

        public void openDirectory(string link)
        {
            if (Directory.Exists(link))
            {
                ListBox1.Items.Clear();
                foreach (string dir in Directory.GetDirectories(link))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(dir);

                    string attrb = directoryInfo.Attributes.ToString();
                    string[] attrbArray = attrb.Split(',');
                    if (Array.Exists(attrbArray, word => word == "Hidden") && checkBox.IsChecked == false)
                    {
                        continue;
                    }

                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = directoryInfo.Name;                    
                    ListBox1.Items.Add(lbi);
                }
                 
                foreach (string file in Directory.GetFiles(link))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(file);

                    string attrb = directoryInfo.Attributes.ToString();
                    string[] attrbArray = attrb.Split(',');
                    if (Array.Exists(attrbArray, word => word == "Hidden") && checkBox.IsChecked == false)
                    {
                        continue;
                    }

                    ListBox1.Items.Add(directoryInfo.Name);
                }

                searchBar.Text = link;
                if (link.LastIndexOf(@"\") != 2)
                {
                    backButton.IsEnabled = true;
                }
            }
        } 

        public void backButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                link = link.Substring(0, link.LastIndexOf(@"\"));
                link = link.Substring(0, link.LastIndexOf(@"\") + 1);
                openDirectory(link);

                if (link.LastIndexOf(@"\") == 2)
                {
                    backButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                backButton.IsEnabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        public void searchButtonClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(searchBar.Text))
            {
                link = searchBar.Text + @"\";
                openDirectory(link);
            }
            else
            {
                MessageBox.Show("Такой папки не существует");
            }
           
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {            
                Keyboard.ClearFocus();            
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu cMenu = menuItem.Parent as ContextMenu;            

            if (Directory.Exists(copyLink))
            {
                TargetDirectory(cMenu);
            }
            else if (File.Exists(copyLink))
            {
                TargetFile(cMenu);               
            }

        }

        private void TargetDirectory(ContextMenu destination)
        {
            if (destination.PlacementTarget is ListBox)
            {
                CopyDirectory(copyLink, link + copyObjectName, true);
                openDirectory(link);
            }
            else
            {
                ListBoxItem lbi = destination.PlacementTarget as ListBoxItem;

                CopyDirectory(copyLink, Path.Combine(link, lbi.Content.ToString(), copyObjectName), true);
            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {            
            var dir = new DirectoryInfo(sourceDir);            
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        private void TargetFile (ContextMenu destination)
        {
            if (destination.PlacementTarget is ListBox)
            {
                FileInfo fInfo = new FileInfo(copyLink);
                fInfo.CopyTo(link + copyObjectName);

                openDirectory(link);
            }
            else
            {
                ListBoxItem lbi = destination.PlacementTarget as ListBoxItem;
                FileInfo fInfo = new FileInfo(copyLink);

                fInfo.CopyTo(Path.Combine(link, lbi.Content.ToString(), copyObjectName));
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                ListBoxItem lbi = (ListBoxItem)ListBox1.SelectedItem;
                if (Directory.Exists(link + lbi.Content))
                {
                    copyObjectName = lbi.Content + "";
                    copyLink = link + lbi.Content;
                }
            }
            catch
            {
                copyObjectName = ListBox1.SelectedItem + "";
               copyLink = link + ListBox1.SelectedItem;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBoxItem lbItem = (ListBoxItem)ListBox1.SelectedItem;
                if (Directory.Exists(link + lbItem.Content))
                {
                    Directory.Delete(link + lbItem.Content, true);
                }
            }
            catch
            {
                File.Delete(link + ListBox1.SelectedItem);
            }
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex);
        }
                
        private void searchButton_Changed(object sender, RoutedEventArgs e)
        {
            openDirectory(link);

            TreeView1.Items.Clear();
            TreeViewStart();
        }

    }
}
