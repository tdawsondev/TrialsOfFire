using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthBarSlider;
    [SerializeField] private Health health;
    // Start is called before the first frame update
    void Start()
    {
        if(health != null)
        {
            UpdateBar();
            health.onHealthChange += UpdateBarEvent;
        } 
        
    }
    private void OnDestroy()
    {
        health.onHealthChange -= UpdateBarEvent;
    }

    private void UpdateBarEvent(float amount, Transform changedBy = null)
    {
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar()
    {
        if(health != null)
        {
            healthBarSlider.minValue = 0;
            healthBarSlider.maxValue = health.maxHP;
            healthBarSlider.value = health.currentHP;
        }
    }
}
