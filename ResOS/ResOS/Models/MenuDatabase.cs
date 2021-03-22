using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Threading.Tasks;

namespace ResOS.Models
{
    class MenuDatabase
    {
        private DatabaseModel databaseModel;
        private SQLiteAsyncConnection connection;
        public MenuDatabase() 
        {
            databaseModel = new DatabaseModel();
            connection = databaseModel.GetConnection();
            connection.CreateTableAsync<MenuItems>();
        }

        public async Task<List<MenuItems>> GetMenuItemsAsync() 
        {
            return await connection.Table<MenuItems>().ToListAsync();
        }

        public async Task AddMenuItem(MenuItems menuItem) 
        {
            await connection.InsertAsync(menuItem);
        }
        public async Task UpdateMenuItem(MenuItems menuItem) 
        {
            await connection.UpdateAsync(menuItem);
        }

        public async Task<MenuItems> GetMenuItem(int id) 
        {
            return await connection.FindAsync<MenuItems>(id);
        }
        public async Task DeleteMenuItem(MenuItems menuItem) 
        {
            await connection.DeleteAsync(menuItem);
        }
        

        public async Task<List<MenuItems>> GetAddedItems() 
        {
            return await connection.QueryAsync<MenuItems>("SELECT * FROM MenuItems WHERE IsAdded = 1");
        }
    }
}
