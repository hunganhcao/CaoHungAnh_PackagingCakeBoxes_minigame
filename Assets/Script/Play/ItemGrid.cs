using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public Vector2Int Pos;
    public int Value;
    [SerializeField] private GameObject Candy;
    public void Init(int val,int x, int y)
    {
        Value = val;
        Pos = new Vector2Int(x,y);
        if (Value == 1)
        {
            Candy.SetActive(true);
        }
        else
        {
            Candy.SetActive(false);
        }
    }
    public void SetValue (int val)
    {
        Value = val;
    }
}
