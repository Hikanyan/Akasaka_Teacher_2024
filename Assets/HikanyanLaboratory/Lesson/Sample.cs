using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HikanyanLaboratory.Lesson001
{
    public class Sample : MonoBehaviour
    {
        // 問題：
        private void Start()
        {
            string str = "Hello, World!";
            HikanyanString hikanyanString = new HikanyanString(str);
            foreach (var test in hikanyanString)
            {
                Debug.Log(test);
            }
        }

        public class HikanyanString : IEnumerable<char>
        {
            private readonly char[] _characters;

            public HikanyanString(string initialString)
            {
                _characters = initialString.ToCharArray();
            }

            public IEnumerator<char> GetEnumerator()
            {
                foreach (var t in _characters)
                {
                    yield return t;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return new string(_characters);
            }
        }
    }
}