using UnityEngine;

public class JSONReader : MonoBehaviour {

    public Word[] words;
    public Word testWord;

    private void Start() {
        Word word = new Word() {
            en = "Pencil",
            tr = "Kalem"
        };

        string jsonString = JsonUtility.ToJson(word);
        Debug.Log(jsonString);

        Word convertedFromJson = JsonUtility.FromJson<Word>(jsonString);
    }

}
