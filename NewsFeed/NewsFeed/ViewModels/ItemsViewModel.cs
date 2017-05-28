using System;
using System.Diagnostics;
using System.Threading.Tasks;

using NewsFeed.Helpers;
using NewsFeed.Models;
using NewsFeed.Views;

using Xamarin.Forms;

namespace NewsFeed.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "News feed";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ChangeFeedPage, string>(this, "ChangeFeed", async (obj, feedUrl) =>
            {
                var _feedUrl = feedUrl as string;
                ApplicationProperties.SetProperty<string>(ApplicationProperties.PropertyNames.Feed, _feedUrl);
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}