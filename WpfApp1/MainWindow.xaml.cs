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

            #region LstBox
            //foreach (string dir in Directory.GetDirectories(link))
            //{
            //    DirectoryInfo info = new DirectoryInfo(dir);
            //    ListBox1.Items.Add(info.Name);
            //}

            //foreach (string file in Directory.GetFiles(link))
            //{
            //    DirectoryInfo info = new DirectoryInfo(file);
            //    ListBox1.Items.Add(info.Name);
            //}
            #endregion

            #region что-то
            //public class FileModel
            //{
            //    public string Name { get; set; }
            //    public string Path { get; set; }
            //    public DateTime DateCreated { get; set; }
            //    public FileType Type { get; set; }

            //    public bool IsFolder => Type == FileType.Folder;
            //}

            //public enum FileType
            //{
            //    File, Folder, Drive,
            //    Shortcut
            //}

            //public FileModel File
            //{
            //    get => this.DataContext as FileModel;
            //    set => this.DataContext = value;
            //}

            ///// <summary>
            ///// A callback used for telling 'something' to navigate to the path
            ///// </summary>
            //public Action<FileModel> NavigateToPathCallback { get; set; }



            //private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
            //{
            //    if (e.ChangedButton == MouseButton.Left &&
            //        e.LeftButton == MouseButtonState.Pressed &&
            //        e.ClickCount == 2)
            //    {
            //        NavigateToPathCallback?.Invoke(File);
            //    }
            //}


            //public static List<FileModel> GetDirectories(string directory)
            //{
            //    List<FileModel> directories = new List<FileModel>();

            //    if (!directory.IsDirectory())
            //        return directories;

            //    string currentDirectory = "";

            //    try
            //    {
            //        // Checks for normal directories
            //        foreach (string dir in Directory.GetDirectories(directory))
            //        {
            //            currentDirectory = dir;

            //            DirectoryInfo dInfo = new DirectoryInfo(dir);
            //            FileModel dModel = new FileModel()
            //            {
            //                Name = dInfo.Name,
            //                Path = dInfo.FullName,
            //                DateCreated = dInfo.CreationTime,
            //                Type = FileType.Folder,
            //            };

            //            directories.Add(dModel);
            //        }

            //        // Checks for directory shortcuts
            //        foreach (string file in Directory.GetFiles(directory))
            //        {
            //            if (Path.GetExtension(file) == ".lnk")
            //            {
            //                string realDirPath = ExplorerHelpers.GetShortcutTargetFolder(file);
            //                FileInfo dInfo = new FileInfo(realDirPath);
            //                FileModel dModel = new FileModel()
            //                {

            //                    Name = dInfo.Name,
            //                    Path = dInfo.FullName,
            //                    DateCreated = dInfo.CreationTime,
            //                    Type = FileType.Folder,

            //                };

            //                directories.Add(dModel);
            //            }
            //        }

            //        return directories;
            //    }

            //    catch (IOException io)
            //    {
            //        MessageBox.Show(
            //            $"IO Exception getting folders in directory: {io.Message}",
            //            "Exception getting folders in directory");
            //    }
            //    catch (UnauthorizedAccessException noAccess)
            //    {
            //        MessageBox.Show(
            //            $"No access for a directory: {noAccess.Message}",
            //            "Exception getting folders in directory");
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(
            //            $"Failed to get directories in '{directory}' || " +
            //            $"Something to do with '{currentDirectory}'\n" +
            //            $"Exception: {e.Message}", "Error");
            //    }

            //    return directories;
            //}
            #endregion


        }

        public void TreeViewStart () //создается изначальное дерево папок
        {
            #region TrView

            //DriveInfo[] drives = DriveInfo.GetDrives();
            //foreach (DriveInfo drive in drives)
            //{
            //    TreeView1.Items.Add(drive.Name);
            //}           

            
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = drive.Name;  // + drive.DriveType;                
                TreeView1.Items.Add(item);

                if (drive.IsReady)
                {
                    DirectoryInfo rootDirectory = drive.RootDirectory;
                    if (rootDirectory.Exists)
                    {
                        foreach (DirectoryInfo directory in rootDirectory.GetDirectories()) // можно закомментировать, отсавить только нулевой элемент
                        {
                            item.Items.Add(null);
                            #region 
                            //TreeViewItem subItem = new TreeViewItem();
                            ////subItem.Header = directory.Name;
                            //link = link + directory.Name + @"\";
                            //item.Items.Add(subItem);
                            ////try
                            ////{

                            ////    if (directory.GetDirectories().Length > 0)
                            ////    {
                            ////        subItem.IsExpanded = false;
                            ////    }
                            ////}
                            ////catch
                            ////{
                            ////    subItem.IsExpanded = false;
                            ////}
                            //link = link.Substring(0, link.LastIndexOf(@"\"));
                            //link = link.Substring(0, link.LastIndexOf(@"\") + 1);
                            #endregion
                        }
                    }
                    //string[] dif = Directory.GetDirectories(drive.Name);                 // это было но я убрал (вроде и так работает)  
                    //if (dif.Length > 0)
                    //{
                    //    item.IsExpanded = false;
                    //}
                }
            }
            #endregion
        }

        private void ListBoxItemDoubleClick(object sender, MouseButtonEventArgs args)
        {

            //if (sender is ListBoxItem)
            //{
            //    if (((ListBoxItem)sender).IsSelected)
            //    {


            //        //Label1.Content = "123";

            //        ListBoxItem lbItem = (ListBoxItem)sender;
            //        //backLink = link;
            //        link = link + lbItem.Content + @"\";
            //        //MessageBox.Show(link);
            //        openDirectory(link);

            //        if (link != @"d:\")
            //        {
            //            backButton.IsEnabled = true;
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}


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
                        //link = Path.Combine(link, lbItem.Content.ToString()); // можно так
                        //MessageBox.Show(link);

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
                //openDirectory(link = @"d:\");
                //backButton.IsEnabled = false;

            }
        }


        // https://ru.stackoverflow.com/questions/839535/c-%D0%9F%D0%BE%D0%BB%D1%83%D1%87%D0%B8%D1%82%D1%8C-%D0%B2%D1%8B%D0%B1%D1%80%D0%B0%D0%BD%D0%BD%D0%BE%D0%B5-%D0%B7%D0%BD%D0%B0%D1%87%D0%B5%D0%BD%D0%B8%D0%B5-%D0%B8-%D0%B2%D1%81%D0%B5%D1%85-%D0%B5%D0%B3

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e) 
        {
 #region мб поможет
            //TreeViewItem tvItem = (TreeViewItem)sender;
            //string folderPath;

            //tvItem = e.OriginalSource as TreeViewItem; // получаю ссылку на объект который раскрыл (точнее получаю оъект который раскрыл, то есть имя папки а не её путь)// я заменил item на TvItem(оставил без замен )

            //string strTvItem = tvItem.Header.ToString();
            //if (strTvItem.IndexOf(":") == -1)
            //{

            //    TreeViewItem tvItemParent = tvItem.Parent as TreeViewItem;
            //    folderPath = tvItemParent.Header + @"\" + tvItem.Header + ""; // надо поработать с parent т.к.он не получает весь пед путь а получает только пред папку (E:\123\789     123\789 - вот так будет)
            //    TreeViewItem tvItemPP = tvItemParent.Parent as TreeViewItem;
            //    while (tvItemPP != null && tvItemParent != null)
            //    {
            //        tvItemPP = tvItemParent.Parent as TreeViewItem;
            //        folderPath = tvItemPP.Header + @"\" + folderPath;  // ввести ещё переменную чтобы не путаться 
            //        tvItemParent = tvItemPP.Parent as TreeViewItem; // можно поменять здесь tvitemparent  на другую переменную
            //    }

            //}
            //else
            //{
            //    folderPath = tvItem.Header + "";
            //}

            ////string strTvItem = tvItem.Header.ToString();
            ////if (strTvItem.IndexOf(":") == -1)
            ////{
            ////    //tvItemParent = tvItem.Parent as TreeViewItem; 
            ////    //tvItem = tvItemParent.Header + tvItem.Header;
            ////}           



            //if (tvItem != null && tvItem.HasItems)
            //{
            //    if (Directory.Exists(folderPath))
            //    {
            //        string[] subfolders = Directory.GetDirectories(folderPath);
            //        if (subfolders.Length > 0)
            //        {
            //            tvItem.Items.Clear();
            //            // папки существуют
            //            foreach (string subfolder in subfolders)
            //            {
            //                // добавляю элементы дерева для каждой найденной папки
            //                TreeViewItem subitem = new TreeViewItem();
            //                subitem.Header = Path.GetFileName(subfolder);
            //                subitem.Tag = subfolder;
            //                subitem.Items.Add(null); // добавляем пустой элемент, чтобы показать, что узел раскрывается
            //                tvItem.Items.Add(subitem);
            //            }
            //        }
            //        else
            //        {
            //            // папки не найдены
            //        }
            //    }
            //    else
            //    {
            //        // папка не существует
            //    }
            //}
            #endregion

            TreeViewItem tvItem = (TreeViewItem)sender;
            tvItem = e.OriginalSource as TreeViewItem;
            string strTvItem = tvItem.Header.ToString();
            string path = null;

            if (strTvItem.IndexOf(":") == 1) // == 1 было !=1
            {
                path = tvItem.Header + "";
            }
            else
            {
                path = tvItem.Tag + "";
            }
            tvItem.Items.Clear();


            foreach (var dir in Directory.EnumerateDirectories(path))  //str
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
                        subitem.Items.Add(null); // добавляем пустой элемент, чтобы показать, что узел раскрывается
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

        #region без замены 

        //// если открыть папку то в ней откроются папки из диска
        //private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem tvItem = (TreeViewItem)sender;

        //    TreeViewItem item = e.OriginalSource as TreeViewItem; // получаю ссылку на объект который раскрыл // проверить с sender(заменить попробовать на tvItm)
        //    if (item != null && item.HasItems)
        //    {
        //        string folderPath = tvItem.Header + "";
        //        if (Directory.Exists(folderPath))
        //        {
        //            string[] subfolders = Directory.GetDirectories(folderPath);
        //            if (subfolders.Length > 0)
        //            {
        //                item.Items.Clear();
        //                // папки существуют
        //                foreach (string subfolder in subfolders)
        //                {
        //                    // добавляю элементы дерева для каждой найденной папки
        //                    TreeViewItem subitem = new TreeViewItem();
        //                    subitem.Header = Path.GetFileName(subfolder);
        //                    subitem.Tag = subfolder;
        //                    subitem.Items.Add(null); // добавляем пустой элемент, чтобы показать, что узел раскрывается
        //                    item.Items.Add(subitem);
        //                }
        //            }
        //            else
        //            {
        //                // папки не найдены
        //            }
        //        }
        //        else
        //        {
        //            // папка не существует
        //        }
        //    }
        //}
        #endregion
                
        public void TreeViewItemClick(object sender, MouseButtonEventArgs args)
        {
            //TreeViewItem tvItem = (TreeViewItem)sender;
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

                    //if (link != @"d:\")
                    //{
                    //    backButton.IsEnabled = true;
                    //}
                    //FindName - мб поможет в будующем
                }
                else
                {
                    return;
                }


            }
            #region не работает
            //if (args.ChangedButton == MouseButton.Left &&
            //            args.LeftButton == MouseButtonState.Pressed //&&
            //            /*args.ClickCount == 0*/)
            //{
            //    TreeViewItem tvItem = (TreeViewItem)sender;
            //    link = link + tvItem.Header + @"\";
            //    openDirectory(link);

            //    if (link != @"d:\")
            //    {
            //        backButton.IsEnabled = true;
            //    }
            //}
            #endregion
        }
        

        public void openDirectory(string link)
        {
            if (Directory.Exists(link))
            {
                ListBox1.Items.Clear();
                foreach (string dir in Directory.GetDirectories(link))
                {
                    //DirectoryInfo info = new DirectoryInfo(dir);
                    //ListBox1.Items.Add(info.Name);


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
                    //lbi.Name = dir + "";
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
                //Label1.;

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
                //openDirectory(backLink);
                //link = backLink;
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
                //searchBar.Select(0, searchBar.MaxLength);
            }
           
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {            
                Keyboard.ClearFocus();            
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            #region не надо
            //try
            //{
            //    MenuItem menuItem = sender as MenuItem;
            //    ContextMenu cMenu = menuItem.Parent as ContextMenu;
            //    //ListBox lb = cMenu.PlacementTarget as ListBox; // не надо 

            //    if (cMenu.PlacementTarget is ListBox)
            //    {
            //        if (Directory.Exists(copyLink))
            //        {
            //            CopyDirectory(copyLink, link + copyObjectName, true);
            //            openDirectory(link);
            //        }
            //        else if (File.Exists(copyLink))
            //        {
            //            // создать функцию и передавать туда буллевое значение (cMenu.PlacementTarget is ListBox) но лучше передавать сендер и делать всё там
            //            FileInfo fInfo = new FileInfo(copyLink);
            //            fInfo.CopyTo(link + copyObjectName);

            //            openDirectory(link);
            //        }
            //    }
            //    else
            //    {
            //        ListBoxItem lbi = cMenu.PlacementTarget as ListBoxItem;

            //        if (Directory.Exists(copyLink))
            //        {
            //            CopyDirectory(copyLink, Path.Combine(link, lbi.Content.ToString(), copyObjectName), true); 
            //        }
            //        else if (File.Exists(copyLink))
            //        {
            //            FileInfo fInfo = new FileInfo(copyLink);
            //            fInfo.CopyTo(Path.Combine(link, lbi.Content.ToString(), copyObjectName));
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


            //https://metanit.com/sharp/tutorial/5.3.php
            //https://learn.microsoft.com/ru-ru/dotnet/standard/io/how-to-copy-directories
            #endregion

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
    #region ещё что то
    //public static class ExplorerHelpers
    //{
    //    /// <summary>
    //    /// Checks if a path is a directory
    //    /// </summary>
    //    /// <param name="path"></param>
    //    /// <returns></returns>
    //    public static bool IsDirectory(this string path)
    //    {
    //        return !string.IsNullOrEmpty(path) && Directory.Exists(path);
    //    }

    //    public static string GetShortcutTargetFolder(string shortcutFilename)
    //    {
    //        string pathOnly = Path.GetDirectoryName(shortcutFilename);
    //        string filenameOnly = Path.GetFileName(shortcutFilename);

    //        Shell shell = new Shell();
    //        Folder folder = shell.NameSpace(pathOnly);
    //        FolderItem folderItem = folder.ParseName(filenameOnly);
    //        if (folderItem != null)
    //        {
    //            ShellLinkObject link = (ShellLinkObject)folderItem.GetLink;
    //            return link.Path;
    //        }

    //        return string.Empty;
    //    }
    //}
    #endregion


}
