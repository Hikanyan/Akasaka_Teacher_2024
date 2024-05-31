using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Task.Script.Othello.Factory;
using HikanyanLaboratory.Task.Script.Othello.Model;
using HikanyanLaboratory.Task.Script.Othello.Services;
using HikanyanLaboratory.Task.Script.Othello.View;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    /// <summary>
    /// Othelloのプレゼンター
    /// </summary>
    public abstract class OthelloPresenter
    {
        private readonly StateMachine _stateMachine;
        private readonly PlayerTurnState _playerTurnState;
        private readonly AITurnState _aiTurnState;
        private readonly IOthelloModel _model;
        private readonly OthelloBoardGenerator _boardGenerator;
        private readonly OthelloPieceView _pieceView;
        private readonly IGameService _gameService;
        private readonly IAnimationService _animationService;
        private readonly IPersistenceService _persistenceService;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly SceneLoader _sceneLoader;


        public OthelloPresenter(
            StateMachine stateMachine,
            PlayerTurnState playerTurnState,
            AITurnState aiTurnState,
            IOthelloModel model,
            OthelloBoardGenerator boardGenerator,
            OthelloPieceView pieceView,
            IGameService gameService,
            IAnimationService animationService,
            IPersistenceService persistenceService,
            IGameObjectFactory gameObjectFactory,
            SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _playerTurnState = playerTurnState;
            _aiTurnState = aiTurnState;
            _model = model;
            _boardGenerator = boardGenerator;
            _pieceView = pieceView;
            _gameService = gameService;
            _animationService = animationService;
            _persistenceService = persistenceService;
            _gameObjectFactory = gameObjectFactory;
            _sceneLoader = sceneLoader;

            _playerTurnState.Presenter = this;
            _aiTurnState.Presenter = this;
        }

        public void ShowTitleScreen()
        {
            // タイトル画面を表示する処理
        }

        public void HideTitleScreen()
        {
            // タイトル画面を非表示にする処理
        }

        public void ShowResultScreen()
        {
            // リザルト画面を表示する処理
        }

        public void HideResultScreen()
        {
            // リザルト画面を非表示にする処理
        }


        public async void StartGame()
        {
            await _sceneLoader.LoadSceneAsync("Othello");
            _gameService.StartGame();
            _stateMachine.ChangeState(_playerTurnState);
            UpdateBoard();
        }

        public void EndGame()
        {
            _gameService.EndGame();
            _sceneLoader.LoadSceneAsync("ResultScene").Forget();
        }

        public void PlacePiece(int x, int y)
        {
            _gameService.MakeMove(x, y);
            UpdateBoard();
            SwitchToAITurn();
        }

        public void UpdateBoard()
        {
            OthelloGameState gameState = _model.GetGameState();
            _boardGenerator.UpdateBoard(gameState);
        }

        public void AnimateMove(GameObject piece, Vector2 newPosition)
        {
            _animationService.AnimatePiece(piece, newPosition);
        }

        public async UniTask SaveGameState()
        {
            OthelloGameState gameState = _model.GetGameState();
            await _persistenceService.SaveState(gameState);
        }

        public async UniTask LoadGameState()
        {
            OthelloGameState gameState = await _persistenceService.LoadState();
            _model.SetGameState(gameState);
            UpdateBoard();
        }

        public void SwitchToAITurn()
        {
            _stateMachine.ChangeState(_aiTurnState);
        }

        public void SwitchToPlayerTurn()
        {
            _stateMachine.ChangeState(_playerTurnState);
        }

        public async void SwitchToTitleState()
        {
            await _sceneLoader.LoadSceneAsync("TitleScene");
        }
    }
}