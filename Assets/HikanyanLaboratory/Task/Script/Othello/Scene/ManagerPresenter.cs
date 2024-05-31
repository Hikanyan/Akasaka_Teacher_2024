using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class ManagerPresenter : IStartable, ITickable
    {
        private readonly StateMachine _stateMachine;
        private readonly ManagerSceneController _managerSceneController;
        private readonly SceneLoader _sceneLoader;

        public ManagerPresenter(
            StateMachine stateMachine,
            ManagerSceneController managerSceneController,
            SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _managerSceneController = managerSceneController;
            _sceneLoader = sceneLoader;
        }

        public void Start()
        {
            // 初期ステートを設定
            _stateMachine.ChangeState(new TitleState());
        }

        public void Tick()
        {
            // ステートマシンのTickメソッドを呼び出し
            _stateMachine.Tick();
        }

        // ステートの切り替え例
        public void ChangeToInGameState()
        {
            _stateMachine.ChangeState(new InGameState());
        }

        public void ChangeToResultState()
        {
            _stateMachine.ChangeState(new ResultState());
        }
    }
}