using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour {
    public static bool isStarted;
    public Text NewGameButton;
    public Text SettingsButton;
    public Text LeftAndRight;
    public Text AboutButton;
    public Text VersionText;

    public void ButtonNewGame_Click()
    {
        if (Settings_Manager.GameMode < 5)
        {
            SceneManager.LoadScene("GameMode1");
        }
        else
        {
            SceneManager.LoadScene("SoundTest");
        }
    }
    public void ButtonSettings_Click()
    {
        SceneManager.LoadScene("Settings");
    }
    public void ButtonSoundTest_Click()
    {
        SceneManager.LoadScene("SoundTest");
    }

    void Start()
    {
        VersionText.text = "Version " + Application.version + ".0";

        if (!isStarted)
        {
            Settings_Manager.Language = Application.systemLanguage;
            isStarted = true;
        }

        if (Settings_Manager.Language == SystemLanguage.Russian)
        {
            NewGameButton.text = "Новая игра";
            SettingsButton.text = "Настройки";
            LeftAndRight.text = "Лево и Право";
            AboutButton.text = "Создатели";
        }
        else
        {
            NewGameButton.text = "New Game";
            SettingsButton.text = "Settings";
            LeftAndRight.text = "Left and Right";
            AboutButton.text = "About";
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void ButtonAbout_Click()
    {
        SceneManager.LoadScene("AboutScene");
    }
}
