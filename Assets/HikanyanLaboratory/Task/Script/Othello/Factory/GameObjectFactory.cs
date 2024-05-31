using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Factory
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly GameObject _blackStonePrefab;
        private readonly GameObject _whiteStonePrefab;
        private readonly GameObject _boardPrefab;

        public GameObjectFactory(GameObject blackStonePrefab, GameObject whiteStonePrefab, GameObject boardPrefab)
        {
            _blackStonePrefab = blackStonePrefab;
            _whiteStonePrefab = whiteStonePrefab;
            _boardPrefab = boardPrefab;
        }

        public GameObject CreatePiece(int color)
        {
            if (color == 0)
            {
                return Object.Instantiate(_blackStonePrefab);
            }
            else
            {
                return Object.Instantiate(_whiteStonePrefab);
            }
        }

        public GameObject CreateBoard()
        {
            return Object.Instantiate(_boardPrefab);
        }
    }
}