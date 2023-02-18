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
    public Animator hitAnimator;



    // Start is called before the first frame update
    void Start()
    {
        health.onHealthChange += PlayerHealthChanged;
    }

    private void PlayerHealthChanged(float amount, Transform changedBy = null)
    {
        HUDController.instance.UpdateHealth();

        if(amount > 0)
        {
            AudioManager.instance.Play("PlayerHit");

            Vector3 direction = transform.position - changedBy.position;
            float frontDot = Vector3.Dot(direction, transform.forward);
            float rightDot = Vector3.Dot(direction, transform.right);
            if (Mathf.Abs(frontDot) > Mathf.Abs(rightDot))
            {
                if (frontDot > 0)
                {
                    HUDController.instance.StartDirectionFade(HUDController.instance.bottomBar);
                    hitAnimator.Play("PlayerHitLeft");
                }
                else
                {
                    HUDController.instance.StartDirectionFade(HUDController.instance.topBar);
                    hitAnimator.Play("PlayerHitRight");
                }
            }
            else
            {
                if (rightDot > 0)
                {
                    HUDController.instance.StartDirectionFade(HUDController.instance.leftBar);
                    hitAnimator.Play("PlayerHitLeft");
                }
                else
                {
                    HUDController.instance.StartDirectionFade(HUDController.instance.rightBar);
                    hitAnimator.Play("PlayerHitRight");
                }
            }
        }
        if (health.Dead)
        {
            MenuController.instance.OpenGameOverMenu();
        }

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
