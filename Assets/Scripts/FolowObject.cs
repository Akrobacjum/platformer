using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform positionX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = positionX.position;
    }
}
