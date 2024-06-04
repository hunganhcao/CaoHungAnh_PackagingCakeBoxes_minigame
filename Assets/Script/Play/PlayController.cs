
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayController : MonoBehaviour
{
    [SerializeField] private PlayUI ui;
    [SerializeField] private ItemGrid prefabItem;
    [SerializeField] private GridLayoutGroup table;
	[SerializeField] private ItemMove cake;
	[SerializeField] private ItemMove gift;
	[SerializeField] private Timer timer;
	private int level=0;
	private int[,] grid;
	private List<List<ItemGrid>> listItemGrid =new();

	private bool isEnd = false;
	//Object Level
	List<int[,]> levels = new List<int[,]> {
	new int[,] {
		{ 0, 0, 1 },
		{ 2, 0, 3 },
		{ 1, 0, 0 }
	},
	new int[,] {
		{ 1, 3, 0 },
		{ 0, 0, 1 },
		{ 1, 2, 0 }
	},
	new int[,] {
		{ 1, 0, 0 },
		{ 0, 0, 0 },
		{ 2, 1, 3 },
		{ 0, 0, 0 }
	},
	};
	private void Start()
	{
		isEnd = false;
		listItemGrid = new();
		SpawnLevel();
		timer.StartCount();

	}
	public void SpawnLevel()
	{
		if (PlayerPrefs.HasKey("level"))
		{
			level = PlayerPrefs.GetInt("level");
		}
		else
		{
			level = 1;
		}
		if (level > levels.Count)
		{
			grid = levels[0];
		}
		else
		grid = levels[level - 1];
		
		for (int i = 0; i < grid.GetLength(0); i++)
		{
			var listTemp = new List<ItemGrid>();
			for(int j=0;j< grid.GetLength(1); j++)
			{
				var item = Instantiate(prefabItem, table.transform);
				item.Init(grid[i,j],i,j);
				if (item.Value == 2)
				{
					cake.transform.SetParent(item.transform,false);
					cake.transform.localPosition = Vector2.zero;
					cake.SetPos(i,j);
					cake.SetValue(2);
					
				
				}
				if (item.Value == 3)
				{
					gift.transform.SetParent(item.transform, false);
					gift.transform.localPosition = Vector2.zero;
					gift.SetPos(i, j);
					gift.SetValue(3);	
				}
				listTemp.Add(item);
			}
			listItemGrid.Add(listTemp);
		}
		 
	}
	void Update()
	{
		if(isEnd) return;
		if (timer.isEnd) {
			ui.Lose();
			isEnd = true;
			return;
		}
		else if (timer.timeCount >= 0)
		{
			ui.SetTextTime(timer.timeCount);
		}
		
		if (SwipeManger.swipeUp)
		{
			MoveItems(Dir.Up);
		}
		if (SwipeManger.swipeDown) { MoveItems(Dir.Down); }
		if (SwipeManger.swipeLeft) { MoveItems(Dir.Left); }
		if (SwipeManger.swipeRight) { MoveItems(Dir.Right); }
		if(Input.GetKeyUp(KeyCode.A)) 
		{
			MoveItems(Dir.Left);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			MoveItems(Dir.Down);

		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			MoveItems(Dir.Right);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			MoveItems(Dir.Up);
		}
	}

	public void MoveItems(Dir direction)
	{
		Vector2Int moveVector = directionVectors[direction];
		bool canMove=true;

		
		if (SameLineOrColumn(cake, gift, direction))
		{
			
			ItemMove first, second;
			DetermineOrder(cake, gift, direction, out first, out second);
			do
			{
				canMove = CanMove(first, moveVector);
				if (canMove) MoveItem(first, moveVector);
			} while (canMove);
			canMove = true;
			do
			{
				canMove = CanMove(second, moveVector);
				if (canMove) MoveItem(second, moveVector);
			} while (canMove);
			canMove=true;
			if (direction == Dir.Up ||direction== Dir.Down)
			{
				if((first.Pos.x==second.Pos.x-1 && first.Value == 2)|| (first.Pos.x == second.Pos.x + 1 && first.Value == 3))
				{
					//Win
					Debug.Log("zo");
					canMove = CanMoveWin(first, moveVector);
					if (canMove) MoveItem(first, moveVector);
					canMove = CanMoveWin(second, moveVector);
					if (canMove) MoveItem(second, moveVector);

					isEnd = true;
					StartCoroutine(Win());
					
				}
			}
		}
		else
		{
			do
			{
				canMove = CanMove(cake, moveVector);
				if (canMove) MoveItem(cake, moveVector);
			} while (canMove);
			canMove = true;
			do
			{
				canMove = CanMove(gift, moveVector);
				if (canMove) MoveItem(gift, moveVector);
			} while (canMove);
		}
	}


	private Dictionary<Dir, Vector2Int> directionVectors = new Dictionary<Dir, Vector2Int>
{
	{ Dir.Left, new Vector2Int(0, -1) },
	{ Dir.Right, new Vector2Int(0, 1) },
	{ Dir.Up, new Vector2Int(-1, 0) },
	{ Dir.Down, new Vector2Int(1, 0) }
};


	private bool SameLineOrColumn(ItemMove item1, ItemMove item2, Dir direction)
	{
		if (direction == Dir.Left || direction == Dir.Right)
		{
			return item1.Pos.x == item2.Pos.x;
		}
		else
		{
			return item1.Pos.y == item2.Pos.y;
		}
	}


	private void DetermineOrder(ItemMove item1, ItemMove item2, Dir direction, out ItemMove first, out ItemMove second)
	{
		if (direction == Dir.Left || direction == Dir.Right)
		{
			if (item1.Pos.y < item2.Pos.y)
			{
				first = direction == Dir.Left ? item1 : item2;
				second = direction == Dir.Left ? item2 : item1;
			}
			else
			{
				first = direction == Dir.Left ? item2 : item1;
				second = direction == Dir.Left ? item1 : item2;
			}
		}
		else
		{
			if (item1.Pos.x < item2.Pos.x)
			{
				first = direction == Dir.Up ? item1 : item2;
				second = direction == Dir.Up ? item2 : item1;
			}
			else
			{
				first = direction == Dir.Up ? item2 : item1;
				second = direction == Dir.Up ? item1 : item2;
			}
		}
	}

	bool CanMoveWin(ItemMove item, Vector2Int moveVector)
	{
		int newX = item.Pos.x + moveVector.x;
		int newY = item.Pos.y + moveVector.y;

		if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1) || grid[newX, newY] == 1)
		{
			return false;
		}
		return true;
	}
	private bool CanMove(ItemMove item, Vector2Int moveVector)
	{
		int newX = item.Pos.x + moveVector.x;
		int newY = item.Pos.y + moveVector.y;

		if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1) || grid[newX, newY] != 0)
		{
			return false;
		}
		return true;
	}


	private void MoveItem(ItemMove item, Vector2Int moveVector)
	{
		int newX = item.Pos.x + moveVector.x;
		int newY = item.Pos.y + moveVector.y;

		grid[item.Pos.x, item.Pos.y] = 0;
		grid[newX, newY] = item.Value;

		
		item.SetPos(newX, newY);
		item.transform.SetParent(listItemGrid[newX][newY].transform);
		item.transform.localPosition = Vector2.zero;

	}
	IEnumerator Win()
	{
		yield return new WaitForSeconds(1.0f);
		ui.Win();
	}
}
public enum Dir
{
	Up,Down, Left, Right
}

