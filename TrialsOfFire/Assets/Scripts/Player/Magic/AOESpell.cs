using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpell : ProjectileSpell
{
    public GameObject explosionEffect;
    public float damageRange;
    public float AOEDamage;

    public override void OnCollision(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.Damage(effectivePotency);
            }
            else
            {
                EnemyHealthReference reference = other.GetComponent<EnemyHealthReference>();
                if (reference != null)
                {
                    reference.enemyHealth.Damage(effectivePotency);
                }
            }
        }
        if (other.tag != "Projectile")
        {
            DoAOEDamage(transform.position);
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * damageRange;
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    public void DoAOEDamage(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, damageRange);
        List<Health> enemyHealth = new List<Health>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Health healf = colliders[i].GetComponent<EnemyHealthReference>()?.enemyHealth;
            if(healf != null && !enemyHealth.Exists(x => Object.ReferenceEquals(x, healf)))
            {
                Debug.Log(healf.gameObject.name);
                enemyHealth.Add(healf);
            }
        }
        float dmg = AOEDamage * (effectivePotency / baseStats.potency);
        enemyHealth.ForEach(health => health.Damage(dmg));

    }
    
}
