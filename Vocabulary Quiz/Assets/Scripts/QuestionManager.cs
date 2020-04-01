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
    private QuestionAnswer _currentQuestionAnswer = null;

    private JSONReader _jsonReader = null;
    private List<Word> _words = new List<Word>();
    private bool _isReadyToNextQuestion = false;

    private void Start() {
        _jsonReader = GetComponent<JSONReader>();

        _words = _jsonReader.GetWords();

        SetNextQuestion();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && _isReadyToNextQuestion) {
            SetNextQuestion();
        }
    }

    public void SetNextQuestion() {
        _isReadyToNextQuestion = false;

        int randomIndex = Random.Range(0, _words.Count);
        Word currentWord = _words[randomIndex];
        _txtQuestion.text = currentWord.en;

        int randomCorrectButton = Random.Range(0, 4);
        _currentQuestionAnswer = _questionAnswers[randomCorrectButton];

        foreach (QuestionAnswer questionAnswer in _questionAnswers) {
            if (questionAnswer == _currentQuestionAnswer) {
                questionAnswer.SetAnswer(currentWord);
            } else {
                Word falseWord = _words[RandomRangeExcept(0, _words.Count, randomIndex)];
                questionAnswer.SetAnswer(falseWord);
            }
        }
    }

    public void OnAnswerClicked(QuestionAnswer questionAnswer) {
        foreach (QuestionAnswer qa in _questionAnswers) {
            if (_currentQuestionAnswer == qa) {
                qa.SetAnswerGraphic(true);
            } else {
                questionAnswer.SetAnswerGraphic(false);
                _currentQuestionAnswer.SetAnswerGraphic(true);
            }
        }

        _isReadyToNextQuestion = true;
    }

    private int RandomRangeExcept(int min, int max, int except) {
        int number = -1;
        do {
            number = Random.Range(min, max);
        } while (number == except);
        return number;
    }

}
