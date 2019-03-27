/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/
#pragma warning disable 0649

using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Logging;
using System.Collections;
using IBM.Watson.DeveloperCloud.Connection;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;


public class ExampleToneAnalyzer : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/tone-analyzer/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Tooltip("The version date with which you would like to use the service in the form YYYY-MM-DD.")]
    [SerializeField]
    private string _versionDate;
    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string _iamApikey;

    [Tooltip("Text field to display the results of streaming.")]
    public Text EmotionResults;
    public Text TextToAnalyze;
    #endregion

    private ToneAnalyzer _service;
    private string _stringToTestTone;
    private string _oldStringToTestTone;

    private bool _analyzeToneTested = false;

    void Start()
    {

        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    private void Update()
    {

        _stringToTestTone = TextToAnalyze.text;

        if(_oldStringToTestTone != _stringToTestTone)
        {
            Runnable.Run(CreateService());
        }

        _oldStringToTestTone = _stringToTestTone;
        //_service.GetToneAnalyze(OnGetToneAnalyze, OnFail, _stringToTestTone);

    }

    private IEnumerator CreateService()
    {
        if (string.IsNullOrEmpty(_iamApikey))
        {
            throw new WatsonException("Plesae provide IAM ApiKey for the service.");
        }

        //  Create credential and instantiate service
        Credentials credentials = null;
        
        //  Authenticate using iamApikey
        TokenOptions tokenOptions = new TokenOptions()
        {
            IamApiKey = _iamApikey
        };

        credentials = new Credentials(tokenOptions, _serviceUrl);

        //  Wait for tokendata
        while (!credentials.HasIamTokenData())
            yield return null;

        _service = new ToneAnalyzer(credentials);
        _service.VersionDate = _versionDate;

        Runnable.Run(Examples());
    }

    private IEnumerator Examples()
    {

        //  Analyze tone
        if (!_service.GetToneAnalyze(OnGetToneAnalyze, OnFail, _stringToTestTone))
            Log.Debug("ExampleToneAnalyzer.Examples()", "Failed to analyze!");

        while (!_analyzeToneTested)
            yield return null;

        Log.Debug("ExampleToneAnalyzer.Examples()", "Tone analyzer examples complete.");
    }

    private void OnGetToneAnalyze(ToneAnalysis resp, Dictionary<string, object> customData)
    {
        Log.Debug("ExampleToneAnalyzer.OnGetToneAnalyze()", "{0}", customData["json"].ToString());

        _analyzeToneTested = true;

        string RAW = (customData["json"].ToString());  // works but long and cannot read
        string test = "{\"document_tone\":{\"tones\":[{\"score\":0.880435,\"tone_id\":\"joy\",\"tone_name\":\"Joy\"},{\"score\":0.946222,\"tone_id\":\"tentative\",\"tone_name\":\"Tentative\"}]}}";
        
        var emotions = JsonUtility.FromJson<DocumentTone>(test);

        //

        //print(ex1.document_tone.tones[0].score);
        //print(ex1.health);
        //print(ex1.lives);

        /*RAW = string.Concat("Tone Response \n", RAW); 
        RAW = Regex.Replace(RAW, "tone_categories", "");
        RAW = Regex.Replace(RAW, "sentence_id", "\\\n");
        RAW = Regex.Replace(RAW, "}", "");
        RAW = Regex.Replace(RAW, "tone_id", " ");
        RAW = Regex.Replace(RAW, "tone_name", " ");
        RAW = Regex.Replace(RAW, "score", " ");
        RAW = Regex.Replace(RAW, @"[{\\},:]", "");
        RAW = Regex.Replace(RAW, @"[\[\]']+", "");
        RAW = Regex.Replace(RAW, "\"", "");*/
        EmotionResults.text = RAW;
        // Debug.Log(emotions.tones.ToArray());
//        Debug.Log(RAW);

    }

    [Serializable]
    public class Tone
    {
        public double score;
        public string tone_id;
        public string tone_name;
    }
    [Serializable]
    public class DocumentTone
    {
        [SerializeField]
        public Tone[] tones;
    }
    [Serializable]
    public class Example
    {
        [SerializeField]
        public DocumentTone document_tone;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleRetrieveAndRank.OnFail()", "Error received: {0}", error.ToString());
    }
}
