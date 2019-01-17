using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int x = 0;
    public int y = 0;

    private int _num;

    public int num
    {
        get { return _num; }
        set { _num = value; text.text = value.ToString(); }
    }

    private Text text;

    public void Awake()
    {
        text = GetComponentInChildren<Text>();

        num = 2;
    }

    [Conditional("UNITY_EDITOR")]
    public void RefreshName()
    {
        gameObject.name = string.Format("Tile ({0}, {1}", x, y);
    }
}
