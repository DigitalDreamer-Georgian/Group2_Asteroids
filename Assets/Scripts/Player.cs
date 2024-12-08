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
    public Wrap wrap;
    public AudioSource Teleport;
    public AudioSource fire;
    public AudioSource engine;
    public AudioSource explosion;
    private void Start()
    {
        shipRB = GetComponent<Rigidbody2D>();
        shipRB.drag = 1f;
    }
    private void Update()
    {
        if (isAlive)
        {
            if (wrap.telep == true)
            {
                Teleport.Play();
                wrap.telep = false;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if(!engine.isPlaying)
                engine.Play();
            }
            else
            {
                if (engine.isPlaying)
                {
                    engine.Pause(); 
                }
            }
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(shipRotate * Time.deltaTime * transform.forward);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-shipRotate * Time.deltaTime * transform.forward);
        }

    }

    private void HandleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
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
            fire.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            isAlive = false;
            if (engine.isPlaying)
            {
                engine.Pause();
            }
            explosion.Play();
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }


}