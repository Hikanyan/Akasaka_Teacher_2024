using UnityEngine;

namespace HikanyanLaboratory.Task.Othello
{
    // Register Factory 
    public class ObjectGenerator
    {
        private readonly GameObject _prefab;

        public ObjectGenerator(GameObject prefab)
        {
            this._prefab = prefab;
        }

        public GameObject Generate()
        {
            return Object.Instantiate(_prefab);
        }
    }
}