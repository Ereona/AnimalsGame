using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager
{
    private List<int> _indices = new List<int>();
    private int _count;

    public int CurrentIndex
    {
        get;
        private set;
    }

    public void Init(int count)
    {
        if (count <= 0)
        {
            throw new System.ArgumentException();
        }
        _count = count;
        bool loaded = Load();
        if (!loaded)
        {
            InitList();
            GoToNextIndex();
        }
    }

    public void GoToNextIndex()
    {
        if (_indices.Count == 0)
        {
            InitList();
        }
        int rand = Random.Range(0, _indices.Count);
        CurrentIndex = _indices[rand];
        _indices.RemoveAt(rand);
        Save();
    }

    private void InitList()
    {
        _indices.Clear();
        for (int i = 0; i < _count; i++)
        {
            _indices.Add(i);
        }
    }

    private bool Load()
    {
        int index = IniWorker.ReadIntValue("CurrentIndex", -1);
        if (index == -1)
        {
            return false;
        }
        string indicesStr = IniWorker.ReadStringValue("Indices", null);
        if (indicesStr == null)
        {
            return false;
        }
        string[] indices = indicesStr.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries);
        _indices.Clear();
        foreach (string i in indices)
        {
            _indices.Add(int.Parse(i));
        }
        CurrentIndex = index;
        return true;
    }

    private void Save()
    {
        IniWorker.WriteIntValue("CurrentIndex", CurrentIndex);
        string indicesStr = string.Join(" ", _indices);
        IniWorker.WriteStringValue("Indices", indicesStr);
    }
}
