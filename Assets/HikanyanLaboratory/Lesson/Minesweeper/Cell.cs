using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.Lesson.Minesweeper
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Text _view = null;

        [SerializeField] private CellState _cellState = CellState.None;

        public CellState CellState
        {
            get => _cellState;
            set
            {
                _cellState = value;
                OnCellStateChanged();
            }
        }

        private void OnValidate()
        {
            OnCellStateChanged();
        }

        private void OnCellStateChanged()
        {
            if (_view == null)
            {
                return;
            }

            if (_cellState == CellState.None)
            {
                _view.text = "";
            }
            else if (_cellState == CellState.Mine)
            {
                _view.text = "X";
                _view.color = Color.red;
            }
            else
            {
                _view.text = ((int)_cellState).ToString();
                _view.color = Color.blue;
            }
        }
    }
}