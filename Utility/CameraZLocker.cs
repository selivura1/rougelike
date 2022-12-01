using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZLocker : MonoBehaviour
{
    [SerializeField] float ZValue = -10;
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, ZValue);
    }
}
