using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public string leftOrRight;
    [SerializeField]
    public Spell equipSpell;
    public Spell spell1, spell2;
    public int spellEquip = 1;

    public bool isCharging = false;
    public bool delaying = false;
    Transform castPoint;

    public float chargeTime = 0f;

    [Header("Animation")]
    public Animator handAnim;
    public Animator chargeAnim;
    // Start is called before the first frame update
    void Start()
    {
        castPoint = Player.Instance.Magic.castPoint;
        EquipSpell(spell1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!delaying && isCharging)
        {
            chargeTime += Time.deltaTime;
            if (chargeTime > equipSpell.baseStats.chargeTime)
            {
                chargeTime = equipSpell.baseStats.chargeTime;
            }
            if(leftOrRight == "Right")
            {
                HUDController.instance.UpdateRightCharge();
            }
            if (leftOrRight == "Left")
            {
                HUDController.instance.UpdateLeftCharge();
            }
            handAnim.SetBool("Charging", true);
        }
        if (delaying)
        {
            chargeTime = 0f;
        }
    }

    // called when button is no longer held
    public void OnRelease()
    {
        isCharging = false;
        
        if (delaying) return;
        float chargeRatio = chargeTime / equipSpell.baseStats.chargeTime;
        if(chargeRatio < 0.15f) 
        {
            chargeRatio = 0.15f; // minimum potency ratio
        }
        handAnim.SetTrigger("Casting");
        handAnim.SetBool("Charging", false);
        equipSpell.CastSpell(castPoint, chargeRatio);
        chargeTime = 0f;
        HUDController.instance.UpdateHUD();
        StartCoroutine(Delaying());
    }

    IEnumerator Delaying()
    {
        delaying = true;
        yield return new WaitForSeconds(equipSpell.baseStats.chargeDelay);
        delaying = false;
    }

    public void OnPress()
    {
        isCharging = true;
    }

    public void SwapSpells()
    {
        if(spellEquip == 1)
        {
            spellEquip = 2;
            EquipSpell(spell2);
        }
        else
        {
            spellEquip = 1;
            EquipSpell(spell1);
        }
    }

    public void EquipSpell(Spell newSpell)
    {
        isCharging = false;
        chargeTime = 0f;
        handAnim.SetFloat("ChargeSpeed", (.792f / newSpell.baseStats.chargeTime) + .45f); // .792 is the default animation time;
        equipSpell = newSpell;
        StartCoroutine(Delaying());
        HUDController.instance.UpdateHUD();
    }
}
