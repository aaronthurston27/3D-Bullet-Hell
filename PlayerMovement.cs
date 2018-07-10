using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Main Variables

    public float rotationSpeed = 50;
    public float movementSpeed = 5;
    private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
    private Vector3 lastMovement;
    #endregion

    #region MonoBehavior Methods

    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = Vector3.zero;
        lastMovement = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(screenRect.Contains(Input.mousePosition))
            RotateCamera();
        MoveCamera();       
    }

    // If we hit the screen boundary collider, don't move anymore

    #endregion

    #region Methods

    // Rotate the camera.
    private void RotateCamera()
    {
        Debug.Log(lastMovement);
        // Move the camera as needed
        float xRotation = 0;
        xRotation = (Input.mousePosition - lastMovement).x * Time.deltaTime;

        float yRotation = 0;
        yRotation = (Input.mousePosition - lastMovement).y * Time.deltaTime;

        Vector3 Rotation = Camera.main.transform.eulerAngles;
        Rotation.y += (xRotation * rotationSpeed);
        Rotation.x -= (yRotation * rotationSpeed);

        // If we want to reset the camera rotation, Right-Click
        if (Input.GetMouseButtonDown(1))
        {
            Rotation = transform.eulerAngles - transform.eulerAngles;
            Camera.main.transform.eulerAngles = Rotation;
        }
        lastMovement = Input.mousePosition;

        Camera.main.transform.eulerAngles = Rotation;
        this.gameObject.transform.eulerAngles = Rotation;
        
    }

    // Move around the world space.
    private void MoveCamera()
    {
        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left.normalized * movementSpeed * Time.deltaTime);
        }
        // Move Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right.normalized * movementSpeed * Time.deltaTime);
        }
        // Move Forward
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 dir = transform.forward * Time.deltaTime * movementSpeed;
            dir.y = 0;
            transform.position += (dir);
        }
        // Move Backward
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 dir = transform.forward.normalized * Time.deltaTime * movementSpeed;
            dir.y = 0;
            transform.position -= (dir);
        }
       Camera.main.transform.position = this.transform.position;
    }
    #endregion
}
