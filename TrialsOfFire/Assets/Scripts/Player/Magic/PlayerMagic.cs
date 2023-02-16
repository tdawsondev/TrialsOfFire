using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public Transform castPoint;
    public Hand rightHand;
    public Hand leftHand;

    private bool systemLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (systemLocked) return;

        if (Input.GetMouseButton(0))
        {
            rightHand.OnPress();
        }
        if (Input.GetMouseButtonUp(0))
        {
            rightHand.OnRelease();
        }
        if (Input.GetMouseButton(1))
        {
            leftHand.OnPress();
        }
        if (Input.GetMouseButtonUp(1))
        {
            leftHand.OnRelease();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rightHand.SwapSpells();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            leftHand.SwapSpells();
        }

    }

    public void LockSystem()
    {
        systemLocked = true;
    }
    public void UnlockSystem()
    {
        systemLocked = false;
    }
}
