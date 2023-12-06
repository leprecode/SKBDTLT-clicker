using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class SerializableQueue<T>
    {
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        public List<T> elements = new List<T>();

        public void Enqueue(T item)
        {
            elements.Add(item);
        }

        public T Dequeue()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = elements[0];
            elements.RemoveAt(0);
            return item;
        }
    }
}