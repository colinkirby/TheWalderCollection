using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    private UnityEngine.AI.NavMeshAgent navMesh;
    public Transform Player;
    public Transform[] randomPoints; 

    private float playerDist, randomPointDist;
    public int currentRandomPoint;

    public float perceptionDistance, chaseDistance, attackDistance, walkVelocity, chaseVelocity;

    public bool seeingPlayer; 

    void Start(){
        currentRandomPoint = Random.Range(0, randomPoints.Length);
        navMesh = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update(){
        playerDist = Vector3.Distance(Player.transform.position, transform.position);
        randomPointDist = Vector3.Distance(randomPoints[currentRandomPoint].transform.position, transform.position);

        RaycastHit hit;
        Ray rayCast = new Ray(transform.position, Vector3.forward);

        if (Physics.SphereCast(transform.position, navMesh.height / 2, transform.forward, out hit, 6) && playerDist < perceptionDistance) {
            seeingPlayer = hit.collider.gameObject.CompareTag("Player"); 
        }

        if (seeingPlayer) {
            if (playerDist <= attackDistance) {
                attack();
            }
            else if (playerDist <= chaseDistance) {
                chase();
            }
            else {
                walk();
            }
        } else {
            walk();
        }

        if (randomPointDist <= 8) {
            currentRandomPoint = Random.Range(0, randomPoints.Length);
            walk();
        }
    }

    void walk(){
        navMesh.acceleration = 1;
        navMesh.speed = walkVelocity;
        navMesh.destination = randomPoints[currentRandomPoint].position;
    }

    void chase(){
        transform.LookAt(Player);
        navMesh.acceleration = 5;
        navMesh.speed = chaseVelocity;
        navMesh.destination = Player.position;
    }

    // This actually attack mechanic needs to be added 
    void attack(){
        navMesh.acceleration = 0;
        navMesh.speed = 0;
    }
}
