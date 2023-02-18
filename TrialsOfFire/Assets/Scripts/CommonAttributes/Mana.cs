using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana, currentMana;


    public void UseMana(float amount)
    {

        currentMana -= amount;
        if (currentMana < 0f)
        {
            currentMana = 0f;
        }
    }
    public void ReplenishMana(float amount)
    {
        currentMana += amount;
        if (currentMana > maxMana) currentMana = maxMana;
    }

   
}
