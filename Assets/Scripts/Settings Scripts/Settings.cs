using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = settingsMenu.activeSelf ? 0f : 1f;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        // stop Play Mode in the Editor
        EditorApplication.isPlaying = false;
#else
        // quit in a build
        Application.Quit();
#endif
    }
}

