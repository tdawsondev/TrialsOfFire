using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DragonStates
{
    Flying_FindingLocation,
    Flying_ChasingPlayer,
    Flying_FiringAtPlayer,
    Landed_Swinging1,
    Landed_Swinging2,
}

public class Dragon : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    public Animator animator;
    public Animator flightAnimator;
    public Transform damageBoxPostion;
    public float damage;
    public float damageRadius;

    public DragonStates currentState;

    private Transform targetPlayer;

    public bool flying = true;
    public LookAtTarget lookingAtPlayer;
    public Health health;
    private void Awake()
    {
        health.onHealthChange += Damaged;
    }

    private void Damaged(float amount, Transform changedBy = null)
    {
        HUDController.instance.UpdateBossBar(health);
        if (health.Dead)
        {
            Land();
            animator.SetBool("Dead", true);
            agent.enabled = false;

        }
    }
    private void OnDestroy()
    {
        health.onHealthChange -= Damaged;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = FindObjectOfType<Player>().transform;

        flying = true;

        currentState = DragonStates.Flying_FindingLocation;
        startedFindingLocation = false;
        startedLanding = false;

        HUDController.instance.UpdateBossBar(health);

    }

    // Update is called once per frame
    void Update()
    {
        if (health.Dead)
        {
            return;
        }
        switch (currentState)
        {
            case DragonStates.Flying_FindingLocation:
                FlyingFindingLocation();
                break;
            case DragonStates.Flying_ChasingPlayer:
                FlyingChasingPlayer();
                break;
            case DragonStates.Flying_FiringAtPlayer:
                FlyingFiringAtPlayer();
                break;
            case DragonStates.Landed_Swinging1:
                LandedSwinging1();
                break;
        }
    }

    bool startedFindingLocation = false;
    [Header("RandomLocation")]
    public float maxRandomDistance = 200f;
    public Vector3 targetPoint;
    public void FlyingFindingLocation()
    {
        if (!startedFindingLocation)
        {
            FindLocation();
            lookingAtPlayer.allowUpdate = false;
            startedFindingLocation=true;
        }
        agent.SetDestination(targetPoint);
        agent.stoppingDistance = 5f;


        if(Vector3.Distance(targetPoint, transform.position) <= agent.stoppingDistance)
        {
            currentState = DragonStates.Flying_ChasingPlayer;
            //resetstuff
            startedFindingLocation = false;
        }
        
        

    }
    public void FindLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxRandomDistance;
        randomDirection += Player.Instance.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxRandomDistance, 1);
        targetPoint = hit.position;
    }

    bool startedChasingPlayer = false;
    public float chasingLandDistance = 10f;
    public void FlyingChasingPlayer()
    {
        if (!startedChasingPlayer)
        {
            lookingAtPlayer.allowUpdate = true;
            startedChasingPlayer = true;
        }

        agent.SetDestination(targetPlayer.position);
        agent.stoppingDistance = chasingLandDistance;

        if(Vector3.Distance(transform.position, targetPlayer.position) <= agent.stoppingDistance)
        {
            Debug.Log("Done Chasing");
            currentState = DragonStates.Landed_Swinging1;
            startedChasingPlayer = false;
        }

    }

    public void FlyingFiringAtPlayer()
    {

    }

    bool startedLanding = false;
    public void LandedSwinging1()
    {
        if (!startedLanding)
        {
            animator.SetTrigger("GroundAttack1");
            Land();
            startedLanding = true;
        }
    }


    public void Land()
    {
        flightAnimator.SetBool("Grounded", true);
    }

    public void TakeOff()
    {
        flightAnimator.SetBool("Grounded", false);
    }

    public void AfterGroundAttack()
    {
        TakeOff();
        startedLanding=false;
        currentState = DragonStates.Flying_FindingLocation;
    }

    public void CheckDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(damageBoxPostion.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Player.Instance.health.Damage(damage, transform);
            }
        }
    }

}
