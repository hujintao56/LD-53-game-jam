using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    public GameObject packPrefab; // Pack prefab
    // public AudioClip explosionSound; // Explosion sound

    private bool isCollidingWithGround = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Rocket collided with ground.");
            isCollidingWithGround = true;
            Explode();
        }
    }

    /* private void OnCollisionEnter(Collision collision)
    {
        // find out what hit the Stone
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.CompareTag("Ground"))
        {
            Debug.Log("Rocket collided with ground.");
            isCollidingWithGround = true;
            Explode();
        }
    }*/

    private void Explode()
    {
        // Play explosion sound
        // AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // Disable rocket
        gameObject.SetActive(false);

        // Spawn pack prefab
        Vector3 normal = transform.up;
        Vector3 position = transform.position + (normal * 0.5f);
        GameObject pack = Instantiate(packPrefab, position, Quaternion.identity);
        Rigidbody2D packRb = pack.GetComponent<Rigidbody2D>();
        packRb.AddForce(normal, ForceMode2D.Impulse);
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
        Debug.DrawLine(transform.position, transform.position + (transform.up * 1f), Color.red);
        Debug.DrawLine(transform.position, transform.position - (transform.up * 1f), Color.blue);

    }
}
