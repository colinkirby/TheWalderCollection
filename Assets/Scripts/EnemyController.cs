using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{   
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] randomPoints; 

    public ThirdPersonCharacter character;

    private float playerDist, randomPointDist;
    private float fieldOfView;
    public int currentRandomPoint;

    public float chaseDistance, attackDistance, walkVelocity, chaseVelocity;
    public GameObject blackOutSquare;

    void Start(){
        agent.updateRotation = false;
        currentRandomPoint = Random.Range(0, randomPoints.Length);
        fieldOfView = 45;
    }

    void Update(){
        playerDist = Vector3.Distance(player.transform.position, transform.position);
        randomPointDist = Vector3.Distance(randomPoints[currentRandomPoint].transform.position, transform.position);

        print(SeeingPlayer());
        if (SeeingPlayer()) {
            if (playerDist <= attackDistance) {
                print("attacking");
                agent.acceleration = 0;
                agent.speed = 0;
                agent.velocity = Vector3.zero;
                StartCoroutine(FadeBlackOutSquare());
            }
            else if (playerDist <= chaseDistance) {
                Chase();
            } else {
                Walk();
            }
        } else {
            if (playerDist <= chaseDistance) {
                Vector3 position = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.LookAt(position);
            } 
            else {
                Walk();
            }
        }

        if (randomPointDist <= 8) {
            currentRandomPoint = Random.Range(0, randomPoints.Length);
            Walk();
        }
    }

    bool SeeingPlayer() {
        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Vector3.Angle(toPlayer, transform.forward) <= fieldOfView) {
            if (Physics.Raycast(transform.position, toPlayer, out hit)) {
                return hit.collider.gameObject.CompareTag("Player");
            }
        }
        return false;
    }

    void Walk(){
        agent.acceleration = 2;
        agent.speed = walkVelocity;
        agent.destination = randomPoints[currentRandomPoint].position;
        if (agent.remainingDistance > agent.stoppingDistance) {
           character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }
    }

    void Chase(){
        Vector3 position = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(position);
        agent.acceleration = 5;
        agent.speed = chaseVelocity;
        agent.destination = player.position;
        if (agent.remainingDistance > agent.stoppingDistance) {
           character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }
    }

    IEnumerator FadeBlackOutSquare(float fadeSpeed = 0.35f) {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        while (blackOutSquare.GetComponent<Image>().color.a < 1) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor; 
            yield return null; 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
