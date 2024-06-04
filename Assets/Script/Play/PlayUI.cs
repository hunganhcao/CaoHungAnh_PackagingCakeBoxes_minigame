using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button replayBtn;
	[SerializeField] private PopupsController popupsController;
	private void Awake()
	{
		homeBtn.onClick.RemoveAllListeners();
		homeBtn.onClick.AddListener(LoadHome);
		replayBtn.onClick.RemoveAllListeners();
		replayBtn.onClick.AddListener(LoadGame);
	}
	public void Win()
	{
		popupsController.Win();
	}
	public void Lose()
	{
		popupsController.Lose();
	}
	private void LoadGame()
	{
		SceneManager.LoadScene(1);
	}

	private void LoadHome()
	{
		SceneManager.LoadScene(0);
	}
	public void SetTextTime(float time)
	{
		float minutes = Mathf.FloorToInt(time / 60);
		float seconds = Mathf.FloorToInt(time % 60);

		timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
