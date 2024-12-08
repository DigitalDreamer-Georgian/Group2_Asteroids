using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int size = 3;

    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = 0.5f * size * Vector3.one;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
        gameManager.asteroids++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gameManager.asteroids--;
            Destroy(collision.gameObject);
            if (size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Asteroid newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAsteroid.size = size - 1;
                    newAsteroid.gameManager = gameManager;
                }
            }
            Destroy(gameObject);
        }
    }
}