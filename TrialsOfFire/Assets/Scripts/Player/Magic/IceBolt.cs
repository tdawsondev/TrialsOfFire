using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBolt : ProjectileSpell
{
    public IceEffect iceEffect;
    public override void AfterEnemyHit(EnemyHealthReference reference)
    {
        base.AfterEnemyHit(reference);
        IceEffect effect = new IceEffect(iceEffect, reference.character);
        float chargeRatio = effectivePotency / baseStats.potency;
        if (chargeRatio < 0.15f) chargeRatio = 0.15f;
        effect.maxDuration = effect.maxDuration * chargeRatio;
        reference.character.effectRunner.AddEffect(effect);


    }
}
