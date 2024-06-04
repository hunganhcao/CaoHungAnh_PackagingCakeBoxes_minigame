using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
	public Vector2Int Pos;
	public int Value ;
	public void SetPos(int x, int y)
	{
		Pos.x = x; Pos.y = y;
	}
	public void SetValue(int val)
	{
		Value = val;
	}
}
