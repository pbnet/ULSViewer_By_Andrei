using System;
using System.Collections.Generic;
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
using System.Data;
using System.Collections;
using Microsoft.Win32;
using System.ComponentModel;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        const string rowFilter = "Level like '{0}' AND Category like '{1}' AND Process like '{2}' AND (Message like '*{3}*' OR Level like '*{3}*' OR Process like '*{3}*' OR Category like '*{3}*')";
        const string logPath86 = "c:\\Program Files (x86)\\Common Files\\Microsoft Shared\\web server extensions\\12\\LOGS";
        const string logPath64 = "c:\\Program Files\\Common Files\\Microsoft Shared\\web server extensions\\12\\LOGS";

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        DataView view;

        public Window1()
        {
            InitializeComponent();
         
        }

         

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Multiselect = true;

            //Attempt to automatically open to the log folder if one exists on this machine
            
            if (System.IO.Directory.Exists(logPath64)) open.InitialDirectory = logPath64;
            if (System.IO.Directory.Exists(logPath86)) open.InitialDirectory = logPath86;

            if (open.ShowDialog()==true)
            {
                OpenFile(open.FileNames);
                ClearFilters();
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Stefan Gordon's ULS Viewer, Version 2.0\nhttp://www.stefangordon.com\nReleased under the Microsoft Public License.", "About");
        }

        private void OpenFile(string[] fileNames)
        {    
            try
            {
                DataTable data = null;
                foreach(string fileName in fileNames)
                {
                  
                        LogParser lp = new LogParser();
                        DataTable temp = lp.FillDataTable(fileName);
                        if(data==null) data = temp;
                        else data.Merge(temp);
                 }
                        view = new DataView(data);

                        dataList.ItemsSource = view;
                        List<string> allSev = new List<string>();
                        allSev.Add("*");

                        severityCombo.ItemsSource = (from o in data.AsEnumerable()
                                                     select o.Field<string>("Level")).Distinct().Union(allSev);

                        processCombo.ItemsSource = (from o in data.AsEnumerable()
                                                    select o.Field<string>("Process")).Distinct().Union(allSev);

                        categoryCombo.ItemsSource = (from o in data.AsEnumerable()
                                                     select o.Field<string>("Category")).Distinct().Union(allSev);


                        dataList.Visibility = Visibility.Visible;
                                   
                }  
                catch
                {
                    MessageBox.Show("This is not a valid log file.", "Error");
                }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string level = StringIsNull(severityCombo.SelectedValue);
            string category = StringIsNull(categoryCombo.SelectedValue);
            string process = StringIsNull(processCombo.SelectedValue);
            view.RowFilter = string.Format(rowFilter, level, category, process, textFind.Text==""?"*":textFind.Text);
        }

        private void ClearFilters()
        {
            severityCombo.SelectedIndex=-1;
            categoryCombo.SelectedIndex = -1;
            processCombo.SelectedIndex = -1;
            view.RowFilter = "";
        }

        private string StringIsNull(object value)
        {
            if (value == null || value.ToString() == string.Empty) return "*";
            return value.ToString();
        }

        private void dataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                detailText.Text = ((DataRowView)((ListView)sender).SelectedItem)["Message"].ToString();
            }
        }

        private void dataList_Click(object sender, RoutedEventArgs e)
        {                   
            GridViewColumnHeader headerClicked =
             e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    Sort(headerClicked, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }


        }

        private void Sort(GridViewColumnHeader header, ListSortDirection direction)
        {
            string colname = view.Table.Columns[gview.Columns.IndexOf(header.Column)].ColumnName;
            view.Sort = colname + " " + (direction==ListSortDirection.Ascending?"Asc":"Desc");
        }

        private void textFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            string level = StringIsNull(severityCombo.SelectedValue);
            string category = StringIsNull(categoryCombo.SelectedValue);
            string process = StringIsNull(processCombo.SelectedValue);
            view.RowFilter = string.Format(rowFilter, level, category, process, textFind.Text == "" ? "*" : textFind.Text);
        }

    }
}
