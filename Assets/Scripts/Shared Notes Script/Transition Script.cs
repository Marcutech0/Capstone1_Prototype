using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Linq;


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
    [SerializeField] private Shared_Notes_Manager sharedNotesManager;
    #endregion

    #region Text Fragment 
    [SerializeField] private TMP_Text[] textFragmentDisplay;

    #endregion

    [SerializeField] private int notebookAnimatorLayer = 0;
    [SerializeField] private float notebookAnimationTimeout = 8f;

    void Start()
    {
        if (textFragmentDisplay != null)
        {
            foreach (TMP_Text fragment in textFragmentDisplay)
            {
                if (fragment != null)
                {
                    fragment.alpha = 0f;
                }
            }
        }

        sharedNotesManager?.HideUnplacedFragments();
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

        notebookAnimator.SetTrigger("Show"); //Notebook transition after text displays

        yield return StartCoroutine(WaitForNotebookAnimation());

        sharedNotesManager?.ShowUnplacedFragments();
        textFragmentDisplay.ToList().ForEach(textFragment => textFragment.alpha = 1f); //Reveals text fragments

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

    IEnumerator WaitForNotebookAnimation()
    {
        if (notebookAnimator == null)
            yield break;

        float elapsed = 0f;

        // Allow animator to process trigger
        yield return null;

        while (notebookAnimator.IsInTransition(notebookAnimatorLayer))
        {
            if (AdvanceAnimationTimer(ref elapsed))
                yield break;
            yield return null;
        }

        AnimatorStateInfo stateInfo = notebookAnimator.GetCurrentAnimatorStateInfo(notebookAnimatorLayer);

        while (stateInfo.normalizedTime < 1f)
        {
            if (AdvanceAnimationTimer(ref elapsed))
                yield break;

            yield return null;

            if (notebookAnimator.IsInTransition(notebookAnimatorLayer))
            {
                while (notebookAnimator.IsInTransition(notebookAnimatorLayer))
                {
                    if (AdvanceAnimationTimer(ref elapsed))
                        yield break;
                    yield return null;
                }
            }

            stateInfo = notebookAnimator.GetCurrentAnimatorStateInfo(notebookAnimatorLayer);
        }
    }

    private bool AdvanceAnimationTimer(ref float elapsed)
    {
        if (notebookAnimationTimeout <= 0f)
            return false;

        elapsed += Time.deltaTime;
        return elapsed >= notebookAnimationTimeout;
    }

}
