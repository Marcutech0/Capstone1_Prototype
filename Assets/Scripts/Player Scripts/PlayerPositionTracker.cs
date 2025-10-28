using UnityEngine;

public class PlayerPositionTracker : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
     private CharacterController characterController;
    private Vector3 playerPosition;

    private void Awake()
    {
        characterController = playerTransform.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        playerPosition = playerTransform.position;
    }

    public void SavePlayer()
    {       
        SaveSystem.SavePlayer(playerTransform.position);
        Debug.Log("Game Saved");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        playerTransform.position = position;
        Debug.Log("Game Loaded");

        if (characterController != null)
        {
            characterController.enabled = false; // Disable the CharacterController to avoid conflicts
            playerTransform.position = position;
            characterController.enabled = true; // Re-enable the CharacterController
        }
    }
}
