using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class SerializableQueue<T>
    {
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        private List<T> _elements = new List<T>();
        public int Count => _elements.Count;

        public void Enqueue(T item)
        {
            _elements.Add(item);
        }


        public T Dequeue()
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = _elements[0];
            _elements.RemoveAt(0);
            return item;
        }
    }
}