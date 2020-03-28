using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_Manager : MonoBehaviour {

    public static int GameMode = 1;
    public static SystemLanguage Language = SystemLanguage.English;
    public Toggle ToggleNormal;
    public Toggle ToggleInverse;
    public Toggle ToggleAccelerometer;
    public Toggle ToggleVoice;
    public Toggle ToggleRussian;
    public Toggle ToggleEnglish;
    public Text GameModeText;
    public Text NormalText;
    public Text InverseText;
    public Text AccelerometerText;
    public Text VoiceControlText;
    public Text LanguageText;
    public Text EnglishText;
    public Text RussianText;
    public Text ReturnText;

    private bool ToggleProtect = true;

    void Start()
    {
        //if (GameMode == 0) GameMode = 4;
        if (Settings_Manager.Language == SystemLanguage.Russian)
        {
            GameModeText.text = "Режим игры";
            NormalText.text = "Нормальный";
            InverseText.text = "Инверсный";
            AccelerometerText.text = "Акселерометр";
            VoiceControlText.text = "Управление голосом";
            LanguageText.text = "Язык";
            EnglishText.text = "Английский";
            RussianText.text = "Русский";
            ReturnText.text = "Назад";
        }
        else
        {
            GameModeText.text = "Game Mode";
            NormalText.text = "Normal";
            InverseText.text = "Inverse";
            AccelerometerText.text = "Accelerometer";
            VoiceControlText.text = "Voice Control";
            LanguageText.text = "Language";
            EnglishText.text = "English";
            RussianText.text = "Russian";
            ReturnText.text = "Return";
        }

        //Debug.Log("GameMode = " + GameMode.ToString());
        ToggleProtect = true;
        switch (GameMode) //PlayerPrefs.GetInt("GameMode"))
        {
            case 1:
                ToggleNormal.isOn = true;
                break;
            case 2:
                ToggleInverse.isOn = true;
                break;
            case 3:
                ToggleAccelerometer.isOn = true;
                break;
            case 4:
                ToggleVoice.isOn = true;
                break;
            default:
                ToggleNormal.isOn = true;
                break;
        }

        switch (Language)
        {
            case SystemLanguage.Russian:
                ToggleRussian.isOn = true;
                break;
            default:
                ToggleEnglish.isOn = true;
                break;
        }
        ToggleProtect = false;

    }

    public void ButtonReturn_Click()
    {
        ToggleProtect = true;
        SceneManager.LoadScene("Menu");
    }

    public void ToggleNormal_Click()
    {
        if (!ToggleProtect)
        if (ToggleNormal.isOn) {
            GameMode = 1;
            //PlayerPrefs.SetInt("GameMode", 1);
            Debug.Log("GameMode := 1");
        }
    }

    public void ToggleInverse_Click()
    {
        if (!ToggleProtect)
        if (ToggleInverse.isOn)
        {
            GameMode = 2;
            //PlayerPrefs.SetInt("GameMode", 2);
            Debug.Log("GameMode := 2");
        }
    }

    public void ToggleGyroscope_Click()
    {
        if (!ToggleProtect)
        if (ToggleAccelerometer.isOn)
        {
            GameMode = 3;
            //PlayerPrefs.SetInt("GameMode", 3);
            Debug.Log("GameMode := 3");
        }
    }

    public void ToggleVoiceControl_Click()
    {
        if (!ToggleProtect)
        if (ToggleVoice.isOn)
        {
            GameMode = 4;
            //PlayerPrefs.SetInt("GameMode", 4);
            Debug.Log("GameMode := 4");
        }
    }

    public void ToggleEnglish_Click()
    {
        if (!ToggleProtect)
            Language = SystemLanguage.English;
    }

    public void ToggleRussian_Click()
    {
        if (!ToggleProtect)
            Language = SystemLanguage.Russian;
    }

}
