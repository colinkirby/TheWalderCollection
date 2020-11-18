using UnityEngine;
using System.Collections;
 
public class EnemyWanderer : MonoBehaviour {

    public float speed; 
    public float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    public UnityEngine.AI.NavMeshAgent agent;

    void Start(){
        agent = GetComponent("NavMeshAgent") as UnityEngine.AI.NavMeshAgent;
        agent.speed = speed;
    }

    void Update(){
        if (waitTime < 0){
            randomSpot = Random.Range(0, moveSpots.Length);
            agent.SetDestination(moveSpots[randomSpot].position);
            waitTime = startWaitTime; 
        } else {
            waitTime -= Time.deltaTime;
        }
    }

    // public Transform player;
    // private Vector3 startPosition;
    // private bool chasing = false;
    // private float wanderSpeed = 1f;
    // private float wanderRange = 20f; 
    // private int minDist = 5;
    // private UnityEngine.AI.NavMeshAgent agent;
    // public RaycastHit hit; 

    // void Start(){
    //     agent = GetComponent("NavMeshAgent") as UnityEngine.AI.NavMeshAgent;
    //     agent.speed = wanderSpeed;  
    //     startPosition = transform.position;
    //     InvokeRepeating("Move", 1f, 5f);
    // }

    // public void Update(){ 
    //     transform.LookAt(player);
    //     if (Vector3.Distance(transform.position, player.position) < minDist) {
    //         chasing = true;
    //     }
    //     else {
    //         chasing = false;
    //     }
    // }

    // void Move(){
    //     if (chasing) {
    //         transform.position += transform.forward * wanderSpeed * Time.deltaTime;
    //     }
    //     else {
    //         Vector3 destination = startPosition + new Vector3(Random.Range (-wanderRange, wanderRange), 0, 
    //                                                           Random.Range (-wanderRange, wanderRange));
    //         agent.SetDestination(destination);
    //     }
    // }
}
