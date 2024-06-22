using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using Random = UnityEngine.Random;

public class LightsOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _rows = 5; //行
    [SerializeField] private int _columns = 5; //列
    [SerializeField] private Text _countText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _winText;
    [SerializeField] private bool _isRandomize = true;
    private int _moveCount = 0;
    private readonly List<Cell> _cells = new List<Cell>();
    private GridLayoutGroup _gridLayoutGroup;
    private IDisposable _timerDisposable;
    private bool _isGameRunning;
    private float _currentTime;
    // セルの情報を格納するクラス

    class Cell
    {
        public int Row;
        public int Column;
        public bool IsOn;
        public GameObject CellObject;
    }

    private void Start()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        if (_gridLayoutGroup != null)
        {
            _gridLayoutGroup.constraintCount = _columns;
        }

        Initialize();
        _isGameRunning = true;
        // _countが増えたら
        this.ObserveEveryValueChanged(_ => _moveCount)
            .Subscribe(_ => _countText.text = $"Count: {_moveCount}");
        _currentTime = 0;
        _timerDisposable = Observable.EveryUpdate()
            .Where(_ => _isGameRunning)
            .Subscribe(_ =>
            {
                _currentTime += Time.deltaTime;
                _timeText.text = $"Time: {_currentTime:F2}";
            })
            .AddTo(this);
    }

    /// <summary>
    /// ゲームを初期化する
    /// </summary>
    void Initialize()
    {
        System.Random rand = new System.Random();

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = new Cell
                {
                    Row = r,
                    Column = c,
                    IsOn = !_isRandomize || rand.Next(0, 2) == 0,
                    CellObject = new GameObject($"Cell({r}, {c})")
                };

                cell.CellObject.transform.parent = transform;
                var image = cell.CellObject.AddComponent<Image>();
                image.color = cell.IsOn ? Color.white : Color.black;

                _cells.Add(cell);
            }
        }

        if (_isRandomize)
        {
            // 全てのセルが同じ色にならないようにする
            EnsureNotAllSameColor();
        }
    }

    /// <summary>
    /// セルがクリックされたときに呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var clickedCellObject = eventData.pointerCurrentRaycast.gameObject;
        var clickedCell = _cells.Find(cell => cell.CellObject == clickedCellObject); // クリックされたセルを取得する

        if (clickedCell != null)
        {
            _moveCount++;
            ToggleCell(clickedCell);
            ToggleAdjacentCells(clickedCell);
            CheckForWin();
        }
        else
        {
            Debug.LogWarning("Cell not found.");
        }
    }

    /// <summary>
    /// セルを反転する
    /// </summary>
    /// <param name="cell"></param>
    void ToggleCell(Cell cell)
    {
        cell.IsOn = !cell.IsOn;
        var image = cell.CellObject.GetComponent<Image>(); // セルのImageを取得して、色を変更する
        image.color = cell.IsOn ? Color.white : Color.black;
    }

    /// <summary>
    /// 隣接するセルを反転する
    /// </summary>
    /// <param name="cell"></param>
    void ToggleAdjacentCells(Cell cell)
    {
        // 上下左右のセルを取得して、それぞれのセルを反転する
        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };
        for (int i = 0; i < 4; i++) //上下左右
        {
            int newRow = cell.Row + dr[i];
            int newCol = cell.Column + dc[i];

            // セルが存在するかどうかを確認して、存在する場合はセルを反転する
            if (newRow >= 0 && newRow < _rows && newCol >= 0 && newCol < _columns)
            {
                var adjacentCell = _cells.Find(c => c.Row == newRow && c.Column == newCol);
                if (adjacentCell != null)
                {
                    ToggleCell(adjacentCell);
                }
            }
        }
    }


    /// <summary>
    /// 全てのセルが同じ色にならないようにする
    /// </summary>
    void EnsureNotAllSameColor()
    {
        int switchCount;
        
        // 初期化時に全てのセルをオフ（クリア状態）に設定
        foreach (var cell in _cells)
        {
            cell.IsOn = false;
            var image = cell.CellObject.GetComponent<Image>();
            image.color = Color.black;
        }

        do
        {
            switchCount = 0;

            // 全てのセルをオフにした後にランダムにスイッチ
            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _columns; c++)
                {
                    if (Random.value > 0.1f)
                    {
                        continue;
                    }

                    // クリア済みの状態からボタンを押していく
                    var cell = _cells.Find(cell => cell.Row == r && cell.Column == c);
                    ToggleCell(cell);
                    ToggleAdjacentCells(cell);
                    switchCount++;
                    Debug.Log($"Switch: r={r}, c={c}", cell.CellObject);
                }
            }

            // 少なくとも 2 セル以上がスイッチされることを保証
            if (switchCount < 4)
            {
                Debug.Log("Not enough switches, reinitializing...");
            }
        } while (switchCount < 4 || IsAllCellsSameColor());
    }

    /// <summary>
    /// 全てのセルが同じ色かどうかをチェックするメソッド
    /// </summary>
    private bool IsAllCellsSameColor()
    {
        bool firstCellColor = _cells[0].IsOn;
        return _cells.All(cell => cell.IsOn == firstCellColor);
    }

    /// <summary>
    /// 勝利条件をチェックする
    /// </summary>
    void CheckForWin()
    {
        bool allOff = _cells.All(cell => !cell.IsOn);

        if (!allOff) return;
        _isGameRunning = false;
        if (_timerDisposable != null)
        {
            _timerDisposable.Dispose();
        }

        _winText.text = $"Congratulations! You won in {_moveCount} moves and {_currentTime:F2} seconds.";

        Debug.Log($"Congratulations! You won in {_moveCount} moves and {_currentTime:F2} seconds.");
    }
}