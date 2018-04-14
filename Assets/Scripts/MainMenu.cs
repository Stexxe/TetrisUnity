using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button RestartButton;
    public Button QuitButton;
    public GameObject MenuPanel;

    void Start() {
        RestartButton.GetComponent<Button>().onClick.AddListener(RestartClick);
        QuitButton.GetComponent<Button>().onClick.AddListener(QuitClick);
    }

    void RestartClick() {
        MenuPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitClick() {
        Application.Quit();
    }
}
