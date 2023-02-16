using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of player");
            return;
        }
        Instance = this;
    }
    public Camera mainCamera;
    public Health health;
    public PlayerMagic Magic;
    public Character character;
    public PlayerController controller;



    // Start is called before the first frame update
    void Start()
    {
        health.onHealthChange += PlayerHealthChanged;
    }

    private void PlayerHealthChanged(float amount, Transform changedBy = null)
    {
        HUDController.instance.UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        health.onHealthChange -= PlayerHealthChanged;
    }

    public void LockPlayer()
    {
        controller.movementDisabled = true;
        Magic.LockSystem();
    }
    public void UnlockPlayer()
    {
        controller.movementDisabled = false;
        Magic.UnlockSystem();
    }
}
