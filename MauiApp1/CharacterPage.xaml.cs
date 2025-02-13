using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class CharacterPage : ContentPage
    {
        private string _imagePath;
        public List<Character> Characters { get; set; }

        public CharacterPage()
        {
            InitializeComponent();

            Characters = LoadCharacters();
            CharacterListView.ItemsSource = Characters;

            BindingContext = this;
        }

        // Obs³uga wyboru obrazu
        private async void OnPickImageClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                var localPath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
                using var stream = await result.OpenReadAsync();
                using var newStream = File.Create(localPath);
                await stream.CopyToAsync(newStream);

                _imagePath = localPath;
                CharacterImage.Source = _imagePath;
            }
        }

        // Obs³uga zapisu postaci
        private void OnSaveCharacterClicked(object sender, EventArgs e)
        {
            string name = CharacterNameEntry.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(_imagePath))
            {
                DisplayAlert("B³¹d", "Podaj nazwê i wybierz obraz.", "OK");
                return;
            }

            var newCharacter = new Character { Name = name, ImagePath = _imagePath };
            SaveCharacter(newCharacter);

            ResetList(sender, e);

            DisplayAlert("Sukces", "Postaæ zosta³a dodana!", "OK");

            CharacterNameEntry.Text = string.Empty;
            CharacterImage.Source = null;
        }
        private void ResetList(object sender, EventArgs e)
        {
            Characters = LoadCharacters();
            CharacterListView.ItemsSource = null;
            CharacterListView.ItemsSource = Characters;
        }

        private void DeleteCharacter(object sender, EventArgs e)
        {
            var image = sender as Image;
            var characterToDelete = image?.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault()?.CommandParameter as Character;

            if (characterToDelete != null)
            {
                Characters.Remove(characterToDelete);
                SaveCharacter(null);
                DisplayAlert("Sukces", $"Postaæ {characterToDelete.Name} zosta³a usuniêta!", "OK");
                ResetList(sender, e);
            }
            else
            {
                DisplayAlert("B³¹d", "Nie uda³o siê usun¹æ postaci.", "OK");
            }
        }

        private void SaveCharacter(Character? character)
        {
            if (character != null)
            {
                var characters = LoadCharacters();
                characters.Add(character);
                string json = JsonSerializer.Serialize(characters);
                Preferences.Set("characters", json);
            }
            else
            {
                string json = JsonSerializer.Serialize(Characters);
                Preferences.Set("characters", json);
            }
        }

        private List<Character> LoadCharacters()
        {
            string json = Preferences.Get("characters", "[]");
            return JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();
        }
    }

    public class Character
    {
        public required string Name { get; set; }
        public required string ImagePath { get; set; }
        public Answer answer;
    }
}
