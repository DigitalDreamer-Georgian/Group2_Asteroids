using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    public int asteroids = 0;
    private int level = 0;
    public AudioSource explosion;
    void Update()
    {
        if (asteroids == 0)
        {
            level++;

            int numAsteroids = 2 + (2 * level);
            for (int i = 0; i < numAsteroids; i++)
            {
                SpawnAsteroid();
            }
        }
    }
    private void SpawnAsteroid()
    {
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0);
        } else if (edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1);
        } else if (edge == 2)
        {
            viewportSpawnPosition = new Vector2(0, offset);
        } else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1, offset);
        }
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.gameManager = this;
    }
    public void asteroidExplode()
    {
        asteroids--;
        explosion.Play();
    }

    public void GameOver()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        Debug.Log("You Died: Game Over");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }
}
