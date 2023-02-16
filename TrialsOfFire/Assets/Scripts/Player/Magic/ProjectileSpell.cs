using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : Spell
{
    protected ProjectileSpellSO baseProjectile;
    [SerializeField] protected Projectile projectile;
    protected ProjectileSpell instantiatedSpell;
    public override void CastSpell(Transform castPoint, float chargeRatio)
    {
        base.CastSpell(castPoint, chargeRatio);
        instantiatedSpell = Instantiate(this, castPoint.position, castPoint.rotation).GetComponent<ProjectileSpell>();
        instantiatedSpell.SetEffectivePotency(chargeRatio);
        instantiatedSpell.baseProjectile = baseStats as ProjectileSpellSO;
        instantiatedSpell.projectile.OnHitDetected = instantiatedSpell.OnCollision;
        instantiatedSpell.projectile.Fire(Player.Instance.mainCamera.transform.forward, instantiatedSpell.baseProjectile.speed);
    }

    public virtual void OnCollision(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Health health = other.GetComponent<Health>();
            //if (health != null)
            //{
            //    health.Damage(effectivePotency);
            //}
           
            EnemyHealthReference reference = other.GetComponent<EnemyHealthReference>();
            if (reference != null)
            {
                reference.enemyHealth.Damage(effectivePotency);
            }
            AfterEnemyHit(reference);
            

        }
        if (other.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }

    public virtual void AfterEnemyHit(EnemyHealthReference reference)
    {

    }

}
