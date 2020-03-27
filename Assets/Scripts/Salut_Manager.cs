using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;

public class Salut_Manager : MonoBehaviour
{
    public Text ReturnText;
    public Text WinText;
    public Text WinTextDetails;
    public Text ShareText;

#if UNITY_IOS
    private string gameId = "3521728";
#elif UNITY_ANDROID
    private string gameId = "3521729";
#endif
    public string placementId = "video";
    bool testMode = false;

    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }

    public void ButtonShare_Click()
    {
        if (Settings_Manager.Language == SystemLanguage.Russian)
            StartCoroutine(TakeSSAndShare("Я знаю, где Лево и Право!", "Я знаю, где Лево и Право! А ты? :-) https://play.google.com/store/apps/details?id=com.Kusyandr.LeftAndRight"));
        else
            StartCoroutine(TakeSSAndShare("I Win in Left and Right!", "I Win in Left and Right! What about you? :-) https://play.google.com/store/apps/details?id=com.Kusyandr.LeftAndRight"));
    }

    public void ButtonReturn_Click()
    {
        ShowAd();        
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator TakeSSAndShare(string subjtxt, string bodytxt)
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shareLRimg.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject(subjtxt).SetText(bodytxt).Share();
    }

    void Start()
    {
        string[] RangEng = { "GURU", "Teacher", "Master", "Expert", "Competent", "Novice", "Baby"};
        string[] RangRus = { "ГУРУ", "Учитель", "Мастер", "Эксперт", "Компетентный", "Новичок", "Двоечник"};

        Monetization.Initialize(gameId, testMode);
        
        float t = Mathf.Round((Time.time - GameMode1_Manager.TimeStart) * 100f) / 100f;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        int Rang = GameMode1_Manager.errors + 4 - ((int)t / 15 > 4 ? 0 : 4 - (int)t / 15) * Settings_Manager.GameMode;
        if (Rang > 6)
            Rang = 6;
        if (Rang < 0)
            Rang = 0;

        if (Settings_Manager.Language == SystemLanguage.Russian)
        {
            ReturnText.text = "Назад";
            ShareText.text = "Поделиться";
            WinText.text = "Победа! :)";
            WinTextDetails.text = "Режим игры: ";
            switch (Settings_Manager.GameMode)
            {
                case 1:
                    WinTextDetails.text += "Нормальный\r\n";
                    break;
                case 2:
                    WinTextDetails.text += "Инверсный\r\n";
                    break;
                case 3:
                    WinTextDetails.text += "Акселерометр\r\n";
                    break;
                case 4:
                    WinTextDetails.text += "Голосовое управление\r\n";
                    break;
                default:
                    break;
            }
            WinTextDetails.text += "Правильных ответов: " + GameMode1_Manager.scores.ToString() + "\r\n";
            WinTextDetails.text += "Ошибок: " + GameMode1_Manager.errors.ToString() + "\r\n";
            WinTextDetails.text += "Время: " + (minutes != "0" ? minutes + " минут " : "") + seconds + " секунд\r\n";
            WinTextDetails.text += "Ранг: " + RangRus[Rang];
        }
        else
        {
            ReturnText.text = "Return";
            ShareText.text = "Share";
            WinText.text = "You Win! :)";
            WinTextDetails.text = "Game mode: ";
            switch (Settings_Manager.GameMode)
            {
                case 1:
                    WinTextDetails.text += "Normal\r\n";
                    break;
                case 2:
                    WinTextDetails.text += "Inverse\r\n";
                    break;
                case 3:
                    WinTextDetails.text += "Accelerometer\r\n";
                    break;
                case 4:
                    WinTextDetails.text += "Voice Control\r\n";
                    break;
                default:
                    break;
            }
            WinTextDetails.text += "Scores: " + GameMode1_Manager.scores.ToString() + "\r\n";
            WinTextDetails.text += "Errors: " + GameMode1_Manager.errors.ToString() + "\r\n";
            WinTextDetails.text += "Total time: " + (minutes != "0" ? minutes + " minutes " : "") + seconds + " seconds\r\n";
            WinTextDetails.text += "Rang: " + RangEng[Rang];
        }

    }
}
