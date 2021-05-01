using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Viewer
{
    class LogParser
    {
        public DataTable FillDataTable(string fileName)
        {
            try
            {
                //Create table
                DataTable entries = new DataTable("Entries");
                entries.Columns.Add("Timestamp", typeof(string));
                entries.Columns.Add("Process", typeof(string));
                entries.Columns.Add("TID", typeof(string));
                entries.Columns.Add("Area", typeof(string));
                entries.Columns.Add("Category", typeof(string));
                entries.Columns.Add("EventID", typeof(string));
                entries.Columns.Add("Level", typeof(string));
                entries.Columns.Add("Message", typeof(string));
                entries.Columns.Add("Correlation", typeof(string));

                //Fill table
                FileStream logFile = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(logFile);
                reader.ReadLine().Split(new char[] { '\t' });

                while (!reader.EndOfStream)
                {
                    string[] fields = reader.ReadLine().Split(new char[] { '\t' });

                    DataRow r = entries.Rows.Add(fields);

                    if (r["Timestamp"].ToString().EndsWith("*"))
                    {
                            entries.Rows[entries.Rows.Count - 2]["Message"] = entries.Rows[entries.Rows.Count - 2]["Message"].ToString().TrimEnd('.') + r["Message"].ToString().Substring(3);
                            entries.Rows.Remove(r);
                    }
                }

                if (reader!=null)
                    reader.Dispose();

                return entries;
            }
            catch
            {
                return null;
            }
        }
    }
}
