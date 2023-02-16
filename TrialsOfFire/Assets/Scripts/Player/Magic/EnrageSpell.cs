using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrageSpell : Spell
{
    public EnrageEffect enrageEffect;
    public override void CastSpell(Transform castPoint, float chargeRatio)
    {
        base.CastSpell(castPoint, chargeRatio);
        EnrageEffect effect = new EnrageEffect(enrageEffect, Player.Instance.character);
        effect.maxDuration = effectivePotency;
        Player.Instance.character.effectRunner.AddEffect(effect);
    }
}
