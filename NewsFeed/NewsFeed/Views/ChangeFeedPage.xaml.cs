using System;

using NewsFeed.Models;

using Xamarin.Forms;

namespace NewsFeed.Views
{
    public partial class ChangeFeedPage : ContentPage
    {
        public string FeedUrl { get; set; }

        public ChangeFeedPage()
        {
            InitializeComponent();
            FeedUrl = "http://www.";

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ChangeFeed", FeedUrl);
            await Navigation.PopToRootAsync();
        }
    }
}