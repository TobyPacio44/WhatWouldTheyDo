namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddCharacterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterPage());
        }

        private async void OnQuestionsListClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuestionList());
        }
    }

}
