using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextFragment : MonoBehaviour
{
    [SerializeField] private string fragmentText;
    public bool isCorrect;

    [SerializeField] TMP_Text textCompo;
    [SerializeField] Button button;

    private System.Action<TextFragment> clickCallback;

    public int Index { get; set; }

    public void Initialize(string text, bool correct, System.Action<TextFragment> onClickCallback)
    {
        fragmentText = text;
        isCorrect = correct;
        textCompo.text = fragmentText;

        clickCallback = onClickCallback;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
        button.interactable = true;
    }

    private void HandleClick()
    {
        clickCallback?.Invoke(this);
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }

}
