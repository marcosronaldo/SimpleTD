using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hp = 100;
    public float speed = 1f;
    public bool dead;

    [HideInInspector] public NavMeshAgent navmeshAgent;

    private float slowTimer;

    public bool HasArrivedAtDestination =>
        navmeshAgent.isOnNavMesh &&
        !navmeshAgent.pathPending &&
        navmeshAgent.remainingDistance <= navmeshAgent.stoppingDistance &&
        (!navmeshAgent.hasPath || Mathf.Approximately(navmeshAgent.velocity.sqrMagnitude, 0f));

    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        CameraManager.Instance.AddTarget(transform);
    }

    private void Start()
    {
        navmeshAgent.speed = speed;
        navmeshAgent.SetDestination(GameManager.Instance.mapManager.map.goal);
    }

    private void Update()
    {
        if (dead)
            return;

        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
        }
        else if (slowTimer < 0)
        {
            slowTimer = 0;
            navmeshAgent.speed = speed;
        }

        if (navmeshAgent.path.status != NavMeshPathStatus.PathComplete)
            GameManager.Instance.Lose();

        if (HasArrivedAtDestination)
        {
            dead = true;
            GameManager.Instance.enemies.Remove(this);
            Destroy(gameObject);
            GameManager.Instance.currentHP--;
        }
    }

    private void OnDestroy()
    {
        CameraManager.Instance.RemoveTarget(transform);
    }

    private void OnCollisionEnter(Collision other)
    {
        var shot = other.gameObject.GetComponent<Shot>();

        if (shot)
        {
            var tower = shot.tower;
            if (tower.slow)
            {
                navmeshAgent.speed = speed / 2f;
                slowTimer = 2;
            }

            hp -= tower.damage;
            Destroy(other.gameObject, 1f);
            if (hp <= 0)
            {
                tower.inRange.Remove(this);
                GameManager.Instance.GainEnergyOnKill();
                GameManager.Instance.enemies.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}