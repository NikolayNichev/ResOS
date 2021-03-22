using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Xamarin.Essentials;

namespace ResOS.Models
{
    class DatabaseModel
    {        
        public SQLiteAsyncConnection GetConnection() 
        {
            var folder = FileSystem.AppDataDirectory;
            var name = "ResOS.db3";
            var path = Path.Combine(folder, name);
            return new SQLiteAsyncConnection(path);
        }
    }
}
