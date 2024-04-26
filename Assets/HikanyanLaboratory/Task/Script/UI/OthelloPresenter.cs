namespace HikanyanLaboratory.Task.Othello
{
    public class OthelloPresenter
    {
        private readonly OthelloModel _model;
        private readonly OthelloView _view;

        public OthelloPresenter(OthelloModel model, OthelloView view)
        {
            _model = model;
            _view = view;
        }
    }
}