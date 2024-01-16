using System.Collections.ObjectModel;
using System.Text.Json;

namespace gorselFinal;

public partial class HavaDurumu : ContentPage
{
	public HavaDurumu()
	{
        InitializeComponent();

        if (File.Exists(fileName))
        {
            string data = File.ReadAllText(fileName);
            Sehirler = JsonSerializer.Deserialize<ObservableCollection<SehirHavaDurumu>>(data);
        }
        listCity.ItemsSource = Sehirler;
    }
    string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "hdata.json");
    ObservableCollection<SehirHavaDurumu> Sehirler = new ObservableCollection<SehirHavaDurumu>();
    private async void AddClicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("�ehir:", "�ehir ismi", "OK", "Cancel");
        if (sehir != null)
        {
            sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            sehir = sehir.Replace('�', 'C');
            sehir = sehir.Replace('�', 'G');
            sehir = sehir.Replace('�', 'I');
            sehir = sehir.Replace('�', 'O');
            sehir = sehir.Replace('�', 'U');
            sehir = sehir.Replace('�', 'S');
            Sehirler.Add(new SehirHavaDurumu() { Name = sehir });

            string data = JsonSerializer.Serialize(Sehirler);
        }


    }
    private void UpdateClicked(object sender, EventArgs e)
    {
        refreshView.IsRefreshing = false;
    }

    private void refreshView_Refreshing(object sender, EventArgs e)
    {
        
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;

        if (button != null)
        {
            var info = Sehirler.First(o => o.Name == button.CommandParameter.ToString());
            var control = await DisplayAlert("Silinsin mi ?", "Silmeyi onayla", "Tamam", "Iptal");

            if (control)
            {
                Sehirler.Remove(info);
                string data = JsonSerializer.Serialize(Sehirler);
                File.WriteAllText(fileName, data);
            }
        }
    }
}

public class SehirHavaDurumu
{
    public string Name { get; set; }

    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}