using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> LeftCountChanged;
    private int _leftCount;
    public int LeftCount
    {
        get
        {
            return _leftCount;
        }
        private set
        {
            _leftCount = value;
            IniWorker.WriteIntValue("LeftCount", _leftCount);
            LeftCountChanged?.Invoke(_leftCount);
        }
    }

    public event Action<int> RightCountChanged;
    private int _rightCount;
    public int RightCount
    {
        get
        {
            return _rightCount;
        }
        private set
        {
            _rightCount = value;
            IniWorker.WriteIntValue("RightCount", _rightCount);
            RightCountChanged?.Invoke(_rightCount);
        }
    }

    private void Start()
    {
        LeftCount = IniWorker.ReadIntValue("LeftCount", 0);
        RightCount = IniWorker.ReadIntValue("RightCount", 0);
    }

    public void LeftCountPlus()
    {
        LeftCount++;
    }

    public void RightCountPlus()
    {
        RightCount++;
    }

    public void ResetValues()
    {
        LeftCount = 0;
        RightCount = 0;
    }
}
