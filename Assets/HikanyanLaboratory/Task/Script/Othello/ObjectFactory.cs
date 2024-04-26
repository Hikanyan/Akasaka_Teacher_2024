using UnityEngine;

namespace HikanyanLaboratory.Task.Othello
{
    public class ObjectFactory
    {
        private readonly ObjectGenerator _generator;

        public ObjectFactory(ObjectGenerator generator)
        {
            _generator = generator;
        }

        public GameObject Create()
        {
            return _generator.Generate();
        }
    }
}