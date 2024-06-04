using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupsController : MonoBehaviour
{
    [SerializeField] GameObject PopupWin;
    [SerializeField] GameObject PopupLose;
    [SerializeField] Button homeWin;
    [SerializeField] Button replayWin;
    [SerializeField] Button next;
    [SerializeField] Button homeLose;
    [SerializeField] Button replayLose;
	private void Awake()
	{
		homeWin.onClick.AddListener(LoadHome);
		replayWin.onClick.AddListener(LoadGame);
		next.onClick.AddListener(Next);

		homeLose.onClick.AddListener(LoadHome);
		replayLose.onClick.AddListener(LoadGame);
	}
	public void Win()
	{
		
		this.gameObject.SetActive(true);
        PopupWin.SetActive(true);
        PopupLose.SetActive(false);
       
    }
    public void Lose()
    {
		
		this.gameObject.SetActive(true);
		PopupLose.SetActive(true);
        PopupWin.SetActive(false);
		
	}
	private void Next()
	{
         int level= PlayerPrefs.GetInt("level");
		PlayerPrefs.SetInt("level", level+1);
		SceneManager.LoadScene(1);
	}

	private void LoadGame()
	{
		SceneManager.LoadScene(1);
	}

	private void LoadHome()
	{
		SceneManager.LoadScene(0);
	}
}
