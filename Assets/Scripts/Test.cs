using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {
    public Text timerText;
    public static float startTime = 0;

    Button btn;
    Text txt;
    public static AndroidJavaClass pluginClass;

    public void TimerRestart()
    {
        startTime = Time.time;
    }

    public void ButtonReturn_Click()
    {
        SceneManager.LoadScene("Menu");
    }

    // Use this for initialization
    void Start () {

        btn = GameObject.Find("ButtonStart").GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickStart);

        btn = GameObject.Find("ButtonEnd").GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickEnd);

        btn = GameObject.Find("ButtonCancel").GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickCancel);

        btn = GameObject.Find("ButtonStart2").GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickStart2);

        pluginClass = new AndroidJavaClass("com.plugin.speech.pluginlibrary.TestPlugin");

        GameObject.Find("InputField_MLM").GetComponent<InputField>().text = "1000";
        GameObject.Find("InputField_CSLM").GetComponent<InputField>().text = "1001";
        GameObject.Find("InputField_PCSLM").GetComponent<InputField>().text = "1002";
        TimerRestart();
        pluginClass.CallStatic("PrepareRecording");

    }

    void TaskOnClickCancel()
    {
        Debug.Log("TaskOnClickCancel");
        TimerRestart();
        pluginClass.CallStatic("CancelRecording");
    }

    void TaskOnClickEnd()
    {
        Debug.Log("TaskOnClickEnd");
        TimerRestart();
        pluginClass.CallStatic("StopRecording");

    }

    void ClearTxt()
    {
        txt = GameObject.Find("TextonMessage").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonActivityResult").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonReadyForSpeech").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonBeginningOfSpeech").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonRmsChanged").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonBufferReceived").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonEndOfSpeech").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonError").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonResults").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonPartialResults").GetComponent<Text>(); txt.text = "";
        txt = GameObject.Find("TextonEvent").GetComponent<Text>(); txt.text = "";
    }

    void TaskOnClickStart2()
    {
        ClearTxt();
        TimerRestart();
        pluginClass.CallStatic("RestartRecording");
    }

    void TaskOnClickStart()
    {
        //AndroidJavaClass pluginClass = new AndroidJavaClass("com.plugin.speech.pluginlibrary.TestPlugin");
        Debug.Log("TaskOnClickStart");

        ClearTxt();
        TimerRestart();

        // Pass the name of the game object which has the onActivityResult(string recognizedText) attached to it.
        // The speech recognizer intent will return the string result to onActivityResult method of "Main Camera"
        //pluginClass.CallStatic("setReturnObject", "Main Camera");
        //Debug.Log("Return Object Set");


        // Setting language is optional. If you don't run this line, it will try to figure out language based on device settings
        //pluginClass.CallStatic("setLanguage", "ru_RU");
        //Debug.Log("Language Set");


        // The following line sets the maximum results you want for recognition
        //pluginClass.CallStatic("setMaxResults", 3);
        //Debug.Log("Max Results Set");

        /*
                // The following line sets the question which appears on intent over the microphone icon
                pluginClass.CallStatic("changeQuestion", "Hello, How can I help you???");
                Debug.Log("Question Set");

                Debug.Log("Call 2 Started");
        */

        // Calls the function from the jar file
        //pluginClass.CallStatic("promptSpeechInput");

        pluginClass.CallStatic("set_MLM", System.Convert.ToInt32(GameObject.Find("InputField_MLM").GetComponent<InputField>().text));
        pluginClass.CallStatic("set_CSLM", System.Convert.ToInt32(GameObject.Find("InputField_CSLM").GetComponent<InputField>().text));
        pluginClass.CallStatic("set_PCSLM", System.Convert.ToInt32(GameObject.Find("InputField_PCSLM").GetComponent<InputField>().text));

        pluginClass.CallStatic("StartRecording");

        //Debug.Log("Call End");
    }

    // Update is called once per frame
    void Update () {
        float t = Mathf.Round((Time.time - startTime) * 100f) / 100f;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;

        /*
                if (GameObject.Find("TextonError").GetComponent<Text>().text == "ERROR_SPEECH_TIMEOUT" && pluginClass!=null)
                {
                    pluginClass.Dispose();
                    pluginClass = new AndroidJavaClass("com.plugin.speech.pluginlibrary.TestPlugin");
                    GameObject.Find("TextonError").GetComponent<Text>().text = "Restarted...";
                    TaskOnClickStart();
                }
        */
    }
}
