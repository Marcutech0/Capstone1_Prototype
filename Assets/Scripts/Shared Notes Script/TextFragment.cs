using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextFragment : MonoBehaviour
{
    [SerializeField] private string fragmentText;
    public bool isCorrect;

    [SerializeField] TMP_Text textCompo;
    [SerializeField] Button button;

    public int Index { get; set; }
    public void Initialize(string text, bool correct, System.Action<TextFragment> onClickCallback)
    {
        fragmentText = text;
        isCorrect = correct;
        textCompo.text = fragmentText;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClickCallback(this));
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
        if (!interactable)
            button.onClick.RemoveAllListeners();
    }

}
