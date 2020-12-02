using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{   
    private UnityEngine.AI.NavMeshAgent navMesh;
    public Transform Player;
    public Transform[] randomPoints; 

    private float playerDist, randomPointDist;
    private float fieldOfView;
    public int currentRandomPoint;

    public float chaseDistance, attackDistance, walkVelocity, chaseVelocity;
    public bool seeingPlayer; 

    public GameObject attackSequence;
    private PlayableDirector director;
    public GameObject blackOutSquare;
    private bool fading = false;

    void Start(){
        currentRandomPoint = Random.Range(0, randomPoints.Length);
        navMesh = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        fieldOfView = 45;
        director = attackSequence.GetComponent<PlayableDirector>();
    }

    void Update(){
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
                navMesh.acceleration = 0;
                navMesh.speed = 0;
                Attack();
                if (!fading) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                } 
            }
            else if (playerDist <= chaseDistance) {
                Chase();
            } else {
                Walk();
            }
        } else {
            Walk();
        }

        if (randomPointDist <= 8) {
            currentRandomPoint = Random.Range(0, randomPoints.Length);
            Walk();
        }
    }

    void Walk(){
        navMesh.acceleration = 1;
        navMesh.speed = walkVelocity;
        navMesh.destination = randomPoints[currentRandomPoint].position;
    }

    void Chase(){
        transform.LookAt(Player);
        navMesh.acceleration = 5;
        navMesh.speed = chaseVelocity;
        navMesh.destination = Player.position;
    }

    void Attack() {
        director.Play();
        StartCoroutine(FadeBlackOutSquare());
    }

    IEnumerator FadeBlackOutSquare(float fadeSpeed = 1) {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;
        fading = true;

        while (blackOutSquare.GetComponent<Image>().color.a < 1) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor; 
            yield return null; 
        }
        fading = false;
    }
}
