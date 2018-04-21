using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {

    public float speed = 15;
    Transform player; 
    

     void Start() {

        player = transform;
    }

    // Update is called once per frame
    void Update () {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        //transform.position += moveAmount;
        transform.Translate(moveAmount);

        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, player.transform.position.y));
        Vector3 forward = mouseWorld - player.transform.position;
        player.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
