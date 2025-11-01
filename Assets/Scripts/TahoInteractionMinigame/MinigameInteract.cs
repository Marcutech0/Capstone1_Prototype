using UnityEngine;
using TMPro;
public class MinigameInteract : MonoBehaviour
{
    public GameObject _MinigameInidcator;
    public GameObject _TahoCounter;
    public bool _isIndicating;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TahoMinigame")) 
        {
           _MinigameInidcator.SetActive(true);
            _isIndicating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TahoMinigame")) 
        {
            _MinigameInidcator.SetActive(false);
            _TahoCounter.SetActive(false);
        }
    }

    private void Update()
    {
        if (_isIndicating) 
        {
            if (Input.GetKeyDown(KeyCode.F))    
            {
                _TahoCounter.SetActive(true);
            }
        }
    }
}
