using NewsFeed.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace NewsFeed.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = "Return to news feed";
            Item = item;
            OpenWebCommand = new Command(() => Device.OpenUri(item.Link));
        }

        /// <summary>
        /// Command to open browser uri
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}