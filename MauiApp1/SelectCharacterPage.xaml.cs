using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace MauiApp1
{
    public partial class SelectCharacterPage : ContentPage
    {
        public event EventHandler<Character> CharacterSelected;
        public List<Character> Characters { get; set; }
        public Character SelectedCharacter { get; set; }

        public SelectCharacterPage()
        {
            InitializeComponent();
            LoadCharacters();
            BindingContext = this;
        }

        private void LoadCharacters()
        {
            string json = Preferences.Get("characters", "[]");
            Characters = JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();
        }

        private async void OnCharacterSelected(object sender, EventArgs e)
        {
            if (SelectedCharacter == null)
            {
                await DisplayAlert("B³¹d", "Wybierz postaæ!", "OK");
                return;
            }

            CharacterSelected?.Invoke(this, SelectedCharacter);

            await Navigation.PopAsync();
        }

        private async void OnCancelSelection(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
