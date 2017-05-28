using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NewsFeed.Models;

using Xamarin.Forms;
using NewsFeed.Helpers;
using System.Net.Http;
using System.Xml.Linq;

[assembly: Dependency(typeof(NewsFeed.Services.ItemsDataStore))]
namespace NewsFeed.Services
{
    public class ItemsDataStore : IDataStore<Item>
    {
        bool isInitialized;
        List<Item> items;

        public async Task<bool> AddItemAsync(Item item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                isInitialized = false;
            }

            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
            {
                return;
            }
                

            items = new List<Item>();
            var feedUri = new Uri(ApplicationProperties.GetProperty<string>(ApplicationProperties.PropertyNames.Feed, ApplicationProperties.DefaultValues.Feed));

            using (var webClient = new HttpClient())
            {
                try
                {
                    string result = await webClient.GetStringAsync(feedUri);
                    XDocument document = XDocument.Parse(result);

                    var temp = ((from u in document.Descendants("item")
                                 select new Item()
                                 {
                                     Title = u.Element("title").Value,
                                     Description = u.Element("description").Value,
                                     Link = new Uri(u.Element("link").Value),
                                     PubDate = DateTime.Parse(u.Element("pubDate").Value),
                                     EncloseUrl = new Uri(u.Element("enclosure").Attribute("url").Value)
                                 }).ToList());

                    items.AddRange(temp.OrderByDescending(x => x.PubDate));
                }
                catch
                {
                    throw;
                }
            }

            isInitialized = true;
        }
    }
}
