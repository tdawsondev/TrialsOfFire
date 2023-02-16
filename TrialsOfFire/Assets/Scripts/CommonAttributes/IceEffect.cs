using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IceEffect : StatusEffect
{
    public float speedPotency;
    public float damagePotency;
    private StatModifier speedMod, damageMod;
    public IceEffect(float duration, Character character) : base(duration, character)
    {
    }
    public IceEffect(IceEffect effect, Character character) : base(effect, character)
    {
        speedPotency = effect.speedPotency;
        damagePotency = effect.damagePotency;
    }
    public override void StartRun()
    {
        speedMod = new StatModifier(speedPotency);
        damageMod = new StatModifier(damagePotency);
        effectedCharacter.Speed.AddModifier(speedMod);
        effectedCharacter.DamageVulnerabitly.AddModifier(damageMod);
        base.StartRun();
    }
    public override void Stop()
    {
        effectedCharacter.Speed.RemoveModifier(speedMod);
        effectedCharacter.DamageVulnerabitly.RemoveModifier(damageMod);
        base.Stop();
    }
}
