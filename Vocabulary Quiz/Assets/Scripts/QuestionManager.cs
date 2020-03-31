using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour {

    private JSONReader _jsonReader = null;
    private List<Word> _words = new List<Word>();

    private void Awake() {
        _jsonReader = GetComponent<JSONReader>();

        _words = _jsonReader.GetWords();
    }

}
