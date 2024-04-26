namespace HikanyanLaboratory.Task.Othello
{
    public class OthelloPresenter
    {
        readonly private OthelloModel _model;
        readonly private OthelloView _view;

        public OthelloPresenter(OthelloModel model, OthelloView view)
        {
            _model = model;
            _view = view;
        }
    }
}