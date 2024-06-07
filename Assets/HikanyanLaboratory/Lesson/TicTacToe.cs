using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using System.Linq;

public class TicTacToe : MonoBehaviour
{
    [SerializeField] private Color _normalCell = Color.white;
    [SerializeField] private Color _selectedCell = Color.cyan;
    [SerializeField] private Sprite _circle = null;
    [SerializeField] private Sprite _cross = null;
    [SerializeField] private Player _startingPlayer = Player.Circle;
    private const int Size = 3;
    private Image[,] _cells;

    private int _selectedRow;
    private int _selectedColumn;

    private Player _currentPlayer;
    private bool _isPlayerTurn = true;
    private bool _isGameOver = false;

    private Subject<bool> _gameEndSubject = new Subject<bool>();

    enum Player
    {
        Circle,
        Cross
    }

    void Start()
    {
        _cells = new Image[Size, Size];
        _currentPlayer = _startingPlayer;
        InitializeBoard();

        // ゲームループを開始
        RunGameLoop().Forget();
        // ゲーム終了時の処理
        _gameEndSubject
            .Where(isGameOver => isGameOver)
            .Subscribe(_ => { Debug.Log("ゲーム終了"); });

        _isPlayerTurn = _startingPlayer == Player.Circle;
    }

    // ゲームループ
    async UniTaskVoid RunGameLoop()
    {
        while (!_isGameOver)
        {
            if (_isPlayerTurn)
            {
                await PlayerTurn();
            }
            else
            {
                await AITurn();
            }

            CheckGameOver();
        }
    }

    // プレイヤーのターン
    async UniTask PlayerTurn()
    {
        while (_isPlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateMarker();
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    // AIのターン
    async UniTask AITurn()
    {
        await UniTask.DelayFrame(30); // AIの考える時間をシミュレーション

        // 空いているセルを探して配置
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                if (_cells[r, c].sprite == null)
                {
                    _selectedRow = r;
                    _selectedColumn = c;
                    CreateMarker();
                    return;
                }
            }
        }
    }

    // ボードの初期化
    void InitializeBoard()
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r},{c})");
                obj.transform.parent = transform;
                var cell = obj.AddComponent<Image>();
                cell.color = _normalCell; // 初期色を設定
                _cells[r, c] = cell;
            }
        }
    }

    void Update()
    {
        if (_isPlayerTurn && _currentPlayer == Player.Circle)
        {
            HandlePlayerInput();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // セルに配置する
    void CreateMarker()
    {
        if (_cells[_selectedRow, _selectedColumn].sprite == null) // 空のセルにのみ配置
        {
            PlaceMarker();
            if (CheckWin())
            {
                Debug.Log($"{_currentPlayer} の勝ちです！");
                _isGameOver = true;
                _gameEndSubject.OnNext(true); // ゲーム終了を通知
            }
            else if (IsBoardFull())
            {
                Debug.Log("引き分けです！");
                _isGameOver = true;
                _gameEndSubject.OnNext(true); // ゲーム終了を通知
            }
            else
            {
                SwitchPlayer();
            }
        }
    }

    // プレイヤーの入力を処理
    void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _selectedColumn--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _selectedColumn++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _selectedRow--;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _selectedRow++;
        }

        _selectedColumn = Mathf.Clamp(_selectedColumn, 0, Size - 1);
        _selectedRow = Mathf.Clamp(_selectedRow, 0, Size - 1);

        UpdateCellColors();
    }

    // セルの色を更新
    void UpdateCellColors()
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = _cells[r, c];
                cell.color = (r == _selectedRow && c == _selectedColumn) ? _selectedCell : _normalCell;
            }
        }
    }

    // Playerのマーカーを配置
    void PlaceMarker()
    {
        var cell = _cells[_selectedRow, _selectedColumn];
        cell.sprite = _currentPlayer == Player.Circle ? _circle : _cross;
    }

    // プレイヤーを切り替え
    void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == Player.Circle ? Player.Cross : Player.Circle;
        _isPlayerTurn = !_isPlayerTurn;
    }

    // Gameの勝敗判定
    bool CheckWin()
    {
        return CheckRows() || CheckColumns() || CheckDiagonals();
    }

    // ボードが埋まったかどうか
    bool IsBoardFull()
    {
        return _cells.Cast<Image>().All(cell => cell.sprite != null);
    }

    // 横の勝敗判定
    bool CheckRows()
    {
        for (int r = 0; r < Size; r++)
        {
            if (_cells[r, 0].sprite != null && _cells[r, 0].sprite == _cells[r, 1].sprite &&
                _cells[r, 1].sprite == _cells[r, 2].sprite)
            {
                return true;
            }
        }

        return false;
    }

    // 縦の勝敗判定
    bool CheckColumns()
    {
        for (int c = 0; c < Size; c++)
        {
            if (_cells[0, c].sprite != null && _cells[0, c].sprite == _cells[1, c].sprite &&
                _cells[1, c].sprite == _cells[2, c].sprite)
            {
                return true;
            }
        }

        return false;
    }

    // 斜めの勝敗判定
    bool CheckDiagonals()
    {
        if (_cells[0, 0].sprite != null && _cells[0, 0].sprite == _cells[1, 1].sprite &&
            _cells[1, 1].sprite == _cells[2, 2].sprite)
        {
            return true;
        }

        if (_cells[0, 2].sprite != null && _cells[0, 2].sprite == _cells[1, 1].sprite &&
            _cells[1, 1].sprite == _cells[2, 0].sprite)
        {
            return true;
        }

        return false;
    }

    // ゲーム終了のチェック
    void CheckGameOver()
    {
        if (CheckWin() || IsBoardFull())
        {
            _isGameOver = true;
            _gameEndSubject.OnNext(true); // ゲーム終了を通知
        }
    }

    void RestartGame()
    {
        _currentPlayer = _startingPlayer;
        _isPlayerTurn = _startingPlayer == Player.Circle;
        _isGameOver = false;
        _selectedRow = 0;
        _selectedColumn = 0;

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                _cells[r, c].sprite = null;
                _cells[r, c].color = _normalCell; // 初期色にリセット
            }
        }

        RunGameLoop().Forget();
    }
}