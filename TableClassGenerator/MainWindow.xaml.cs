/*
Copyright 2012 Brian Adams

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Data.DBAccess.Generic;
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Linq;
using System.Windows;

namespace TableClassGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Database> Databases { get; set; }
        private const String getDatabases = "SELECT db_name(s_mf.database_id) FROM sys.master_files s_mf WHERE s_mf.state = 0 AND has_dbaccess(db_name(s_mf.database_id)) = 1 GROUP BY s_mf.database_id ORDER BY 1";
        private const String getTables = "SELECT name FROM [{0}].sys.tables ORDER BY 1";
        private const String getColumns = @"SELECT c.name,c.is_nullable,t.name [type]
FROM [{0}].sys.columns c
JOIN [{0}].sys.types t ON c.system_type_id = t.system_type_id
WHERE c.object_id = OBJECT_ID('[{0}]..[{1}]')
ORDER BY c.column_id"; // 0 is db name // 1 is table name

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new SqlDBAccess();
                if (chkIntegratedSecurity.IsChecked == true)
                    db.ConnectionString = String.Format("server={0};integrated security=true", txtServer.Text);
                else
                    db.ConnectionString = String.Format("server={0};uid={1};pwd={2}", txtServer.Text, txtUserName.Text, txtPassword.Password);

                db.IsStoredProcedure = false;
                db.QueryString = getDatabases;

                this.Databases = db.ExecuteScalarEnumeration<String>()
                                   .Select(d => 
                                       {
                                           db.QueryString = String.Format(getTables, d);
                                           return new Database
                                           {
                                               Name = d,
                                               Tables = db.ExecuteScalarEnumeration<String>()
                                                          .Select(t => 
                                                              {
                                                                  db.QueryString = String.Format(getColumns, d, t);
                                                                  return new Table
                                                                  {
                                                                      Name = t,
                                                                      Columns = db.ExecuteRead<GetColumn>()
                                                                                  .GroupBy(c => c.Name)
                                                                                  .Select(c => Column.GetColumn(c.Key, c.Select(col => col.Type).ToList(), c.First().IsNullable)).ToList()
                                                                  };
                                                              }).ToList()
                                           };
                                       }).ToList();

                comboDatabases.IsEnabled = true;
                comboTables.IsEnabled = true;

                comboDatabases.ItemsSource = this.Databases;
                comboDatabases.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        public class GetColumn
        {
            public String Name { get; set; }
            [DALSQLParameterName(Name = "Is_Nullable")]
            public Boolean IsNullable { get; set; }
            public String Type { get; set; }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            var table = comboTables.SelectedValue as Table;
            if (table == null)
                return;

            String interfaces = "";

            if (chkIQuickPopulate.IsChecked == true && chkIQuickRead.IsChecked == true)
                interfaces = " : IQuickPopulate, IQuickRead";
            else if (chkIQuickPopulate.IsChecked == true)
                interfaces = " : IQuickPopulate";
            else if (chkIQuickRead.IsChecked == true)
                interfaces = " : IQuickRead";

            String classText = String.Format(@"public class {0}{1}
{{
{2}{3}{4}
}}",
                table.Name,
                interfaces,
                String.Join(Environment.NewLine, table.Columns.Select(c => String.Format(@"    public {0} {1} {{ get; set; }}", c.Type, c.Name))),
                chkIQuickPopulate.IsChecked == true ? GetIQuickPopulateFunction(table.Columns) : "",
                chkIQuickRead.IsChecked == true ? GetIQuickReadFunction(table.Columns) : "");

            Clipboard.SetText(classText);
        }

        private String GetIQuickPopulateFunction(List<Column> columns)
        {
            return String.Format(@"{0}{0}    public void DALPopulate(Object[] dr, Dictionary<String, int> indexes)
    {{
{1}
    }}", Environment.NewLine, String.Join(Environment.NewLine, columns.Select(c => String.Format("        this.{0} = dr[indexes[\"{0}\"]].CastToT<{1}>();", c.Name, c.Type))));
        }

        private String GetIQuickReadFunction(List<Column> columns)
        {
            return String.Format(@"{0}{0}    public Object[] ToObjectArray()
    {{
        return new Object [] {{ {1} }};
    }}

    public Dictionary<String, Type> GetColumnNamesTypes()
    {{
        return new Dictionary<String, Type> {{ {2} }};
    }}", Environment.NewLine,
         String.Join(", ", columns.Select(c => String.Format("this.{0}", c.Name))),
         String.Join(", ", columns.Select(c => String.Format("{{\"{0}\", typeof({1})}}", c.Name, c.Type.Replace("?", "")))));
        }
    }
}