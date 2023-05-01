using System.Collections;
using System.Collections.Generic;
using Camera;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    public CameraManager cameraManager;
    
    public GameObject packPrefab; // Pack prefab
    public AudioClip explosionSound; // Explosion sound
    public GameObject explosionPrefab; // Explosion prefab
    
    public int explosionCount = 6;

    [SerializeField]
    private float packExplosionForceMult = 1f;

    private bool isCollidingWithGround = false;
    
    void Start()
    {
        
    }
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Rocket collided with ground.");
            isCollidingWithGround = true;
            explosionCount--;
            if (explosionCount <= 6)
            {
                Debug.Log("Game over! Too many landings.");
            }
            Explode();
        }
    }

    private void Explode()
    {
        // Play explosion sound
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // Disable rocket
        gameObject.SetActive(false);

        // Spawn explosion prefab
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem explosionParticles = explosion.GetComponent<ParticleSystem>();
        explosionParticles.Play();

        // Spawn pack prefab
        Vector3 normal = transform.up;
        Vector3 position = transform.position + (normal * 0.5f);
        GameObject pack = Instantiate(packPrefab, position, Quaternion.identity);
        cameraManager.FollowPackage(pack);
        Rigidbody2D packRb = pack.GetComponent<Rigidbody2D>();
        packRb.AddForce(normal * packExplosionForceMult, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        // Check if rocket is colliding with ground and disable it
        if (isCollidingWithGround)
        {
            // gameObject.SetActive(false);
            // Destroy rocket
            Destroy(gameObject);
        }
        //Debug.DrawLine(transform.position, transform.position + (transform.up * 1f), Color.red);
        //Debug.DrawLine(transform.position, transform.position - (transform.up * 1f), Color.blue);
    }
}
