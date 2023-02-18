using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationRef : MonoBehaviour
{
    public Dragon dragon;
    public void AfterGroundAttack()
    {
        dragon.AfterGroundAttack();
    }

    public void CheckDamage()
    {
        dragon.CheckDamage();
    }
    public void FlyUp()
    {
        ChainBoxManager.Instance.FlyUp();
    }
}
