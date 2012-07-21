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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.DBAccess.Generic.Providers.CSV
{
    /// <summary>
    /// A class for reading from CSV flat files.
    /// </summary>
    public class CSVAccess : IDBAccess
    {
        /// <summary>
        /// Constructs a CSVAccess object for reading from CSV files.
        /// </summary>
        public CSVAccess() : this((String)null) { }

        /// <summary>
        /// Constructs a CSVAccess object using the provided file.
        /// </summary>
        /// <param name="file">The file to read.</param>
        public CSVAccess(String file) : this(new List<String> { file }) { }

        /// <summary>
        /// Constructs a CSVAccess object using the provided list of files for an ExecuteSetRead operation.
        /// </summary>
        /// <param name="files">The list of files to read.</param>
        public CSVAccess(List<String> files)
        {
            ((IDBAccess)this).ModelsData = new Dictionary<Type, ModelData>();
            this.Encoding = Encoding.UTF8;
            this.Files = files;
        }

        /// <summary>
        /// Executes a read against a CSV file.
        /// </summary>
        /// <returns>The ExecuteReadQuickTuple of the parsed CSV file.</returns>
        ExecuteReadQuickTuple IDBAccess.ExecuteReadQuick()
        {
            if (this.File == null)
                throw new ArgumentException("File cannot be null.");

            return this.ReadCSV(this.File, this.Delimiter, this.HasHeaderRow, this.TableName);
        }

        /// <summary>
        /// Not implemented for the CSV provider.
        /// </summary>
        /// <returns>Throws NotImplementedException.</returns>
        public int ExecuteNonQuery()
        {
            throw new NotImplementedException("Not implemented for the CSV provider.");
        }

        /// <summary>
        /// Not implemented for the CSV provider.
        /// </summary>
        /// <returns>Throws NotImplementedException.</returns>
        public object ExecuteScalar()
        {
            throw new NotImplementedException("Not implemented for the CSV provider.");
        }

        /// <summary>
        /// Executes a set read against a collection of CSV files.
        /// </summary>
        /// <returns>The list of ExecuteReadQuickTuple objects.</returns>
        List<ExecuteReadQuickTuple> IDBAccess.ExecuteSetReadQuick()
        {
            if (this.Files == null || !this.Files.Any())
                throw new ArgumentException("Files cannot be null or empty.");

            if (this.Delimiters == null || !this.Delimiters.Any())
                throw new ArgumentException("Delimiters cannot be null or empty.");

            if (this.HasHeaderRows == null || !this.HasHeaderRows.Any())
                throw new ArgumentException("HasHeadRow cannot be null or empty.");

            if (this.Delimiters.Count < this.Files.Count)
                throw new ArgumentException("Number of delimiters cannot be less than the number of files.");

            if (this.HasHeaderRows.Count < this.Files.Count)
                throw new ArgumentException("Number of has head row cannot be less than the number of files.");

            return this.Files.Select((f, i) => this.ReadCSV(this.Files[i], this.Delimiters[i], this.HasHeaderRows[i], this.TableNames == null ? null : this.TableNames.Count > i ? this.TableNames[i] : null)).ToList();
        }

        /// <summary>
        /// The file to use for an ExecuteRead operation.
        /// </summary>
        public String File
        {
            get
            {
                if (this.Files == null || !this.Files.Any())
                    return null;
                else
                    return this.Files[0];
            }
            set
            {
                this.Files = new List<String> { value };
            }
        }

        /// <summary>
        /// The delimiter to use for an ExecuteRead operation.  Defaults to comma.
        /// </summary>
        public Char Delimiter
        {
            get
            {
                if (this.Delimiters == null || !this.Delimiters.Any())
                    return ',';
                else
                    return this.Delimiters[0];
            }
            set
            {
                this.Delimiters = new List<Char> { value };
            }
        }

        /// <summary>
        /// The HasHeaderRow setting for an ExecuteRead operation.  Defaults to false.
        /// </summary>
        public Boolean HasHeaderRow
        {
            get
            {
                if (this.HasHeaderRows == null || !this.HasHeaderRows.Any())
                    return false;
                else
                    return this.HasHeaderRows[0];
            }
            set
            {
                this.HasHeaderRows = new List<Boolean> { value };
            }
        }

        /// <summary>
        /// The TableName to apply to an ExecuteRead operation.
        /// </summary>
        public String TableName
        {
            get
            {
                if (this.TableNames == null || !this.TableNames.Any())
                    return null;
                else
                    return this.TableNames[0];
            }
            set
            {
                this.TableNames = new List<String> { value };
            }
        }

        /// <summary>
        /// The list of files to use for a set read.
        /// </summary>
        public List<String> Files { get; set; }

        /// <summary>
        /// The list of delimiters which correspond to each file for a set read.
        /// </summary>
        public List<Char> Delimiters { get; set; }

        /// <summary>
        /// The list of HasHeaderRow which correspond to each file for a set read.
        /// </summary>
        public List<Boolean> HasHeaderRows { get; set; }

        /// <summary>
        /// The list of table names to apply to the set read.
        /// </summary>
        public List<String> TableNames { get; set; }

        /// <summary>
        /// Sets whether or not model properties not returned by a query should be defaulted to the value defined in the [DALDefaultValue] attribute.
        /// </summary>
        public Boolean PopulateDefaultValues { get; set; }

        /// <summary>
        /// Returns true/false if the models should be populated using multiple threads or not.
        /// </summary>
        Boolean IDBAccess.IsMultiThreaded { get { return this.Threads > 1; } }

        private int m_threads = 1;
        /// <summary>
        /// The number of threads to use when populating the return from ExecuteRead and ExecuteSetRead.  Allowable range is 1-64.
        /// </summary>
        public int Threads
        {
            get { return m_threads; }
            set
            {
                if (value < 1)
                    m_threads = 1;
                else if (value > 64)
                    m_threads = 64;
                else
                    m_threads = value;
            }
        }

        /// <summary>
        /// The level of trace information to output.
        /// </summary>
        public TraceLevel TraceOutputLevel { get; set; }

        /// <summary>
        /// The ModelData dictionary.
        /// </summary>
        Dictionary<Type, ModelData> IDBAccess.ModelsData { get; set; }

        /// <summary>
        /// The encoding to use.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Reads a CSV file using the given options.
        /// </summary>
        /// <param name="file">The file to read.</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <returns>An ExecuteReadQuickTuple of the parsed CSV file.</returns>
        public ExecuteReadQuickTuple ReadCSV(String file, Char delimiter)
        {
            return this.ReadCSV(file, delimiter, true);
        }

        /// <summary>
        /// Reads a CSV file using the given options.
        /// </summary>
        /// <param name="file">The file to read.</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <param name="hasHeaderRow">True/False if the CSV file has a header row or not.</param>
        /// <returns>An ExecuteReadQuickTuple of the parsed CSV file.</returns>
        public ExecuteReadQuickTuple ReadCSV(String file, Char delimiter, Boolean hasHeaderRow)
        {
            return this.ReadCSV(file, delimiter, hasHeaderRow, null);
        }

        private int m_colCount;
        /// <summary>
        /// Reads a CSV file using the given options.
        /// </summary>
        /// <param name="file">The file to read.</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <param name="hasHeaderRow">True/False if the CSV file has a header row or not.</param>
        /// <param name="tableName">The name of the "table" for this CSV file.</param>
        /// <returns>An ExecuteReadQuickTuple of the parsed CSV file.</returns>
        public ExecuteReadQuickTuple ReadCSV(String file, Char delimiter, Boolean hasHeaderRow, String tableName)
        {
            var tuple = new ExecuteReadQuickTuple();
            tuple.DataRows = new List<Object[]>();

            using (var sr = new StreamReader(file, this.Encoding))
            {
                if (hasHeaderRow)
                {
                    tuple.ColumnNames = sr.ReadLine().Split(delimiter).ToList();
                    tuple.ColumnTypes = tuple.ColumnNames.Select(c => typeof(String)).ToList();

                    this.m_colCount = tuple.ColumnNames.Count;
                }
                else
                {
                    this.m_colCount = -1;
                }

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    //after loop add this row
                    tuple.DataRows.Add(this.ReadLine(line, delimiter).ToArray());
                }
            }

            return tuple;
        }

        /// <summary>
        /// Reads the fields from a line.
        /// </summary>
        /// <param name="line">The line to read.</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <returns>A list of objects representing the fields.</returns>
        private List<Object> ReadLine(String line, Char delimiter)
        {
            Boolean inQuotes = false;
            var objs = this.m_colCount == -1 ? new List<Object>() : new List<Object>(this.m_colCount);
            var sb = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                Char c = line[i];

                Char lastC = i == 0 ? c : line[i - 1];

                //start of quote to exclude the del char
                if (c == '"' && !inQuotes && lastC != '\'')
                    inQuotes = true;
                //end of quote to exclude the del char
                else if (c == '"' && inQuotes && lastC != '\'')
                    inQuotes = false;

                //if it's not the delimiter, it's one we can add to the value
                if (inQuotes || c != delimiter)
                    sb.Append(c);
                else if (!inQuotes)
                {
                    //other wise add this string to the objs array and clear the string builder
                    objs.Add(sb.ToString().TrimStart('"').TrimEnd('"'));
                    sb.Clear();
                }
            }

            objs.Add(sb.ToString().TrimStart('"').TrimEnd('"'));
            sb.Clear();

            this.m_colCount = objs.Count;

            return objs;
        }
    }
}