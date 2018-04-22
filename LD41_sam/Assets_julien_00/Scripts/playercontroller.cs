using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float speed = 15;
    public Transform lookHeight;
    Transform player;
    Rigidbody myRigidBody;

    private Vector3 velocity;

    void Start()
    {
        player = transform;
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        velocity = direction * speed;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * lookHeight.position.y);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            transform.LookAt(ray.GetPoint(rayDistance));
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
    }
}