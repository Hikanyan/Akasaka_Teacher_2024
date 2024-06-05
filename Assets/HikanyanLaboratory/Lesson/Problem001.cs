using System;
using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.Lesson001
{
    public class Problem001 : MonoBehaviour
    {
        [SerializeField] private int _count = 5; // Cell の数
        private Cell[] _cells; // Cell の配列
        private int _selectedIndex = 0; // 選択中の Cell のインデックス

        private void Start()
        {
            _cells = new Cell[_count];
            for (int i = 0; i < _count; i++)
            {
                GameObject obj = new GameObject($"Cell{i}");
                obj.transform.SetParent(transform);
                Image image = obj.AddComponent<Image>();
                image.color = i == 0 ? Color.red : Color.white;
                _cells[i] = new Cell(obj, image);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(-1);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DeleteSelected();
            }
        }

        /// <summary>
        /// 選択中の Cell を移動する
        /// </summary>
        /// <param name="step">移動するステップ</param>
        private void Move(int step)
        {
            if (_cells == null || _cells.Length == 0) return;
            _cells[_selectedIndex].Image.color = Color.white;
            _selectedIndex += step;
            if (_selectedIndex < 0) _selectedIndex = 0;
            else if (_selectedIndex >= _count) _selectedIndex = _count - 1;
            _cells[_selectedIndex].Image.color = Color.red;
        }

        /// <summary>
        /// 選択中の Cell を削除する
        /// </summary>
        private void DeleteSelected()
        {
            if (_cells == null || _cells.Length == 0) return;
            Destroy(_cells[_selectedIndex].GameObject);
            for (int i = _selectedIndex; i < _count - 1; i++)
            {
                _cells[i] = _cells[i + 1];
            }

            Array.Resize(ref _cells, _count - 1); // Resize は配列の要素数を変更するメソッド
            _count--;
            if (_selectedIndex >= _count) _selectedIndex = _count - 1;
            if (_count > 0) _cells[_selectedIndex].Image.color = Color.red;
        }

        /// <summary>
        /// Cell クラス
        /// </summary>
        private class Cell
        {
            public GameObject GameObject { get; }
            public Image Image { get; }

            public Cell(GameObject gameObject, Image image)
            {
                GameObject = gameObject;
                Image = image;
            }
        }
    }
}