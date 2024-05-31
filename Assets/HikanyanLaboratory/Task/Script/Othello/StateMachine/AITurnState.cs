using HikanyanLaboratory.Task.Script.Othello.Model;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public class AITurnState : State
    {
        private readonly IOthelloModel _model;
        public OthelloPresenter Presenter { get; set; }

        public AITurnState(IOthelloModel model)
        {
            _model = model;
        }

        public override void Enter()
        {
            // Enter時の処理
        }

        public override void Execute()
        {
            // 実行時の処理
        }

        public override void Exit()
        {
            // Exit時の処理
        }
    }
}