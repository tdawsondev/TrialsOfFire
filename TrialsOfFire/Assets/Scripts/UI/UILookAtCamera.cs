using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Player.Instance.mainCamera.transform.rotation * Vector3.forward, Player.Instance.mainCamera.transform.rotation * Vector3.up);
    }
}
