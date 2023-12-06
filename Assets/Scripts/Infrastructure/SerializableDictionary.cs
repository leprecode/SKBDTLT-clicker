using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }
}