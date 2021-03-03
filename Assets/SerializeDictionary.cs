using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializeDictionary<Key, Value> : MonoBehaviour
{

    [System.Serializable]
    public struct DictionaryInfo
    {
        public Key key;
        public Value value;
        // using Queue
        public uint size;
    }

    Dictionary<Key, Value> dictionary;

    public void Init(List<DictionaryInfo> list)
    {
        dictionary = new Dictionary<Key, Value>();
        foreach (var item in list)
        {
            dictionary.Add(item.key, item.value); 
        }
    }

    public void Add(Key key, Value value)
    {
        dictionary.Add(key, value);
    }

    public Value this[Key key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    public ref Dictionary<Key, Value> Get()
    {
        return ref dictionary;
    }

}

