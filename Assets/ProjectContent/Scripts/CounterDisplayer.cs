using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplayer : MonoBehaviour
{
    public Text LeftCounterText;
    public Text RightCounterText;

    void Start()
    {
        Counter counter = FindObjectOfType<Counter>();
        if (counter != null)
        {
            counter.LeftCountChanged += Counter_LeftCountChanged;
            counter.RightCountChanged += Counter_RightCountChanged;
            LeftCounterText.text = counter.LeftCount.ToString();
            RightCounterText.text = counter.RightCount.ToString();
        }
    }

    private void Counter_LeftCountChanged(int obj)
    {
        LeftCounterText.text = obj.ToString();
    }

    private void Counter_RightCountChanged(int obj)
    {
        RightCounterText.text = obj.ToString();
    }

}
