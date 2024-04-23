using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerPivot : MonoBehaviour
{

    public Transform target; // The GameObject to follow

    void Update()
    {
        if (target != null)
        {
            // Set this GameObject's position to match the target's position
            transform.position = target.position;
        }
    }
}
