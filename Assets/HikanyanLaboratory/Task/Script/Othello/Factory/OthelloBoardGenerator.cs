using System.Collections.Generic;
using System.Linq;
using HikanyanLaboratory.Task.Script.Othello.Model;
using UnityEngine;
using VContainer;

namespace HikanyanLaboratory.Task.Script.Othello.Factory
{
    public class OthelloBoardGenerator : MonoBehaviour
    {
        private const int GridSize = 8;
        private GameObject _boardPrefab;
        private readonly List<GameObject> _tiles = new List<GameObject>();

        [Inject]
        public void Construct(GameObject boardPrefab)
        {
            _boardPrefab = boardPrefab;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    var tile = Instantiate(_boardPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    tile.transform.parent = transform;
                    _tiles.Add(tile);
                }
            }
        }

        public void UpdateBoard(OthelloGameState gameState)
        {
            ClearBoard();
            foreach (var piece in gameState.Pieces)
            {
                var piecePosition = piece.transform.position;
                int x = (int)piecePosition.x;
                int y = (int)piecePosition.y;
                var tile = GetTileAtPosition(x, y);
                if (tile == null) continue;
                piece.transform.parent = tile.transform;
                piece.transform.localPosition = Vector3.up * 0.5f;
            }
        }

        private void ClearBoard()
        {
            foreach (var child in _tiles.SelectMany(tile => tile.transform.Cast<Transform>()))
            {
                Destroy(child.gameObject);
            }
        }

        private GameObject GetTileAtPosition(int x, int y)
        {
            return _tiles.FirstOrDefault(tile => tile.transform.position == new Vector3(x, 0, y));
        }
    }
}