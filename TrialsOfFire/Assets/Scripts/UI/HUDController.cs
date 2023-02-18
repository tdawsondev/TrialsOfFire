using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one instance of HUDController");
        }
        instance = this;
    }

    [SerializeField] Slider rightCharge;
    [SerializeField] Slider leftCharge;
    [SerializeField] TMP_Text leftSpellText, RightSpellText;
    [SerializeField] Image rightSliderImage, leftSliderImage;
    [SerializeField] Image spellImageRight, spellImageLeft;

    [SerializeField] Slider healthBar, manaBar;

    public CanvasGroup topBar, bottomBar, leftBar, rightBar;

    public Slider BossBar;



    // Start is called before the first frame update
    void Start()
    {
        UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateHUD()
    {
        UpdateRightCharge();
        UpdateLeftCharge();
        UpdateLeftSpell();
        UpdateRightSpell();
        UpdateHealth();
        UpdateMana();
    }

    public void UpdateRightCharge()
    {
        Hand hand = Player.Instance.Magic.rightHand;
        rightCharge.maxValue = hand.equipSpell.baseStats.chargeTime;
        rightCharge.value = hand.chargeTime;
    }
    public void UpdateLeftCharge()
    {
        Hand hand = Player.Instance.Magic.leftHand;
        leftCharge.maxValue = hand.equipSpell.baseStats.chargeTime;
        leftCharge.value = hand.chargeTime;
    }
    public void UpdateLeftSpell()
    {
        SpellSO baseStats = Player.Instance.Magic.leftHand.equipSpell.baseStats;
        leftSpellText.text = baseStats.spellName;
        leftSliderImage.color = baseStats.spellChargeColor;
        spellImageLeft.sprite = baseStats.spellIcon;

    }
    public void UpdateRightSpell()
    {
        SpellSO baseStats = Player.Instance.Magic.rightHand.equipSpell.baseStats;
        RightSpellText.text = baseStats.spellName;
        rightSliderImage.color = baseStats.spellChargeColor;
        spellImageRight.sprite = baseStats.spellIcon;
    }
    public void UpdateHealth()
    {
        healthBar.maxValue = Player.Instance.health.maxHP;
        healthBar.value = Player.Instance.health.currentHP;
    }
    public void UpdateMana()
    {

    }

    public void StartDirectionFade(CanvasGroup group)
    {
        group.alpha = 1f;
        StartCoroutine(FadeOut(group));

    }
    IEnumerator FadeOut(CanvasGroup group)
    {
        while (group.alpha > 0f)
        {
            group.alpha -= 1f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void UpdateBossBar(Health health)
    {
        BossBar.maxValue = health.maxHP;
        BossBar.value = health.currentHP;
    }


}
