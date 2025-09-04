using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackDistance = 2f;
    public float attackCooldown = 1f;
    public float chaseSpeed = 3.5f;
    private NavMeshAgent agent;
    private float lastAttackTime;
    private bool isDisabled = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(player == null || isDisabled) return;
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance <= attackDistance && Time.time - lastAttackTime > attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    public void Reappear()
    {
        gameObject.SetActive(true);
    }

    void AttackPlayer() => GameManager.Instance.GameOver();

    public void DisableTemporarily(float duration)
    {
        if(isDisabled) return;
        StartCoroutine(DisableRoutine(duration));
    }

    private IEnumerator DisableRoutine(float duration)
    {
        isDisabled = true;
        agent.isStopped = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(duration);
        agent.isStopped = false;
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
        isDisabled = false;
    }
}
