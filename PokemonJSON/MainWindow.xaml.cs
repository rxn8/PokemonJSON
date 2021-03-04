using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokemonJSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string url = "https://pokeapi.co/api/v2/pokemon/?limit=1200";
            PokemonAPI api = new PokemonAPI();

            using (var client = new HttpClient())
            {
                string json = client.GetStringAsync(url).Result;
                var result = client.GetAsync(url).Result;

                if (result.IsSuccessStatusCode == true)
                {
                    api = JsonConvert.DeserializeObject<PokemonAPI>(json);
                }
            }

            foreach (var result in api.results = api.results.OrderBy(x => x.name).ToList())
            {
                lstPokemon.Items.Add(result);
            }
        }

        private void lstPokemon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pokemon = (PokemonDetails)lstPokemon.SelectedItem;
            imgPokemon.Source = new BitmapImage(new Uri(pokemon.sprites.front_default));


            //PokemonAPI api = new PokemonAPI();

            //using (var client = new HttpClient())
            //{
            //    string json = client.GetStringAsync(pokemon.url).Result;

            //    var result = client.GetAsync(pokemon.url).Result;
            //    if (result.IsSuccessStatusCode == true)
            //    {
            //        api = JsonConvert.DeserializeObject<PokemonAPI>(json);
            //    }
            //}
        }
    }
}