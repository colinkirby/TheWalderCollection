using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    public Transform Player;
    float MoveSpeed = 2f;
    int MinDist = 5;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) > MinDist) {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
    }
}
