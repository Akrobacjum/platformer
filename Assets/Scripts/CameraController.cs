using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    void Update()
    {
        //Glues camera to the player position.
        transform.position = new Vector3(Player.transform.position.x + 1, Player.transform.position.y, -10);
    }
}
