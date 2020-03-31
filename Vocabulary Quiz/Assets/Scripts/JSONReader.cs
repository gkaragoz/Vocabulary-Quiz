using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour {

    [SerializeField]
    private string jsonFileName;

    private string LoadResourceTextfile() {
        TextAsset targetFile = Resources.Load<TextAsset>(jsonFileName);

        return targetFile.text;
    }

    public List<Word> GetWords() {
        string jsonString = LoadResourceTextfile();

        ExportData exportedData = JsonUtility.FromJson<ExportData>(jsonString);

        return exportedData.data;
    }

}
