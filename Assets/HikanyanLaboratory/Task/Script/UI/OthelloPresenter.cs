using HikanyanLaboratory.Task.Script.Othello;
using HikanyanLaboratory.Task.Script.Othello.Model;

namespace HikanyanLaboratory.Task.Othello
{
    public class OthelloPresenter
    {
        private readonly IOthelloModel _model;
        private readonly OthelloView _view;

        public OthelloPresenter(IOthelloModel model, OthelloView view)
        {
            _model = model;
            _view = view;
        }
    }
}