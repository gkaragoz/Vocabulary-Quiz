using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour {

    #region Singleton

    public static QuestionManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private TextMeshProUGUI _txtQuestion = null;
    [SerializeField]
    private QuestionAnswer[] _questionAnswers = null;
    [SerializeField]
    private Word _currentQuestion = null;

    private JSONReader _jsonReader = null;
    private List<Word> _words = new List<Word>();

    private void Start() {
        _jsonReader = GetComponent<JSONReader>();

        _words = _jsonReader.GetWords();

        SetNextQuestion();
    }

    public void SetNextQuestion() {
        _currentQuestion = SetRandomQuestion();
    }

    public Word SetRandomQuestion() {
        int randomIndex = Random.Range(0, _words.Count);
        Word correntWord = _words[randomIndex];
        _txtQuestion.text = correntWord.en;

        int randomCorrectButton = Random.Range(0, 4);
        QuestionAnswer correctAnswer = _questionAnswers[randomCorrectButton];

        foreach (QuestionAnswer questionAnswer in _questionAnswers) {
            if (questionAnswer == correctAnswer) {
                questionAnswer.SetAnswer(correntWord);
            } else {
                Word falseWord = _words[RandomRangeExcept(0, _words.Count, randomIndex)];
                questionAnswer.SetAnswer(falseWord);
            }
        }

        return correntWord;
    }

    public void OnAnswerClicked(Word word) {
        if (_currentQuestion == word) {
            Debug.Log("Bildin");
        } else {
            Debug.LogWarning("Bilemedin: " + _currentQuestion.tr);
        }

        SetNextQuestion();
    }

    private int RandomRangeExcept(int min, int max, int except) {
        int number = -1;
        do {
            number = Random.Range(min, max);
        } while (number == except);
        return number;
    }

}
