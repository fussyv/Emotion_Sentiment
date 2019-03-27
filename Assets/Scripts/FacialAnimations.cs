using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Timers;

public class FacialAnimations : MonoBehaviour
{
    private float talkAnimation = 0.0f;
    private float smileAnimation = 0.0f;
    private float mildSmileAnimation = 0.0f;
    private float shockAnimation = 0.0f;
    private float sadAnimation = 0.0f;
    private float tentativeAnimation = 0.0f;
    private float angerAnimation = 0.0f;
    private bool zeroflag;
    private bool activeTalk;
    private string textChanged;
    private string oldTextChanged;
    public Text EmotionResults;
    public Text TextToAnalyze1;
    public bool smileFlag;
    public bool sadFlag;
    public bool shockFlag;
    public bool tentativeFlag;
    public bool mildSmileFlag;
    public bool analyzeFlag;
    public bool angerFlag;
    public bool sendableString;
    public bool talkFlag;
    public double max;
    public string strongestEmotion;
    public ExampleToneAnalyzer.Example analyzer = new ExampleToneAnalyzer.Example();
    public string[] happinessWords;
    public string[] sadnessWords;
    public string[] shockWords;


    // Start is called before the first frame update
    void Start()
    {
        happinessWords = new string[] { "happy", "enjoy", "joy", "good", "thanks", "laugh", "cheers", "cheerful",
            "nice", "wonderful", "exciting", "beautiful", "cheerful", "enjoyable", "amazing", "funny", "grateful", "lucky", "awesome"};
        sadnessWords = new string[] { "sorry", "unhappy", "blue", "melancholy", "wistful",
            "homesick", "awful", "sad", "sadness", "suicidal", "uneasy", "easy", "disappointed", "disappointing",
            "regretful", "depressed", "depression", "depressing", "upset", "worried", "hopeless", "dolorous", "dark", "regretful", "regret", "helpless" };
        shockWords = new string[] {"fear", "shocked", "scared", "terrified", "terrify", "scary" };
        activeTalk = true;

        //InvokeRepeating("TriggerTalk", 0.0f, 0.001f);
        oldTextChanged = EmotionResults.text;
        //analyzer = JsonUtility.FromJson<ExampleToneAnalyzer.Example>("{\"document_tone\":{\"tones\":[{\"score\":0.880435,\"tone_id\":\"joy\",\"tone_name\":\"Joy\"},{\"score\":0.946222,\"tone_id\":\"tentative\",\"tone_name\":\"Tentative\"}]}}");
    }
 

    void TriggerSmile()
    {
        if (smileAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, smileAnimation += 1);
            zeroflag = false;
        }

        if (smileAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, smileAnimation -= 1);
            zeroflag = true;
        }

        if (smileAnimation <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerSmile");
        }
    }

    void TriggerMildSmile()
    {
        if (mildSmileAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(5, mildSmileAnimation += 1);
            zeroflag = false;
        }

        if (mildSmileAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(5, mildSmileAnimation -= 1);
            zeroflag = true;
        }

        if (mildSmileAnimation <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerMildSmile");
        }
    }

    void TriggerShock()
    {
        if (shockAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, shockAnimation += 1);
            zeroflag = false;
        }

        if (shockAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, shockAnimation -= 1);
            zeroflag = true;
        }

        if (shockAnimation <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerShock");
        }
    }

    void TriggerAnger()
    {
        if (angerAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(6, angerAnimation += 1);
            zeroflag = false;
        }

        if (angerAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(6, angerAnimation -= 1);
            zeroflag = true;
        }

        if (angerAnimation <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerAnger");
        }
    }

    void TriggerSad()
    {
        if (sadAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, sadAnimation += 1);
            zeroflag = false;
        }

        if (sadAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, sadAnimation -= 1);
            zeroflag = true;
        }

        if (sadAnimation <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerSad");
        }
    }

    void TriggerTentative()
    {
        if (tentativeAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, tentativeAnimation += 1);
            zeroflag = false;
        }

        if (tentativeAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, tentativeAnimation -= 1);
            zeroflag = true;
        }

        if (tentativeAnimation <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerTentative");
        }
    }

    void TriggerTalk()
    {
        if (talkAnimation <= 0 || zeroflag == false)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, talkAnimation += 3);
            zeroflag = false;
        }

        if (talkAnimation >= 100 || zeroflag == true)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, talkAnimation -= 3);
            zeroflag = true;
        }
        /*if (lipSize <= 0 && zeroflag == true)
        {
            CancelInvoke("TriggerTalk");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //if (analyzer.document_tone.tones.Length > 0)
        //{
            //print(analyzer.document_tone.tones.Length);
            textChanged = EmotionResults.text;
        //}
        //print(analyzer.document_tone.tones.Length);

        /*for (int i = 0; i < shockWords.Length; i++){
            //print(TextToAnalyze1.text);
            if (TextToAnalyze1.text.Contains(shockWords[i]))
            {
                sendableString = true;
            }
        }*/


        /*if(sendableString){
            print("Sendable state " + sendableString);
        }*/



        if (oldTextChanged != textChanged && EmotionResults.text != "Watson Tone Analyzer") //&& sendableString == true)
        {
            //System.Threading.Thread.Sleep(3000);
            print("JSON IS READY");
            analyzer = JsonUtility.FromJson<ExampleToneAnalyzer.Example>(EmotionResults.text);
            analyzeFlag = false;
            smileFlag = false;
            sadFlag = false;
            tentativeFlag = false;
            mildSmileFlag = false;
            angerFlag = false;
            sendableString = false;
            EmotionResults.text = "";
            oldTextChanged = textChanged;
        }



        //print("HEY THE LENGTH IS: " + analyzer.document_tone.tones.Length );
        //if (analyzer.document_tone.tones.Length > 0 && analyzeFlag == false)
        if (analyzeFlag == false && analyzer != null)
        {
            print("IFS PASSED");
            for (int j = 0; j < analyzer.document_tone.tones.Length; j++)
            {
                print("HEY THE LENGTH IS: " + analyzer.document_tone.tones.Length);
                if (analyzer.document_tone.tones[j].score > max)
                {
                    max = analyzer.document_tone.tones[j].score;
                    strongestEmotion = analyzer.document_tone.tones[j].tone_name;
                }
            }
            print("The highest emotion is: " + strongestEmotion);
            //print("NOW IT'S CHECKING THE TONE IN THE FOR LOOP");

            //print("NOW IT'S CHECKING THE TONE IN THE FOR LOOP");

            if (strongestEmotion == "Joy" && smileFlag != true)
            {
                print("Smile animation triggered");
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerSmile", 0.0f, 0.001f);
                activeTalk = false;
                smileFlag = true;

            }
            if (strongestEmotion == "Sadness" && sadFlag != true)
            {
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerSad", 0.0f, 0.001f);
                activeTalk = false;
                sadFlag = true;
            }
            if (strongestEmotion == "Fear" && shockFlag != true)
            {
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerShock", 0.0f, 0.001f);
                activeTalk = false;
                shockFlag = true;
            }
            if (strongestEmotion == "Analytical" && tentativeFlag != true)
            {
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerTentative", 0.0f, 0.001f);
                activeTalk = false;
                tentativeFlag = true;
            }
            if (strongestEmotion == "Confident" && mildSmileFlag != true)
            {
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerMildSmile", 0.0f, 0.001f);
                activeTalk = false;
                mildSmileFlag = true;
            }
            if (strongestEmotion == "Tentative" && mildSmileFlag != true)
            {
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerMildSmile", 0.0f, 0.001f);
                InvokeRepeating("TriggerTentative", 0.1f, 0.001f);

                activeTalk = false;
                mildSmileFlag = true;
            }
            if (strongestEmotion == "Anger" && angerFlag != true)
            {
                CancelInvoke("TriggerTalk");
                InvokeRepeating("TriggerAnger", 0.0f, 0.001f);
                activeTalk = false;
                angerFlag = true;
            }

            max = 0;
            strongestEmotion = "";
            analyzeFlag = true;
        }
        /*if (activeTalk != true)
        {
            //InvokeRepeating("TriggerTalk", 1.5f, 0.001f);
            activeTalk = true;
        }*/
       // }
    }

}
