

namespace gorselFinal;

public partial class HaberDetay : ContentPage
{
    Item haber;
    public HaberDetay(Item item)
    {
        InitializeComponent();
        haber = item;
        webView.Source = item.link;

    }

    private async void ShareClicked(object sender, EventArgs e)
    {
        await ShareUri(haber.link, Share.Default);
    }

    public async Task ShareUri(string uri, IShare share)
    {
        await share.RequestAsync(new ShareTextRequest
        {
            Uri = uri,
            Title = haber.title
        });
    }
}