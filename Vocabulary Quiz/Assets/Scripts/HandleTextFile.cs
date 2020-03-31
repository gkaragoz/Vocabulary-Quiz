using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;

public class HandleTextFile : MonoBehaviour {

    [SerializeField]
    private string _path = string.Empty;
    [SerializeField]
    private string api_key = string.Empty;

    [SerializeField]
    private List<Word> _words = new List<Word>();

    private int _fileCounter = 0;
    private ExportData _exportData;

    private void Awake() {
        _exportData = new ExportData() {
            data = new List<Word>()
        };

        //StartCoroutine(ITranslate());
    }

    public string[] ReadTextFile() {
        string readedString= Resources.Load<TextAsset>(_path).text;

        if (readedString == string.Empty)
            return null;

        return readedString.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
    }

    public string GetTurkish(string word) {
        string uri = "https://translate.yandex.net/api/v1.5/tr.json/translate?key=" + api_key + "&text=" + word + "&lang=" + "tr";
        StartCoroutine(GetRequest(word, uri));

        // TO DO API request.
        return string.Empty;
    }

    private IEnumerator ITranslate() {
        string[] dataSetAsStrings = ReadTextFile();

        int counter = 0;
        while (true) {
            GetTurkish(dataSetAsStrings[counter++]);
            if (counter == _words.Count)
                break;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator GetRequest(string word, string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError) {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            } else {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                YandexResponse response = JsonUtility.FromJson<YandexResponse>(webRequest.downloadHandler.text);
                HandleResponse(word, response);
            }
        }
    }

    private void HandleResponse(string word, YandexResponse response) {
        _words.Add(new Word() {
            en = word,
            tr = response.text[0]
        });

        _exportData.data = _words;

        string wordJson = JsonUtility.ToJson(_exportData, true);
        System.IO.File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\App\\testexport" + (_fileCounter++) + ".json", wordJson);
    }

}
#endif