using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInertia : MonoBehaviour
{
    public float smooth;
    public float mult;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mult;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mult;
        Quaternion rotationX = Quaternion.AngleAxis(mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
