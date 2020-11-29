using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Windows;

namespace PunchTools
{
    internal static class DB
    {
        internal static bool CreateDBTools(SQLiteConnection sQLiteConnection)
        {
            sQLiteConnection.Open();
            try
            {
                SQLiteCommand command = sQLiteConnection.CreateCommand();

                command.CommandText = "CREATE TABLE PuncherTools (NomenclatureNumberTools INTEGER NOT NULL, NameTools TEXT, TypeTools TEXT, SizesTools TEXT, Quantity INTEGER, MinQuantity INTEGER, MainSize REAL, MinMainSize REAL, PRIMARY KEY(NomenclatureNumberTools));";
                command.ExecuteNonQuery();
                sQLiteConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                sQLiteConnection.Close();
                MessageBox.Show(ex.Message, "Ошибка");
                return false;
            }
        }

        internal static bool AddDBTools(SQLiteConnection sQLiteConnection, Tools tools)
        {
            sQLiteConnection.Open();
            try
            {
                SQLiteCommand command = sQLiteConnection.CreateCommand();

                command.CommandText = "INSERT OR IGNORE INTO PuncherTools(NomenclatureNumberTools, NameTools, TypeTools, SizesTools, Quantity, MinQuantity, MainSize, MinMainSize)VALUES('" + tools.NomenclatureNumberTools + "', '" + tools.NameTools + "', '" + tools.TypeTools + "', '" + tools.SizesTools + "', '" + tools.Quantity + "' , '" + tools.MinQuantity + "' , '" + tools.MainSize + "' , '" + tools.MinMainSize + "')";
                command.ExecuteNonQuery();
                sQLiteConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                sQLiteConnection.Close();
                MessageBox.Show(ex.Message, "Ошибка");
                return false;
            }
        }

        internal static Collection<Tools> ReadDBTools(SQLiteConnection sQLiteConnection)
        {
            sQLiteConnection.Open();
            try
            {
                Collection<Tools> toolses = new Collection<Tools>();
                SQLiteCommand command = sQLiteConnection.CreateCommand();
                command.CommandText = "SELECT * FROM PuncherTools";
                SQLiteDataReader sql = command.ExecuteReader();
                while (sql.Read())
                {
                    Tools tools = new Tools();
                    tools.NomenclatureNumberTools = Convert.ToInt32(sql["NomenclatureNumberTools"]);
                    tools.NameTools = Convert.ToString(sql["NameTools"]);
                    tools.TypeTools = Convert.ToString(sql["TypeTools"]);
                    tools.SizesTools = Convert.ToString(sql["SizesTools"]);
                    tools.Quantity = Convert.ToInt32(sql["Quantity"]);
                    tools.MinQuantity = Convert.ToInt32(sql["MinQuantity"]);
                    tools.MainSize = Convert.ToDouble(sql["MainSize"]);
                    tools.MinMainSize = Convert.ToDouble(sql["MinMainSize"]);
                    toolses.Add(tools);
                }
                sql.Close();
                sQLiteConnection.Close();
                return toolses;
            }
            catch (Exception ex)
            {
                sQLiteConnection.Close();
                MessageBox.Show(ex.Message, "Ошибка");
                return null;
            }
        }
    }
}
