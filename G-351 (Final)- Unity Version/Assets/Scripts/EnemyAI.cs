using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    private Animator mAnimator;
    public bool inCombat;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // added by Jennifer
    AudioManager audioManager;
    public bool isDead = false;

    private void Start(){
        inCombat = false;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(inCombat){
            return;
        }
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            // Debug.Log("Patroling");
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            // Debug.Log("Chasing");
        }
        if (playerInSightRange && playerInAttackRange)
        {
            //change JGG
            //audioManager.PlayBlobSFX(audioManager.blobAttack);
            AttackPlayer();
            mAnimator.SetTrigger("Attack");
            // Debug.Log("Attacking");
        }
    }

    public bool isInCombat(bool isIn){
        inCombat = isIn;
        Debug.Log("Enemy is in combat");
        return inCombat;
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            // Debug.Log("Searching for new patrol point");
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            // Debug.Log("Walkpoint is true");
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        audioManager.StartFighting();
        audioManager.PlayBlobSFX(audioManager.blobAttack);
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            /// Attack code here

            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
            audioManager.EndFighting();
            Invoke(nameof(DestroyEnemey), 2f);
        }
    }

    private void DestroyEnemey()
    {
        // Stop fighting song when the enemy is destroyed
        if (isDead)
        {
            audioManager.EndFighting();
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}