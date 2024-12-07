using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float shipAccel = 10f;
    [SerializeField] private float shipMaxVel = 10f;
    [SerializeField] private float shipRotate = 180f;

    private Rigidbody2D shipRB;
    private bool isAlive = true;
    private bool isAccelerating = false;

    private void Start()
    {
        shipRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isAlive)
        {
            HandleShipAccel();
            HandleShipRotate();
        }
    }
    private void FixedUpdate()
    {
        if (isAlive && isAccelerating)
        {
            shipRB.AddForce(shipAccel * transform.up);
            shipRB.velocity = Vector2.ClampMagnitude(shipRB.velocity, shipMaxVel);
        }
    }
    private void HandleShipAccel()
    {
        isAccelerating = Input.GetKey(KeyCode.UpArrow);
    }

    private void HandleShipRotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(shipRotate * Time.deltaTime * transform.forward);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-shipRotate * Time.deltaTime * transform.forward);
        }

    }
}