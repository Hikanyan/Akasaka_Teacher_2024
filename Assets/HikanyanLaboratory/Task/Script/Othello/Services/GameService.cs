using System.Collections.Generic;
using HikanyanLaboratory.Task.Script.Othello.Factory;
using HikanyanLaboratory.Task.Script.Othello.Model;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Services
{
    public class GameService : IGameService
    {
        private readonly IOthelloModel _model;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameService(IOthelloModel model, IGameObjectFactory gameObjectFactory)
        {
            _model = model;
            _gameObjectFactory = gameObjectFactory;
        }

        public void StartGame()
        {
            // ゲーム開始時の初期化処理を実装
            Debug.Log("Game Started");
            var initialPieces = new List<GameObject>();
            // 初期の駒の配置を設定
            _model.SetGameState(new OthelloGameState
            {
                Pieces = initialPieces,
                CurrentPlayer = new Player { Color = 0 } // 0が先手（黒）、1が後手（白）とする
            });
        }

        public void EndGame()
        {
            // ゲーム終了時の処理を実装
            Debug.Log("Game Ended");
        }

        public void MakeMove(int x, int y)
        {
            // 駒を配置するロジックを実装
            var gameState = _model.GetGameState();
            var newPiece = _gameObjectFactory.CreatePiece(gameState.CurrentPlayer.Color);
            // 駒の位置を設定
            newPiece.transform.position = new Vector2(x, y);
            gameState.Pieces.Add(newPiece);

            // プレイヤーのターンを切り替える
            gameState.CurrentPlayer.Color = (gameState.CurrentPlayer.Color + 1) % 2;
            _model.SetGameState(gameState);
        }
    }
}