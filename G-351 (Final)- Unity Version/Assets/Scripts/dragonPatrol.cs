using UnityEngine;
using UnityEngine.AI;

public class dragonPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    bool roar = true;

    private Animator mAnimator;

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

    // added by Jennifer
    AudioManager audioManager;
    public bool isDead = false;

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
            mAnimator.SetTrigger("Roar");
            // roar
            // play roar sound
            Roar();
            Debug.Log("Roaring");
        }
        if (playerInSightRange && playerInAttackRange)
        {
            mAnimator.SetTrigger("Attack");
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
            if (roar == true)
            {
                audioSource.Play();
                roar = false;
            }
            Debug.Log("Roar sound played");
        }
    }

    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        audioManager.StartFighting();
        audioManager.PlayBlobSFX(audioManager.dragonAttack);
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
            Invoke(nameof(DestroyEnemy), 2f);
        }
    }

    private void DestroyEnemy()
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