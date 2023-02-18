using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float amount;
    public bool healthPickup = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (healthPickup)
            {
                Player.Instance.health.Heal(amount);
            }

            Destroy(gameObject);
        }
    }
}
