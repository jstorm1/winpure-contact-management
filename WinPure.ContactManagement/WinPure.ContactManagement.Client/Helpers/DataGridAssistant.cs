using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WinPure.ContactManagement.Client.Helpers
{
    public static class DataGridAssistant
    {
        public static readonly DependencyProperty BoundColumnsProperty =
            DependencyProperty.RegisterAttached("BoundColumns", typeof (ObservableCollection<string>),
                                                typeof (DataGridAssistant),
                                                new FrameworkPropertyMetadata(null,
                                                                              onBoundColumnsPropertyChanged));

        private static void onBoundColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null) return;

            var columns = e.NewValue as ObservableCollection<string>;
            if (columns == null) return;

            dataGrid.Columns.Clear();
            foreach (string column in columns)
            {
                var dataGridColumn = new DataGridTextColumn {Header = column};
                dataGridColumn.Binding = new Binding {Path = new PropertyPath(column)};


                dataGrid.Columns.Add(dataGridColumn);
            }
        }

        public static void SetBoundColumns(DependencyObject dependencyObject, ObservableCollection<string> value)
        {
            dependencyObject.SetValue(BoundColumnsProperty, value);
        }
    }
}