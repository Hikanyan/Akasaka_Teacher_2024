using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HikanyanLaboratory.Lesson.Minesweeper
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text _view = null;
        [SerializeField] private Image _secretImage = null;
        [SerializeField] private CellState _cellState = CellState.None;
        [SerializeField] private CellVisibility _cellVisibility = CellVisibility.Secret;


        public CellState CellState
        {
            get => _cellState;
            set
            {
                _cellState = value;
                OnCellStateChanged();
            }
        }

        public CellVisibility CellVisibility
        {
            get => _cellVisibility;
            set
            {
                _cellVisibility = value;
                OnCellStateChanged();
            }
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public int AdjacentMineCount { get; set; }

        public Minesweeper Minesweeper { get; set; }

        /// <summary>
        /// セルの初期化
        /// </summary>
        /// <param name="minesweeper"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void Initialize(Minesweeper minesweeper, int row, int column)
        {
            Minesweeper = minesweeper;
            Row = row;
            Column = column;
            CellState = CellState.None;
            _cellVisibility = CellVisibility.Secret;
            AdjacentMineCount = 0;
        }


        /// <summary>
        /// Unity エディター上で値が変更されたときに呼び出される
        /// セルの状態が変更されたときに呼び出される
        /// </summary>
        private void OnValidate()
        {
            OnCellStateChanged();
        }

        public void OnCellStateChanged()
        {
            if (_view == null || _secretImage == null)
            {
                return;
            }

            switch (_cellVisibility)
            {
                case CellVisibility.Secret:
                    _secretImage.enabled = true;
                    _view.text = "";
                    _view.color = Color.black;
                    break;
                case CellVisibility.Revealed:
                    _secretImage.enabled = false;
                    if (_cellState == CellState.Mine)
                    {
                        _view.text = "X";
                        _view.color = Color.red;
                        Minesweeper.GameOver();
                    }
                    else
                    {
                        _view.text = AdjacentMineCount > 0 ? AdjacentMineCount.ToString() : "";
                        _view.color = Color.black;
                    }

                    break;
                case CellVisibility.Flagged:
                    _secretImage.enabled = false;
                    _view.text = "F";
                    _view.color = Color.blue;
                    break;
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("OnPointerClick");
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnFirstClick();
                //Debug.Log("Reveal");
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                ToggleFlag();
                //Debug.Log("ToggleFlag");
            }
        }


        /// <summary>
        /// 最初のクリック時の処理
        /// </summary>
        private void OnFirstClick()
        {
            Minesweeper.RevealCell(this);
        }

        /// <summary>
        /// セルを開く
        /// </summary>
        public void Reveal()
        {
            if (CellVisibility == CellVisibility.Secret || CellVisibility == CellVisibility.Flagged)
            {
                CellVisibility = CellVisibility.Revealed;
                OnCellStateChanged();
            }
        }

        /// <summary>
        /// 旗を立てる/取り外す
        /// </summary>
        public void ToggleFlag()
        {
            if (CellVisibility == CellVisibility.Secret)
            {
                CellVisibility = CellVisibility.Flagged;
            }
            else if (CellVisibility == CellVisibility.Flagged)
            {
                CellVisibility = CellVisibility.Secret;
            }

            OnCellStateChanged();
        }
    }
}