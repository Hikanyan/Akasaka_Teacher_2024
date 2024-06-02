using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class ManagerPresenter : IStartable, ITickable
    {
        private readonly StateMachine _stateMachine;
        private readonly ManagerSceneController _managerSceneController;
        private readonly SceneLoader _sceneLoader;
        private readonly TitleState _titleState;
        private readonly InGameState _inGameState;
        private readonly ResultState _resultState;

        public ManagerPresenter(
            StateMachine stateMachine,
            ManagerSceneController managerSceneController,
            SceneLoader sceneLoader,
            TitleState titleState,
            InGameState inGameState,
            ResultState resultState
        )
        {
            _stateMachine = stateMachine;
            _managerSceneController = managerSceneController;
            _sceneLoader = sceneLoader;
            _titleState = titleState;
            _inGameState = inGameState;
            _resultState = resultState;

            _titleState.Presenter = this;
            _inGameState.Presenter = this;
            _resultState.Presenter = this;
        }

        public void Start()
        {
            // 初期ステートを設定
            ChangeToTitleState();
        }

        public void Tick()
        {
            // ステートマシンのTickメソッドを呼び出し
            _stateMachine.Tick();
        }

        // ステートの切り替え例
        public void ChangeToTitleState()
        {
            _managerSceneController.LoadTitleScene();
            _stateMachine.ChangeState(_titleState);
        }

        public void ChangeToInGameState()
        {
            _managerSceneController.LoadGameScene();
            _stateMachine.ChangeState(_inGameState);
        }

        public void ChangeToResultState()
        {
            _managerSceneController.LoadResultScene();
            _stateMachine.ChangeState(_resultState);
        }
    }
}