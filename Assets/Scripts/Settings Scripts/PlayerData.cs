using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class PlayerData
{
     public float[] position;

    public PlayerData (Vector3 playerPosition)
    {
        position = new float[3];
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;  
    }

} 