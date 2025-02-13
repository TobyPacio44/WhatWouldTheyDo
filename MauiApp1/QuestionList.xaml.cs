namespace MauiApp1
{

    public partial class QuestionList : ContentPage
    {
        private readonly ApiService _apiService = new ApiService();

        public QuestionList()
        {
            InitializeComponent();
            LoadQuestions(null, null);
        }

        private async void LoadQuestions(object? sender, EventArgs? e)
        {
            List<Question> questions = await _apiService.GetQuestionsAsync();
            questionsListView.ItemsSource = questions;
        }

        private async void OnQuestionTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedQuestion = e.Item as Question;

            if (selectedQuestion != null)
            {
                await Navigation.PushAsync(new QuestionDetailPage(selectedQuestion)); // Przenosimy na nowy ekran
            }
        }
    }
}