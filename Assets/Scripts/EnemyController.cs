using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    private UnityEngine.AI.NavMeshAgent navMesh;
    public Transform Player;
    public Light playerLight;
    public Transform[] randomPoints; 

    private float playerDist, randomPointDist;
    private float fieldOfView;
    public int currentRandomPoint;

    public float chaseDistance, attackDistance, walkVelocity, chaseVelocity;
    public bool seeingPlayer; 

    void Start(){
        currentRandomPoint = Random.Range(0, randomPoints.Length);
        navMesh = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        fieldOfView = 45;
    }

    void Update(){
        if (Player.speed > 3f) {
            // Change enemy speed or FOV 
        }

        if (playerLight.enabled == true) {
            // Change enemy speed or FOV 
        }

        playerDist = Vector3.Distance(Player.transform.position, transform.position);
        randomPointDist = Vector3.Distance(randomPoints[currentRandomPoint].transform.position, transform.position);

        Vector3 toPlayer = (Player.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Vector3.Angle(toPlayer, transform.forward) <= fieldOfView) {
            if (Physics.Raycast(transform.position, toPlayer, out hit)) {
                seeingPlayer = hit.collider.gameObject.CompareTag("Player");
            }
        } else {
            seeingPlayer = false;
        }

        if (seeingPlayer) {
            if (playerDist <= attackDistance) {
                attack();
            }
            else if (playerDist <= chaseDistance) {
                chase();
            } else {
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
