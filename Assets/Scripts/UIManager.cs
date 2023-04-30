using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumLibrary;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject _emergencyText;
    private GameObject _gameOverPanel;
    private GameObject[] _unseenItems;
    private GameObject instructions;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartScreen") 
        {
            instructions = GameObject.Find("Instructions");
            CloseControlsPopUp();
        }
    }

    public void AtStartGameScene()
    {
        _emergencyText = GameObject.Find("WarningText");
        _gameOverPanel = GameObject.Find("GameOver");
        _unseenItems = new GameObject[] {GameObject.Find("PunchIcon"),GameObject.Find("KeyIcon").transform.GetChild(0).gameObject,GameObject.Find("GuitarIcon")};
        HideEmergencyText();
        HideGameOverPanel();
    }
    public void ShowEmergencyText(string text)
    {
        _emergencyText.SetActive(true);
        _emergencyText.GetComponent<TMP_Text>().text= text;
    }

    public void HideEmergencyText()
    {
        _emergencyText.SetActive(false);
    }

    public void ShowGameOverText(string text)
    {
        _gameOverPanel.SetActive(true);
        _gameOverPanel.transform.Find("EndGameText").GetComponent<TMP_Text>().text= text;
    }
    public void HideGameOverPanel()
    {
        _gameOverPanel.SetActive(false);
    }

    public void OnClickLoadGameScreen()
    {
        GameManager.Instance.LoadScene(Escenas.GameScreen);
    }

    public void OnClickLoadStartScreen()
    {
        GameManager.Instance.LoadScene(Escenas.StartScreen);
    }

    public void ShowColectedItem(ItemData itemData)
    {
        foreach (var item in _unseenItems)
        {
            if(item.GetComponent<IconData>().Id==itemData.IconId)
            {
               item.GetComponent<Renderer>().material = new Material(item.GetComponent<IconData>().seenMaterial);
            }
        }
    }

    public void OpenControlsPopUp()
    {
        instructions.SetActive(true);
    }

    public void CloseControlsPopUp()
    {
        instructions.SetActive(false);
    }
}
