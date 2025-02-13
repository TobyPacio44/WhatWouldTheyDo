using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class QuestionDetailPage : ContentPage
    {
        public QuestionDetailPage(Question question)
        {
            InitializeComponent();
            BindingContext = question;
        }

        private async void OnAddCharacterClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button?.BindingContext is not Answer selectedAnswer)
                return;

            var selectPage = new SelectCharacterPage();
            selectPage.CharacterSelected += (s, selectedCharacter) =>
            {
                if (selectedCharacter != null)
                {
                    selectedAnswer.Characters.Add(selectedCharacter);
                    selectedCharacter.answer = selectedAnswer;

                    OnPropertyChanged(nameof(Answer));
                }
            };

            await Navigation.PushAsync(selectPage);
        }

        private void DeleteCharacter(object sender, EventArgs e)
        {        
            var image = sender as Image;
            var characterToDelete = image?.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault()?.CommandParameter as Character;

            if (characterToDelete != null)
            {
                characterToDelete.answer.Characters.Remove(characterToDelete);

                OnPropertyChanged(nameof(characterToDelete.answer));
            }
        }

    }
}
