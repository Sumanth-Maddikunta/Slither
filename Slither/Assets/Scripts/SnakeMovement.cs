using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SnakeMovement : MonoBehaviour
{

    public float snakeSpeed = 3.5f;
    public float currentRotation;
    public float rotationSpeed = 20f;

    [Range(0.1f,1.0f)]
    public float smoothTime = 0.1f;

    public List<Transform> bodyParts = new List<Transform>();
    public Transform bodyPrefab;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //I/O for rotation
          
        
	}
    private void FixedUpdate()
    {
        Movement();
        CameraFollow();
    }

    void Movement()
    {
        this.transform.position += transform.up * snakeSpeed * Time.deltaTime;
        currentRotation += rotationSpeed * Time.deltaTime * (-Input.GetAxisRaw("Horizontal"));
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation));
    }

    void CameraFollow()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Vector3 cameraVelocity = Vector3.zero;
        camera.position = Vector3.SmoothDamp(camera.position,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,camera.position.z),ref cameraVelocity,smoothTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if(other.transform.tag=="Orb")
        {
            Debug.Log("Food");
            Destroy(other.gameObject);
            if (bodyParts.Count == 0)
            {
                Vector3 currentPos = transform.position;
                Transform newBodyPart = Instantiate(bodyPrefab, currentPos, Quaternion.identity) as Transform;
                bodyParts.Add(newBodyPart);

            }
            else
            {
                Vector3 currentPos = bodyParts[bodyParts.Count - 1].position;
                Transform newBodyPart = Instantiate(bodyPrefab, currentPos, Quaternion.identity) as Transform;
                bodyParts.Add(newBodyPart);
            }
        }
    }
}
