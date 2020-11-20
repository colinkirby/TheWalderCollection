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

        Vector3 startRay = transform.position;
        Vector3 endRay = Player.transform.position;
        Vector3 direction = endRay - startRay;

        if (Physics.Raycast(transform.position, direction, out hit, 1000) && playerDist < perceptionDistance) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                seeingPlayer = true;
            } else {
                seeingPlayer = false;
            }
        }

        if (playerDist > perceptionDistance) {
            walk();
        }

        if (playerDist <= perceptionDistance && playerDist > chaseDistance) {
            if (seeingPlayer == true){
                look();
            } else {
                walk();
            }
        }

        if (playerDist <= chaseDistance && playerDist > attackDistance) {
            if (seeingPlayer == true) {
                chase();
            } else {
                walk();
            }
        }

        if (playerDist <= attackDistance && seeingPlayer == true) {
            attack();
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

    void look(){
        navMesh.speed = 0;
        transform.LookAt(Player);
    }

    void chase(){
        navMesh.acceleration = 5;
        navMesh.speed = chaseVelocity;
        navMesh.destination = Player.position;
    }

    void attack(){
        navMesh.acceleration = 0;
        navMesh.speed = 0;
    }

}
