using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace HikanyanLaboratory.Lesson.Minesweeper
{
    public class Minesweeper : MonoBehaviour
    {
        [SerializeField] private int _rows = 10;
        [SerializeField] private int _columns = 10;
        [SerializeField] private int _mineCount = 10;
        [SerializeField] private float _timeLimit = 120f;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup = null;
        [SerializeField] private Cell _cellPrefab = null;
        [SerializeField] private Text _timerText = null;
        [SerializeField] private Text _resultText = null;

        private Cell[,] _cells;
        private bool _minesPlaced = false;
        private bool _gameOver = false;
        private float _timeRemaining;
        private int _revealedCellCount = 0;

        private void Start()
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _columns;

            GenerateGrid();
            _timeRemaining = _timeLimit;
            _resultText.text = "";
        }

        private void Update()
        {
            if (_gameOver) return;

            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                GameJudgment(false);
            }

            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            if (_timerText == null) return;
            _timerText.text = $"Time: {_timeRemaining:F2}";
        }

        public void GameJudgment(bool win)
        {
            _gameOver = true;
            _resultText.text = win ? "You Win!" : "Game Over";
        }

        void CheckWinCondition()
        {
            if (_revealedCellCount == _rows * _columns - _mineCount)
            {
                GameJudgment(true);
            }
        }

        /// <summary>
        /// グリッドを生成する
        /// </summary>
        private void GenerateGrid()
        {
            _cells = new Cell[_rows, _columns];
            var parent = _gridLayoutGroup.transform;

            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _columns; c++)
                {
                    var cell = Instantiate(_cellPrefab, parent, true);
                    cell.Initialize(this, r, c);
                    _cells[r, c] = cell;
                }
            }
        }

        /// <summary>
        /// セルを開く
        /// </summary>
        /// <param name="cell"></param>
        public void RevealCell(Cell cell)
        {
            if (_gameOver) return;
            if (!_minesPlaced)
            {
                Debug.Log("地雷配置");
                PlaceMines(cell.Row, cell.Column);
                _minesPlaced = true;
            }

            Reveal(cell.Row, cell.Column);

            CheckWinCondition();
        }

        /// <summary>
        /// 隣接しているセルを再帰的に開く
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void Reveal(int row, int column)
        {
            if (row < 0 || row >= _rows || column < 0 || column >= _columns) return;

            Cell cell = _cells[row, column];
            if (cell.CellVisibility == CellVisibility.Revealed ||
                cell.CellVisibility == CellVisibility.Flagged) return;

            cell.Reveal();

            RevealedCellCount(cell.CellState == CellState.Mine);


            if (cell.AdjacentMineCount == 0 && cell.CellState != CellState.Mine)
            {
                int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

                for (int i = 0; i < 8; i++)
                {
                    Reveal(row + dr[i], column + dc[i]);
                }
            }
        }

        /// <summary>
        /// 開示済みのセル数をカウントする
        /// </summary>
        /// <param name="isMine"></param>
        public void RevealedCellCount(bool isMine)
        {
            // 三項演算子
            _revealedCellCount += isMine ? -1 : 1;
            Debug.Log($" RevealedCellCount{_revealedCellCount}");
        }

        /// <summary>
        /// 地雷を配置する
        /// 地雷をはいちする際、隣接するセルの地雷数をインクリメントする
        /// </summary>
        private void PlaceMines(int initialRow = -1, int initialColumn = -1)
        {
            int placedMines = 0;
            // セル数より大きい値は設定できないようにする
            _mineCount = _mineCount > _cells.Length ? _cells.Length : _mineCount;

            while (placedMines < _mineCount)
            {
                var r = Random.Range(0, _rows);
                var c = Random.Range(0, _columns);
                var cell = _cells[r, c];
                // 初期セルと隣接しているセルに地雷を配置しない
                if (Mathf.Abs(r - initialRow) <= 1 && Mathf.Abs(c - initialColumn) <= 1 ||
                    cell.CellState == CellState.Mine)
                {
                    Debug.Log("地雷配置再抽選");
                    continue;
                }

                if (_cells[r, c].CellState != CellState.Mine)
                {
                    _cells[r, c].CellState = CellState.Mine;
                    placedMines++;
                    IncrementAdjacentCells(r, c);
                }
            }

            Debug.Log($"地雷配置完了{placedMines}");
        }


        /// <summary>
        /// 隣接するセルの地雷数をインクリメントする
        /// </summary>
        /// <param name="mineRow"></param>
        /// <param name="mineColumn"></param>
        private void IncrementAdjacentCells(int mineRow, int mineColumn)
        {
            /*
             dr/dc     -1       0      +1
               -1   (-1,-1)  (-1,0)  (-1,+1)
               0    (0, -1)  (0,0)   (0, +1)
               +1   (+1,-1)  (+1,0)  (+1,+1)
             */
            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int nr = mineRow + dr[i];
                int nc = mineColumn + dc[i];

                if (nr >= 0 && nr < _rows && nc >= 0 && nc < _columns && _cells[nr, nc].CellState != CellState.Mine)
                {
                    _cells[nr, nc].AdjacentMineCount++;
                }
            }
        }
    }
}