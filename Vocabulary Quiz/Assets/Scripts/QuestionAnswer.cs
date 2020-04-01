using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAnswer : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txt = null;
    [SerializeField]
    private Word _word = null;
    [SerializeField]
    private Color _successColor = Color.white;
    [SerializeField]
    private Color _incorrectColor = Color.white;
    [SerializeField]
    private Color _defaultColor = Color.white;

    private Button _button;
    private Image _image;

    public Word Word {
        get {
            return _word;
        }
    }

    private void Awake() {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        InitOnClick();
    }

    public void SetAnswer(Word word) {
        _word = word;
        _txt.text = word.tr;

        _image.color = _defaultColor;
    }

    public void SetAnswerGraphic(bool isCorrect) {
        if (isCorrect) {
            _image.color = _successColor;
        } else {
            _image.color = _incorrectColor;
        }
    }

    private void InitOnClick() {
        _button.onClick.AddListener(() => {
            QuestionManager.instance.OnAnswerClicked(this);
        });
    }
    
}
