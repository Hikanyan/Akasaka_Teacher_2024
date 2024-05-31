using Cysharp.Threading.Tasks;
using UnityEngine;
using HikanyanLaboratory.Task.Script.Othello.Model;
using HikanyanLaboratory.Task.Script.Othello.View;
using HikanyanLaboratory.Task.Script.Othello.Services;
using HikanyanLaboratory.Task.Script.Othello.Factory;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public abstract class OthelloPresenter
    {
        private readonly StateMachine _stateMachine;
        private readonly PlayerTurnState _playerTurnState;
        private readonly AITurnState _aiTurnState;
        private readonly IOthelloModel _model;
        private readonly OthelloBoardView _boardView;
        private readonly OthelloPieceView _pieceView;
        private readonly IGameService _gameService;
        private readonly IAnimationService _animationService;
        private readonly IPersistenceService _persistenceService;
        private readonly IGameObjectFactory _gameObjectFactory;

        public OthelloPresenter(
            StateMachine stateMachine,
            PlayerTurnState playerTurnState,
            AITurnState aiTurnState,
            IOthelloModel model,
            OthelloBoardView boardView,
            OthelloPieceView pieceView,
            IGameService gameService,
            IAnimationService animationService,
            IPersistenceService persistenceService,
            IGameObjectFactory gameObjectFactory)
        {
            _stateMachine = stateMachine;
            _playerTurnState = playerTurnState;
            _aiTurnState = aiTurnState;
            _model = model;
            _boardView = boardView;
            _pieceView = pieceView;
            _gameService = gameService;
            _animationService = animationService;
            _persistenceService = persistenceService;
            _gameObjectFactory = gameObjectFactory;
        }

        public void StartGame()
        {
            _gameService.StartGame();
            _stateMachine.ChangeState(_playerTurnState);
            UpdateBoard();
        }

        public void EndGame()
        {
            _gameService.EndGame();
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
            _boardView.UpdateBoard(gameState);
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
    }
}