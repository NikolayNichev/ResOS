using ResOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace ResOS.Services
{
    public class MockDataStore : IDataStore<MenuItems>
    {
        readonly List<MenuItems> items;

        public MockDataStore()
        {
            items = new List<MenuItems>()
            {
                new MenuItems { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new MenuItems { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new MenuItems { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new MenuItems { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new MenuItems { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new MenuItems { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(MenuItems item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MenuItems item)
        {
            var oldItem = items.Where((MenuItems arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((MenuItems arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MenuItems> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MenuItems>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}