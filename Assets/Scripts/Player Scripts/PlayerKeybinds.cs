using UnityEngine;

public class PlayerKeybinds : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            Time.timeScale = settingsMenu.activeSelf ? 0f : 1f;
        }
    }
}