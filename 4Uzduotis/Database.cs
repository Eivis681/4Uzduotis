using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Uzduotis
{
    public class Database
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Sergantis.db; Version = 3; New = True; Compress = True; ");
        public List<DataFormat> GetData()
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "Select * From Patient";
            SQLiteDataReader sQLiteDataReader = sqlite_cmd.ExecuteReader();
            List<DataFormat> dataFormats = new List<DataFormat>();
            while (sQLiteDataReader.Read())
            {
                dataFormats.Add(new DataFormat { id = sQLiteDataReader.GetInt32(0),vardas= sQLiteDataReader.GetString(1), x1 = sQLiteDataReader.GetInt32(2), x2 = sQLiteDataReader.GetInt32(3), x3 = sQLiteDataReader.GetInt32(4), x4 = sQLiteDataReader.GetInt32(5), x5 = sQLiteDataReader.GetInt32(6), x6 = sQLiteDataReader.GetInt32(7), x7 = sQLiteDataReader.GetInt32(8), x8 = sQLiteDataReader.GetInt32(9), x9 = sQLiteDataReader.GetInt32(10), x10  = sQLiteDataReader.GetInt32(11) });
            }
            sqlite_conn.Close();
            return dataFormats;
        }
    }
}
