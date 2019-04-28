using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private void Update()
    {
        // TODO Maybe: Wrap (instead of destroy) if it goes off screen?
        if (GameManager.gameBounds.Intersects(
            GetComponent<Collider>().bounds))
        {
            Debug.Log("Removing asteroid that went offscreen.");
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // TODO add some force to it to make it move
        // around. Make the speed go up AND down over time.
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Asteroid got hit.");

        if (gameObject.transform.localScale.z > 3)
        {
            Debug.Log("Creating children asteroids.");

            // Create children.
            GameObject leftChild = 
                Instantiate(GameManager.instance.GetRandomRockPrefab());
            GameObject rightChild = 
                Instantiate(GameManager.instance.GetRandomRockPrefab());

            // Set parent.
            leftChild.transform.parent = transform.parent;
            rightChild.transform.parent = transform.parent;

            // Set position.
            leftChild.transform.position = new Vector3(
                transform.position.x - 1,
                transform.position.y,
                transform.position.z);
            rightChild.transform.position = new Vector3(
                transform.position.x + 1,
                transform.position.y,
                transform.position.z);

            // Set scale.
            Vector3 smallerScale = transform.localScale * .7f;
            leftChild.transform.localScale = smallerScale;
            rightChild.transform.localScale = smallerScale;

            // TODO set velocity (or rotation, just match asteroid movement).
        }
        Destroy(gameObject);
        Destroy(collision.gameObject);

        createExplosion();
    }

    private void createExplosion()
    {
        // TODO Num rocks in explosion should make sense
        // for the size of this asteroid.
        for (int i = 0; i < 0; i++)
        {
            GameObject explosionRock = 
                Instantiate(GameManager.instance.GetRandomRockPrefab());
            // TODO add force to it in a random direction.
            // TODO Destroy it after a few seconds.
        }
    }
}
