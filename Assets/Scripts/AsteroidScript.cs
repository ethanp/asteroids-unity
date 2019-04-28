using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;

    private void Update()
    {
        // TODO destroy it if it goes off screen.
    }

    private void FixedUpdate()
    {
        // TODO add some force to it to make it move
        // around. Make the speed go up AND down over time.
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Asteroid got hit.");

        if (gameObject.transform.localScale.z > 1)
        {
            Debug.Log("Creating children asteroids.");

            // Create children.
            GameObject leftChild = Instantiate(asteroidPrefab);
            GameObject rightChild = Instantiate(asteroidPrefab);

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

            // TODO set velocity.
        }
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
