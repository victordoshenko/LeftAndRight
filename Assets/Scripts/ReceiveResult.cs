using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ReceiveResult : MonoBehaviour {
    public static int State = 0;
    // Use this for initialization
    void Start () {
        //GameObject.Find("Text").GetComponent<Text>().text = "You need to be connected to Internet";
	}
	
    void onActivityResult(string recognizedText){
        //char[] delimiterChars = {'~'};
        //string[] result = recognizedText.Split(delimiterChars);

        //You can get the number of results with result.Length
        //And access a particular result with result[i] where i is an int
        //I have just assigned the best result to UI text
        //GameObject.Find("Text").GetComponent<Text>().text = result[0];
        GameObject.Find("TextonActivityResult").GetComponent<Text>().text = recognizedText;
    }

    void onReadyForSpeech(string myText)
    {
        GameObject.Find("TextonReadyForSpeech").GetComponent<Text>().text = myText;
    }

    void onBeginningOfSpeech(string myText)
    {
        GameObject.Find("TextonBeginningOfSpeech").GetComponent<Text>().text = myText;
    }

    void onRmsChanged(string myText)
    {
        GameObject.Find("TextonRmsChanged").GetComponent<Text>().text = myText;
    }

    void onBufferReceived(string myText)
    {
        GameObject.Find("TextonBufferReceived").GetComponent<Text>().text = myText;
    }

    void onEndOfSpeech(string myText)
    {
        GameObject.Find("TextonEndOfSpeech").GetComponent<Text>().text = myText;
        //if (State != 2)
        //{
        //Test.pluginClass.CallStatic("StopRecording");
        //Test.pluginClass.CallStatic("StartRecording");
            //State = 2;
        //}
    }

    void onError(string myText)
    {
        /*
        if (myText == "ERROR_RECOGNIZER_BUSY" && State != 1)
        {
            GameObject.Find("TextonEndOfSpeech").GetComponent<Text>().text = "";
            Test.pluginClass.CallStatic("StopRecording");
            //Test.pluginClass.CallStatic("StartRecording");
            //State = 1;
        }
        else if ((myText == "ERROR_SPEECH_TIMEOUT" || myText == "ERROR_NO_MATCH" || myText == "ERROR_CLIENT") && State != 2)
        {
            Test.pluginClass.CallStatic("StartRecording");
            //State = 2;
        }
        */
        GameObject.Find("TextonError").GetComponent<Text>().text = myText;
    }

    void onResults(string myText)
    {
        GameObject.Find("TextonResults").GetComponent<Text>().text = myText;
    }

    void onPartialResults(string myText)
    {
        GameObject.Find("TextonPartialResults").GetComponent<Text>().text = myText;
        int d = GetDir(myText, "прав трав", "лев лег");
        switch (d)
        {
            case 1:
                GameObject.Find("TextonEvent").GetComponent<Text>().text = "Право!";
                break;
            case 2:
                GameObject.Find("TextonEvent").GetComponent<Text>().text = "Лево!";
                break;
            default:
                GameObject.Find("TextonEvent").GetComponent<Text>().text = "N/A";
                break;
        }
    }

    void onEvent(string myText)
    {
        GameObject.Find("TextonEvent").GetComponent<Text>().text = myText;
    }

    void onMessage(string myText)
    {
        GameObject.Find("TextonMessage").GetComponent<Text>().text = myText;
    }

    public int GetDir(string s, string r_, string l_)
    {
        // Get Direction from input s and masked templates r_, l_
        // Return: 1 = Right  2 = Left  0 = N/A
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

    // Update is called once per frame
    void Update () {
		
	}
}
