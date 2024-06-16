using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyBase EnemyBase;
    public float dirX;
    bool right;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion Rotation = Quaternion.LookRotation(transform.position, Vector2.right);
        transform.rotation = Rotation;
    }
    public void CheckSide()
    {
        if (dirX < 0 && right)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            right = false;
        }
        if (dirX > 0 && !right)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            right = true;

        }
    }
}
