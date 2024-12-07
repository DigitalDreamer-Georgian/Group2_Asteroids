using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float shipAccel = 10f;
    [SerializeField] private float shipMaxVel = 10f;
    [SerializeField] private float shipRotate = 180f;
    [SerializeField] private float bulletSpeed = 8f;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;

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
            HandleShoot();
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

    private void HandleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Vector2 shipVel = shipRB.velocity;
            Vector2 shipDir = transform.up;
            float ForwardSpeed = Vector2.Dot(shipVel, shipDir);
            if (ForwardSpeed < 0)
            {
                ForwardSpeed = 0; 
            }
            bullet.velocity = shipDir * ForwardSpeed;
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            isAlive = false;
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }
}