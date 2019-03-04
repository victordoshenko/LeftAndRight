using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameMode1_Manager : MonoBehaviour
{
    public AudioClip AudioLeftRusClip;
    public AudioClip AudioRightRusClip;
    public AudioClip AudioLeftEngClip;
    public AudioClip AudioRightEngClip;
    public AudioClip AudioBeepClip;
    public AudioSource AudioSource;
    public GameObject StripsRightObject;
    private SpriteRenderer StripsRightSpriteRenderer;
    private Text commandText;

    public Text timerText;
    public Text scoresText;
    public Text errorsText;
    public Text speedText;
    public Text gameoverText;
    public Text orientationText;
    public Text rmsText;
    public Text partialResultsText;
    public Text errorText;
    public Text RestartText;
    public Text MenuText;
    public Text SoundText;
    public Text SoundButtonText;
    public Text SayLeftOrRightText;

    public static float startTime = 0;
    public static float TimeStart = 0;

    public static int PressButton = 0;
    public static int scores = 0;
    public static int errors = 0;
    public static double speed = 5;
    public static double minSpeed = 0.7;
    public static int GameOver = 0;
    public static int Win = 0;
    private bool Sound = true;
    public static AndroidJavaClass pluginClass;

    public GameObject RestartButton;
    public GameObject MenuButton;
    public GameObject commandTextObject;
    public GameObject LeftButton;
    public GameObject RightButton;
    public Sprite ImageLeftButton0;
    public Sprite ImageRightButton0;
    public GameObject SliderObject;
    private static string LeftText;
    private static string RightText;
    private static string TextGameOver;
    private static string TextSoundOn;
    private static string TextSoundOff;
    private static AudioClip AudioLeftClip;
    private static AudioClip AudioRightClip;
    public GameObject SoundButton;

    public void MyRestart()
    {
        startTime = Time.time;
        TimeStart = Time.time;
        errorsText.text = "0";
        scoresText.text = "0";
        speed = 5;
        minSpeed = 0.7;
        if (Settings_Manager.GameMode == 4)
        {
            minSpeed += 1.3;
        }
        speedText.text = speed.ToString("f2");
        gameoverText.text = "";
        PressButton = 0;
        scores = 0;
        errors = 0;
        GameOver = 0;
        Win = 0;
        RestartButton.SetActive(false);
        MenuButton.SetActive(false);
        DropTheCoin();        
    }

    // Use this for initialization
    void Start()
    {
        if (Settings_Manager.Language == SystemLanguage.Russian)
        {
            LeftText = "Лево";
            RightText = "Право";
            AudioLeftClip = AudioLeftRusClip;
            AudioRightClip = AudioRightRusClip;
            TextGameOver = "Игра закончена!";
            RestartText.text = "Заново";
            MenuText.text = "Меню";
            TextSoundOn = "Звук Вкл";
            TextSoundOff = "Звук Выкл";
            SayLeftOrRightText.text = "Скажите 'Лево' или 'Право'";
        }
        else
        {
            LeftText = "Left";
            RightText = "Right";
            AudioLeftClip = AudioLeftEngClip;
            AudioRightClip = AudioRightEngClip;
            TextGameOver = "Game Over!";
            RestartText.text = "Restart";
            MenuText.text = "Menu";
            TextSoundOn = "Sound On";
            TextSoundOff = "Sound Off";
            SayLeftOrRightText.text = "Say 'Left' or 'Right'";
        }
        SoundButtonText.text = Sound ? TextSoundOff : TextSoundOn;
        orientationText.text = "";
        rmsText.text = "";
        partialResultsText.text = "";
        errorText.text = "";
        if (Settings_Manager.GameMode != 4)
            SayLeftOrRightText.text = "";
        StripsRightSpriteRenderer = StripsRightObject.GetComponent<SpriteRenderer>();
        commandText = commandTextObject.GetComponent<Text>();
        switch (Settings_Manager.GameMode)
        {
            case 2:
                StripsRightObject.SetActive(true);
                commandTextObject.SetActive(false);
                LeftButton.GetComponent<Image>().sprite = ImageLeftButton0;
                LeftButton.GetComponentInChildren<Text>().text = LeftText;
                RightButton.GetComponent<Image>().sprite = ImageRightButton0;
                RightButton.GetComponentInChildren<Text>().text = RightText;
                SliderObject.SetActive(false);
                break;
            case 3:
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                StripsRightObject.SetActive(false);
                break;
            case 4:
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                SoundButton.SetActive(false);
                SliderObject.SetActive(false);
                commandTextObject.SetActive(false);
                pluginClass = new AndroidJavaClass("com.plugin.speech.pluginlibrary.TestPlugin");
                pluginClass.CallStatic("setReturnObject", "GameObject");
                pluginClass.CallStatic("setLanguage", Settings_Manager.Language == SystemLanguage.Russian ? "ru_RU" : "en-US");
                pluginClass.CallStatic("PrepareRecording");
                //pluginClass.CallStatic("set_MLM", "500");
                //pluginClass.CallStatic("set_CSLM", "500");
                //pluginClass.CallStatic("set_PCSLM", "500");
                break;
            default:
                StripsRightObject.SetActive(false);
                commandTextObject.SetActive(true);
                SliderObject.SetActive(false);
                break;
        }
        MyRestart();
        //InitGyro();
    }

    public void DropTheCoin()
    {
        int r = UnityEngine.Random.Range(0, 2);
        string s = r == 0 ? LeftText : RightText;
        StripsRightSpriteRenderer.flipX = r == 0 ? true : false;
        if (Sound)
        {
            if (Settings_Manager.GameMode == 2 || Settings_Manager.GameMode == 4)
                AudioSource.clip = AudioBeepClip;
            else
                AudioSource.clip = r == 0 ? (AudioLeftClip) : AudioRightClip;
            AudioSource.Play();
        }
        commandText.text = s;
        if (Settings_Manager.GameMode == 4)
        {
            pluginClass.CallStatic("RestartRecording");
            pluginClass.CallStatic("setLanguage", Settings_Manager.Language == SystemLanguage.Russian ? "ru_RU" : "en-US");
            //pluginClass.CallStatic("PrepareRecording");
        }
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
        //Application.Quit();

        if (Settings_Manager.GameMode == 3)
        {
            float iax = Input.acceleration.x;
            float x = iax < -0.4f ? -0.4f : (iax > 0.4f ? 0.4f : iax);
            if (x == -0.4f && PressButton == 0)
                PressButton = 1;
            if (x == 0.4f && PressButton == 0)
                PressButton = 2;
            if (Mathf.Abs(x) < 0.2f && PressButton == 3)
            {
                PressButton = 0;
                DropTheCoin();
            }
            SliderObject.GetComponent<Slider>().value = (x + 0.4f) * 1.25f;
            if (PressButton == 3)
                startTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            PressButton = 2;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            PressButton = 1;
        }

        if (GameOver == 3)
        {
            return;
        }

        if (GameOver == 1)
        {
            if (Win == 1)
            {
                SceneManager.LoadScene("Salut");
            }
            else
            {
                gameoverText.text = TextGameOver;
                RestartButton.SetActive(true);
                MenuButton.SetActive(true);
            }
            GameOver = 3;
            return;
        }
        if (GameOver == 2)
        {
            MyRestart();
        }
        float t = Mathf.Round((Time.time - startTime) * 100f) / 100f;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;

        if (t > speed)
        {
            if (Settings_Manager.GameMode != 4 || PressButton > 0)
              errors++;
            errorsText.text = errors.ToString();
            Debug.Log("t=" + t.ToString() + " speed=" + speed.ToString());

            if (errors > 9)
                GameOver = 1;
            else
                DropTheCoin();
            startTime = Time.time;
        }
        else
        if (PressButton > 0)
        {
            if ((PressButton == 1 && commandText.text == LeftText) || (PressButton == 2 && commandText.text == RightText))
            {
                scores++;
                scoresText.text = scores.ToString();
                if (scores >= 20 && Settings_Manager.GameMode == 4)
                    GameOver = 1;
                else
                if (speed > minSpeed)
                {
                    speed = speed - 0.1;
                    if (speed <= minSpeed)
                        GameOver = 1;
                    else
                        speedText.text = speed.ToString("f2");
                }
            }
            else if (PressButton == 1 || PressButton == 2)
            {
                errors++;
                errorsText.text = errors.ToString();
                Debug.Log("PressButton=" + PressButton.ToString() + " text=" + commandText.text);
                if (((errors > 9 || speed <= minSpeed) && Settings_Manager.GameMode != 4)
                    ||
                   (errors > 10 && Settings_Manager.GameMode == 4))
                    GameOver = 1;
            }

            if (Settings_Manager.GameMode != 3)
            {
                PressButton = 0;
            } else if (PressButton == 1 || PressButton == 2)
            {
                PressButton = 3;
            }
            if (PressButton != 3)
                DropTheCoin();
        }

        if (GameOver == 1 && (double)errors / (double)scores <= 0.1 * (Settings_Manager.GameMode == 4 ? 5 : 1))
        {
            Win = 1;
            Debug.Log("Win! ");
        }            
    }

    public void ButtonLeftPress()
    {
        Debug.Log("Left!");
        PressButton = 1;
    }

    public void ButtonRightPress()
    {
        Debug.Log("Right!");
        PressButton = 2;
    }

    public void ButtonRestartPress()
    {
        Debug.Log("Restart!");
        GameOver = 2;
    }

    public void DoDown()
    {
        Debug.Log("Down!");
        commandText.color = Color.gray;
    }
    public void DoUp()
    {
        Debug.Log("Up!");
        commandText.color = Color.yellow;
    }

    public void ButtonMenuPress()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ButtonSoundPress()
    {
        Debug.Log("Sound!");
        Sound = !Sound;
        SoundButtonText.text = Sound ? TextSoundOff : TextSoundOn;
    }

    void onRmsChanged(string myText)
    {
        rmsText.text = myText;
    }

    void onError(string myText)
    {
        //errorText.text = myText;
        pluginClass.CallStatic("RestartRecording");
        pluginClass.CallStatic("setLanguage", Settings_Manager.Language == SystemLanguage.Russian ? "ru_RU" : "en-US");
    }

    void onPartialResults(string myText)
    {
        int d = Settings_Manager.Language == SystemLanguage.Russian ? GetDir(myText, "прав трав", "лев лег") : GetDir(myText, "right great bright prate crate Bridge krait", "left Lyft list thrift Clift");
        partialResultsText.text = myText;
        switch (d)
        {
            case 1:
                PressButton = 2;
                break;
            case 2:
                PressButton = 1;
                break;
            default:
                break;
        }
        /*
        if (d > 0)
        {
            pluginClass.CallStatic("RestartRecording");
            pluginClass.CallStatic("setLanguage", Settings_Manager.Language == SystemLanguage.Russian ? "ru_RU" : "en-US");
        }
        */
    }

    public int GetDir(string s, string r_, string l_)
    {
        // Get Direction from input s and masked templates r_, l_
        // Return: 1 = Right  2 = Left  0 = N/A
        s = s.ToUpper();
        r_ = r_.ToUpper();
        l_ = l_.ToUpper();
        var r = r_.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var l = l_.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        int ri = -1;
        foreach (string rs in r)
        {
            int n = s.IndexOf(rs, 0);
            if (n >= 0)
                if ((ri == -1) || (ri >= 0 && n < ri))
                    ri = n;
        }
        int li = -1;
        foreach (string ls in l)
        {
            int n = s.IndexOf(ls, 0);
            if (n >= 0)
                if ((li == -1) || (li >= 0 && n < li))
                    li = n;
        }

        int g = 0;
        if (ri >= 0 && (ri < li || li == -1))
            g = 1;
        else
        if (li >= 0 && (li < ri || ri == -1))
            g = 2;

        return g;
    }
}
