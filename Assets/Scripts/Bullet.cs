using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLife = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Destroy(gameObject, bulletLife);
    }
}
