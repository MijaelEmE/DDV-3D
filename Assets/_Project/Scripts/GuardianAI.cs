using UnityEngine;

public class GuardianAI : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private AudioClip alertSound;
    [SerializeField] private AudioClip attackSound;

    private AIStateMachine stateMachine;
    private Transform playerTransform;
    private Rigidbody rb;
    private float lastAttackTime;
    private bool isAttacking;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = GetComponent<AIStateMachine>();

        if (stateMachine == null)
            stateMachine = gameObject.AddComponent<AIStateMachine>();

        // Registrar estados
        stateMachine.RegisterState("Patrol", new PatrolState(stateMachine, this));
        stateMachine.RegisterState("Chase", new ChaseState(stateMachine, this));

        stateMachine.SetState("Patrol");

        // Buscar jugador
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            playerTransform = playerObj.transform;
    }

    public bool CanSeePlayer()
    {
        if (playerTransform == null) return false;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > detectionRange) return false;

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            return hit.transform.CompareTag(playerTag);
        }

        return false;
    }

    public void MoveTowards(Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        float targetVelX = direction.x * speed;
        float targetVelZ = direction.z * speed;

        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            new Vector3(targetVelX, rb.linearVelocity.y, targetVelZ),
            Time.deltaTime * 3f
        );

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * 5f
            );
    }

    public void PlayAlertSound()
    {
        if (alertSound != null)
            AudioSource.PlayClipAtPoint(alertSound, transform.position);
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        isAttacking = true;
        lastAttackTime = Time.time;

        if (attackSound != null)
            AudioSource.PlayClipAtPoint(attackSound, transform.position);

        Invoke(nameof(StopAttack), 0.3f);
    }

    private void StopAttack()
    {
        isAttacking = false;
    }

    public bool IsAttacking() => isAttacking;
    public Transform GetPlayer() => playerTransform;
    public float GetAttackRange() => attackRange;
    public float GetPatrolSpeed() => patrolSpeed;
    public float GetChaseSpeed() => chaseSpeed;
    public PatrolPath GetPatrolPath() => patrolPath;

    // Estados
    private class PatrolState : AIState
    {
        private GuardianAI guardian;
        private Vector3 targetWaypoint;

        public PatrolState(AIStateMachine sm, GuardianAI g) : base(sm)
        {
            guardian = g;
        }

        public override void OnEnter()
        {
            if (guardian.GetPatrolPath() != null)
                targetWaypoint = guardian.GetPatrolPath().GetCurrentWaypoint();
        }

        public override void OnUpdate()
        {
            // Cambiar a Chase si ve al jugador
            if (guardian.CanSeePlayer())
            {
                guardian.PlayAlertSound();
                stateMachine.SetState("Chase");
                return;
            }

            // Patrullar
            if (guardian.GetPatrolPath() != null)
            {
                guardian.MoveTowards(targetWaypoint, guardian.GetPatrolSpeed());

                float distance = Vector3.Distance(guardian.transform.position, targetWaypoint);
                if (distance < guardian.GetPatrolPath().GetWaypointDistance())
                {
                    targetWaypoint = guardian.GetPatrolPath().GetNextWaypoint();
                }
            }
        }
    }

    private class ChaseState : AIState
    {
        private GuardianAI guardian;
        private float losePlayerTime;

        public ChaseState(AIStateMachine sm, GuardianAI g) : base(sm)
        {
            guardian = g;
        }

        public override void OnUpdate()
        {
            Transform player = guardian.GetPlayer();
            if (player == null)
            {
                stateMachine.SetState("Patrol");
                return;
            }

            // Si ve al jugador, perseguir
            if (guardian.CanSeePlayer())
            {
                losePlayerTime = 0;

                // Moverse hacia el jugador
                guardian.MoveTowards(player.position, guardian.GetChaseSpeed());

                // Atacar si está cerca
                if (Vector3.Distance(guardian.transform.position, player.position) < guardian.GetAttackRange())
                {
                    guardian.Attack();
                }
            }
            else
            {
                // Si pierde de vista al jugador, esperar 2 segundos antes de volver a patrulla
                losePlayerTime += Time.deltaTime;
                if (losePlayerTime > 2f)
                {
                    stateMachine.SetState("Patrol");
                }
            }
        }
    }
}
