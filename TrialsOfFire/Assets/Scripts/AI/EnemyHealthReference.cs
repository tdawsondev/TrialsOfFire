using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a reference to the base health of the enemy. Serves as a way to have multiple colliders associated with one health pool.
/// </summary>
public class EnemyHealthReference : MonoBehaviour
{
    public Health enemyHealth;
    public Character character;
}
