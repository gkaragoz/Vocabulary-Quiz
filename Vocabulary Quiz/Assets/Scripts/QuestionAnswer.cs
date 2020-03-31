using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAnswer : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txt = null;
    [SerializeField]
    private Word _word = null;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();

        InitOnClick();
    }

    public void SetAnswer(Word word) {
        _word = word;
        _txt.text = word.tr;
    }

    private void InitOnClick() {
        _button.onClick.AddListener(() => {
            QuestionManager.instance.OnAnswerClicked(this._word);
        });
    }
    
}
