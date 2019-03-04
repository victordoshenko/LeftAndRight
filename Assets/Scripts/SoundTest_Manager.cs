using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class SoundTest_Manager : MonoBehaviour {

    public Text textCommand;
/*
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    // Use this for initialization
    void Start () {
        keywordActions.Add("left", DoLeft);
        keywordActions.Add("right", DoRight);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        keywordActions[args.text].Invoke();
    }
*/
    private void DoLeft()
    {
        textCommand.text = "left";
    }

    private void DoRight()
    {
        textCommand.text = "right";
    }

    public void ButtonReturn_Click()
    {
        SceneManager.LoadScene("Menu");
    }


    /*
        // Update is called once per frame
        void Update () {

        }
    */
}
