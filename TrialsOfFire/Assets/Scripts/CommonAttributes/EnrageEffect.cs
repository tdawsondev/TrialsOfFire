using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnrageEffect : StatusEffect
{
    public float speedPotency;
    public float damagePotency;
    private StatModifier speedMod, damageMod;
    public EnrageEffect(float duration, Character character) : base(duration, character)
    {
    }
    public EnrageEffect(EnrageEffect effect, Character character): base(effect, character)
    {
        speedPotency = effect.speedPotency;
        damagePotency = effect.damagePotency;
    }
    public override void StartRun()
    {
        speedMod = new StatModifier(speedPotency);
        damageMod = new StatModifier(damagePotency);
        effectedCharacter.Speed.AddModifier(speedMod);
        effectedCharacter.DamageOutputModifier.AddModifier(damageMod);
        base.StartRun();
    }
    public override void Stop()
    {
        effectedCharacter.Speed.RemoveModifier(speedMod);
        effectedCharacter.DamageOutputModifier.RemoveModifier(damageMod);
        base.Stop();
    }
}
