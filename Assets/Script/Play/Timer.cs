using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public const int START_TIME = 45;
    public float timeCount;
	public bool isEnd;
	public bool called ;
	public void StartCount()
	{
		timeCount = START_TIME;
		isEnd = false;
		called = false;
	}
	private void Update()
	{
		if (called) return;
		if(timeCount > 0)
		{
			timeCount-=Time.deltaTime;
			
		}
		else
		{
			isEnd = true;
			called = true;
		}
	}
}
