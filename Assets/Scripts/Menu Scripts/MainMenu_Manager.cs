using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] string[] scenes;
    public GameObject Settings_Menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Settings_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
            SceneManager.LoadScene(scenes[0]);
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

    //either Settings Menu in same scene or load new scene
    #region Settings Menu 
    public void openSettings()
    {
        Settings_Menu.SetActive(true);
        
    }

    public void closeSettings()
    {
        Settings_Menu.SetActive(false);
    }

    #endregion

}

