using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Transform lookHeight;

    private Rigidbody myRigidBody;
    private Vector3 velocity;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    //---------------------------------------------------------------------------------------------------
    // Gets the input and interacts with it
    //---------------------------------------------------------------------------------------------------
    public void Move(float speed)
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        velocity = direction * speed;
    }

    //----------------------------------------------------------------------------------------------------
    // Shoots a ray to the ground to determine where to look
    //----------------------------------------------------------------------------------------------------
    public void LookAtCursor(Transform graphics)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * lookHeight.position.y);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            graphics.LookAt(ray.GetPoint(rayDistance));
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
    }
}