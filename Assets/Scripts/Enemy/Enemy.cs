using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int number;
    [SerializeField]
    private TextMeshProUGUI textNumber;
    
    
    [Header("MOVEMENT PARAMETERS")]
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rangeMovement = 1f;
   protected virtual void Start()
   {
       target = GameObject.FindWithTag("Player").transform;
   }

  
   protected virtual void Update()
   {
        MoveToPlayer();
        RotateTowardPlayer();
   }

   protected virtual void MoveToPlayer()
   {
       if (Vector3.Distance(transform.position, target.position) > rangeMovement)
       {
           transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
       }
   }

   protected virtual void RotateTowardPlayer()
   {
       var lookForward = target.position - transform.position;
       transform.rotation = Quaternion.LookRotation(lookForward, Vector3.up);
   }
    
}
