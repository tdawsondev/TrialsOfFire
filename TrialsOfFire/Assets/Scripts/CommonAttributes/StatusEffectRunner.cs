using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectRunner : MonoBehaviour
{
    public List<StatusEffect> activeEffects = new List<StatusEffect>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (activeEffects.Count <= 0)
            return;
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            if (activeEffects[i].running)
            {
                activeEffects[i].Run();
            }
            if (activeEffects[i].finished)
            {

                activeEffects.Remove(activeEffects[i]);
            }
        }
    }

    public virtual void AddEffect(StatusEffect newEffect)
    {
        CheckDuplicateEffectsAndRefresh(newEffect);
    }
    public virtual void RemoveEffect(StatusEffect effect)
    {
        effect.Stop();
    }

    public void CheckDuplicateEffectsAndRefresh(StatusEffect newEffect)
    {
        bool hasType = false;
        bool addEffect = false;
        foreach(var effect in activeEffects)
        {
            if(effect.GetType() == newEffect.GetType())
            {
                hasType = true;
                if(newEffect.maxDuration > effect.timeLeft)
                {
                    effect.Stop();
                    addEffect = true;
                }
            }
        }
        if (!hasType || addEffect)
        {
            newEffect.StartRun();
            activeEffects.Add(newEffect);
        }
    }
}
