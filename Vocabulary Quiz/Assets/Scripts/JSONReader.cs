using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour {

    public string jsonFileName;

    public string LoadResourceTextfile(string jsonFileName) {
        TextAsset targetFile = Resources.Load<TextAsset>(jsonFileName);

        return targetFile.text;
    }

    public List<Word> GetWords(string jsonString) {
        ExportData exportedData = JsonUtility.FromJson<ExportData>(jsonString);

        return exportedData.data;
    }

}
