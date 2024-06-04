using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemLevel : MonoBehaviour
{
    public int level;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button btn;
	private void OnEnable()
	{
		SetLevel(level);
	}
	public void SetLevel(int lv)
    {
        level = lv;
        levelText.text=level.ToString();

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(1);
    }
}
