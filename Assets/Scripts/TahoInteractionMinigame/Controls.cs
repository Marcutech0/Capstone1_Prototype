using UnityEngine;
using TMPro;

public class Controls : MonoBehaviour
{
    public float _FillRate; 
    public TextMeshProUGUI _FillPercentText; 
    void Update()
    {
        PourSoy();
    }

    void PourSoy()
    {
        // if no selected cup return if the player presses space fill the selected cup based on fill rate
        if (CupClickManager._CurrentlySelectedCup == null) return;

        if (Input.GetKey(KeyCode.Space))
        {
            CupClickManager._CurrentlySelectedCup._FillPercent += _FillRate * Time.deltaTime;
            CupClickManager._CurrentlySelectedCup._FillPercent = Mathf.Clamp(
                CupClickManager._CurrentlySelectedCup._FillPercent, 0f, 100f);

            // Updates UI text based on the currently selected cup fill amount data in correlation to the players fill rate
            if (_FillPercentText != null)
                _FillPercentText.text = $"{CupClickManager._CurrentlySelectedCup._FillPercent:0}%";
        }
    }
}
