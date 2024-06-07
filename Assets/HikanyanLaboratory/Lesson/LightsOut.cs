using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;

public class LightsOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;
    [SerializeField] private Text _countText;
    private int _count = 0;
    private readonly List<Cell> _cells = new List<Cell>();

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
        Initialize();
        // _countが増えたら
        this.ObserveEveryValueChanged(_ => _count)
            .Subscribe(_ => _countText.text = $"Count: {_count}");
    }

    //初期化
    void Initialize()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = new Cell
                {
                    Row = r,
                    Column = c,
                    IsOn = true,
                    CellObject = new GameObject($"Cell({r}, {c})")
                };

                cell.CellObject.transform.parent = transform;
                var image = cell.CellObject.AddComponent<Image>();
                image.color = Color.white;

                _cells.Add(cell);
            }
        }
    }

    // セルがクリックされたときに呼び出される
    public void OnPointerClick(PointerEventData eventData)
    {
        var clickedCellObject = eventData.pointerCurrentRaycast.gameObject;
        var clickedCell = _cells.Find(cell => cell.CellObject == clickedCellObject);

        if (clickedCell != null)
        {
            _count++;
            ToggleCell(clickedCell);
            ToggleAdjacentCells(clickedCell);
        }
    }

    // セルを反転する
    void ToggleCell(Cell cell)
    {
        cell.IsOn = !cell.IsOn;
        var image = cell.CellObject.GetComponent<Image>();
        image.color = cell.IsOn ? Color.white : Color.black;
    }

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
}