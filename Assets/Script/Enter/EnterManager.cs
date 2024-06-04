using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterManager : MonoBehaviour
{
    [SerializeField] private GameObject EnterObject;
    [SerializeField] private GameObject HtpObject;
    [SerializeField] private GameObject SelectObject;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button tutoBtn;
    [SerializeField] private Button closeHtpBtn;
    [SerializeField] private Button closeSelectBtn;
	// Start is called before the first frame update
	private void Awake()
	{
        playBtn.onClick.AddListener(ShowLevel);
        tutoBtn.onClick.AddListener(ShowHtp);
        closeHtpBtn.onClick.AddListener(ShowEnter);
		closeSelectBtn.onClick.AddListener(ShowEnter);
	}
	void Start()
    {
        ShowEnter();
    }
    public void ShowHtp()
    {
        DisableAll();
        HtpObject.SetActive(true);
    }
    public void ShowEnter()
    {
		DisableAll();
		EnterObject.SetActive(true);
	}
	public void ShowLevel()
    {
        DisableAll();
        SelectObject.SetActive(true);
        //Set list level ???
    }
    public void DisableAll()
    {
        EnterObject.SetActive(false);
        HtpObject.SetActive(false);
        SelectObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
