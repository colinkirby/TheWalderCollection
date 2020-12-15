﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{   
    public NavMeshAgent agent;
    public Transform player;
    public Light playerLantern;
    public Transform[] randomPoints; 

    public ThirdPersonCharacter character;
    public MonsterSoundController monsterSounds;

    private float playerDist, randomPointDist;
    private float fieldOfView;
    public int currentRandomPoint;

    public float walkVelocity, chaseVelocity;
    private float chaseDistance;
    public GameObject blackOutSquare;

    public Canvas anchorCanvas;

    private bool findBells = false;
    private Transform bellsPos;
    
    [System.Serializable] public class DisableMovementEvent : UnityEvent<bool> {}
    [SerializeField] public DisableMovementEvent disableMovementEvent;

    void Start(){
        agent.updateRotation = false;
        currentRandomPoint = Random.Range(0, randomPoints.Length);
        fieldOfView = 90;
    }

    void Update(){
        if (playerLantern.enabled) {
            chaseDistance = 5f;
        } else {
            chaseDistance = 3f;
        }

        playerDist = Vector3.Distance(player.transform.position, transform.position);
        randomPointDist = Vector3.Distance(randomPoints[currentRandomPoint].transform.position, transform.position);

        if (SeeingPlayer()) {
            if (playerDist <= chaseDistance && agent.velocity == Vector3.zero) {
                disableMovementEvent.Invoke(false);
                if (!playerLantern.enabled) {
                    playerLantern.enabled = true;
                }
                anchorCanvas.enabled = false;
                StartCoroutine(FadeBlackOutSquare());
            }
            else if (playerDist <= chaseDistance) {
                Chase();
            } 
            else if (findBells) {
                FindBells();
            }
            else {
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
        agent.acceleration = 1;
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
        agent.acceleration = 4;
        agent.speed = chaseVelocity;
        agent.destination = player.position;
        if (agent.remainingDistance > agent.stoppingDistance) {
           character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }
    }

    void FindBells(){
        agent.acceleration = 4;
        agent.speed = chaseVelocity;
        agent.destination = bellsPos.position;
        if (agent.remainingDistance > agent.stoppingDistance) {
           character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
            findBells = false;
        }
    }

    IEnumerator FadeBlackOutSquare(float fadeSpeed = 0.5f) {
        monsterSounds.Death();
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

    public void HearBells(Transform pos) {
        bellsPos = pos; 
        findBells = true;
    }
}
