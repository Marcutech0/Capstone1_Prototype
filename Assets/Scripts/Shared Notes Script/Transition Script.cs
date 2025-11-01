using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


public class TransitionScript : MonoBehaviour
{
    #region Blackboard Text & Transition
    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] private string[] Blackboard_text;
    [SerializeField] private float writeSpeed = 0.05f;
    [SerializeField] private float eraseDelay = 1.0f;
    [SerializeField] private float eraseDuration = 1.0f;
    #endregion

    #region Notebook Transition
    [SerializeField] private Animator notebookAnimator;

    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlayLectureTopic());
    }

    #region BlackboardText
    IEnumerator PlayLectureTopic() //Plays a transition of text on the whiteboard
    {
        for (int i = 0; i < Blackboard_text.Length; i++)
        {
            yield return StartCoroutine(TypeText(Blackboard_text[i]));

            yield return new WaitForSeconds(eraseDelay);

            yield return StartCoroutine(EraseText());

            textDisplay.alpha = 1f; 
        }
        textDisplay.alpha = 0f;

        notebookAnimator.SetTrigger("Show"); //Notebook transition after text display
    }
    IEnumerator TypeText(string fulltext)
    {
        textDisplay.text = "";
        foreach (char letter in fulltext)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(writeSpeed);
        }
    }

    IEnumerator EraseText()
    {
        float elapsed = 0f;
        while (elapsed < eraseDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
            textDisplay.alpha = 1 - (elapsed / eraseDuration);
        }
    }
    #endregion


}
