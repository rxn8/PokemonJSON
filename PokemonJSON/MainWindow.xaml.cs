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

        int btnFlipCount = 0;
        private void lstPokemon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnFlipCount = 0;
            txtBackSpriteError.Text = "";
            PokemonDetails selectedPokemonDetails = new PokemonDetails();
            PokemonResults p = (PokemonResults)lstPokemon.SelectedItem;

            txtName.Text = p.name.ToUpper();

            using (var client = new HttpClient())
            {
                string pokemonJSON = client.GetStringAsync(p.url).Result;
                selectedPokemonDetails = JsonConvert.DeserializeObject<PokemonDetails>(pokemonJSON);
            }

            txtHeight.Text = (selectedPokemonDetails.height*10).ToString("N0")+" cm";
            txtWeight.Text = (selectedPokemonDetails.weight/10).ToString("N0")+" kg";
            imgPokemon.Source = new BitmapImage(new Uri(selectedPokemonDetails.sprites.front_default));
        }

        private void btnFlip_Button_Click(object sender, RoutedEventArgs e)
        {
            txtBackSpriteError.Text = "";

            if (btnFlipCount == 0)
            {
                ShowBackSprite();
                btnFlipCount++;
            }

            else
            {
                ShowFrontSprite();
                btnFlipCount--;
            }
        }

        private void ShowBackSprite()
        {
            PokemonDetails selectedPokemonDetails = new PokemonDetails();
            PokemonResults p = (PokemonResults)lstPokemon.SelectedItem;

            using (var client = new HttpClient())
            {
                if (p is null)
                {
                    return;
                }

                else
                {
                    string pokemonJSON = client.GetStringAsync(p.url).Result;
                    var result = client.GetAsync(p.url).Result;
                    selectedPokemonDetails = JsonConvert.DeserializeObject<PokemonDetails>(pokemonJSON);

                    if (selectedPokemonDetails.sprites.back_default is null)
                    {
                        txtBackSpriteError.Text = "No back sprite found for this Pokemon.";
                    }

                    else
                    {
                        imgPokemon.Source = new BitmapImage(new Uri(selectedPokemonDetails.sprites.back_default));
                    }
                }
            }   
        }

        private void ShowFrontSprite()
        {
            PokemonDetails selectedPokemonDetails = new PokemonDetails();
            PokemonResults p = (PokemonResults)lstPokemon.SelectedItem;

            using (var client = new HttpClient())
            {
                if (p is null)
                {
                    return;
                }

                else
                {
                    string pokemonJSON = client.GetStringAsync(p.url).Result;

                    selectedPokemonDetails = JsonConvert.DeserializeObject<PokemonDetails>(pokemonJSON);

                    imgPokemon.Source = new BitmapImage(new Uri(selectedPokemonDetails.sprites.front_default));
                }
            }
        }
    }
}