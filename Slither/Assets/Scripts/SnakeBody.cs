 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private int order;
    private Transform head;

    private Vector3 moveVelocity;
    [Range(0.1f,1.0f)]
    public float followSpeed = 0.27f;

    private void Start()
    {
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        for(int i=0;i<head.GetComponent<SnakeMovement>().bodyParts.Count;i++)
        {
            if(gameObject==head.GetComponent<SnakeMovement>().bodyParts[i].gameObject)
            {
                order = i;
            }
        }
    }

    void FixedUpdate()
    {
        if (order == 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.position, ref moveVelocity, followSpeed);
            transform.LookAt(head.transform.position);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.GetComponent<SnakeMovement>().bodyParts[order - 1].position, ref moveVelocity, followSpeed);
            transform.LookAt(head.transform.position);
        }
    }

}
