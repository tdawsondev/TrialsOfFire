using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    //Named this way for a specific reason
    public LayerMask playerShape, isTheGround;

    [SerializeField]
    private Transform targetPlayer;

    [SerializeField]
    private float patrolRange;

    [SerializeField]
    private float sightRange;

    public float stoppingDistance;

    bool walkPointSet;
    private Vector3 walkPoint;

    bool playerInSight;
    public Animator animator;

	private void Awake()
	{
        agent = GetComponent<NavMeshAgent>();
	}


	// Start is called before the first frame update
	void Start()
    {
		targetPlayer = Player.Instance.transform;
	}

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerShape);

        if (!playerInSight)
        {
			Roaming();
        }
        else{
            // player in sight.
			ChaseTarget();

		}
    }

    public void ChaseTarget()
    {
        agent.SetDestination(targetPlayer.position);
        agent.stoppingDistance = stoppingDistance;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetTrigger("Swing");
        }
    }

    private void Roaming() {
		if (!walkPointSet) ChooseLocation();

        if (walkPointSet) {
            agent.SetDestination(walkPoint);
            agent.stoppingDistance = 0;
        
        };

        if ((transform.position - walkPoint).magnitude < 1f) 
            walkPointSet = false;
	}

    private void ChooseLocation(){
        float randomX = Random.Range(-patrolRange, patrolRange);
		float randomZ = Random.Range(-patrolRange, patrolRange);


        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
		agent.SetDestination(walkPoint);

        if(Physics.Raycast(walkPoint, -transform.up, isTheGround))
			walkPointSet = true;



	}

    public void CheckDamage()
    {
        Debug.Log("Check Damage");
    }

    //Visual degugging
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
		
	}
}
