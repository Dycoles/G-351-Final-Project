using UnityEngine;
using UnityEngine.AI;

public class dragonPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // AudioSource for the dragon's roar
    public AudioSource audioSource;

    // Sound trigger distance
    public float soundTriggerDistance = 5f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Check if the player is within the specified distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerWithinSoundDistance = distanceToPlayer <= soundTriggerDistance;

        if (!playerInSightRange && !playerInAttackRange)
        {
            // loop sleeping noise
            Debug.Log("Sleeping");
        }
        if (playerInSightRange && !playerInAttackRange && playerWithinSoundDistance)
        {
            // play wake up animation
            // roar
            // play roar sound
            Roar();
            Debug.Log("Roaring");
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            Debug.Log("Attacking");
        }
    }

    // Playing roaring sound
    private void Roar()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            // Play the roar sound
            audioSource.Play();
            Debug.Log("Roar sound played");
        }
    }

    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
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
            Invoke(nameof(DestroyEnemy), 2f);
        }
    }

    private void DestroyEnemy()
    {
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
