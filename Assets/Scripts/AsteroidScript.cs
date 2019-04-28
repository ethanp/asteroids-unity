using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private void Update()
    {
        // TODO Maybe: Wrap (instead of destroy) if it goes off screen?
        if (!GameManager.gameBounds.Intersects(
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
        Destroy(collision.gameObject);
        Destroy(gameObject);

        StartCoroutine(createExplosion());
    }

    private IEnumerator createExplosion()
    {
        int COUNT = 10;
        GameObject[] children = new GameObject[COUNT];

        // TODO Rock count & size in explosion should make sense
        // for the size of this asteroid.
        for (int i = 0; i < COUNT; i++)
            children[i] = createExplosionRock();

        yield return new WaitForSeconds(2);

        for (int i = 0; i < COUNT; i++)
            Destroy(children[i]);
    }

    private GameObject createExplosionRock()
    {
        Vector3 loc = Random.onUnitSphere;
        GameObject explosionRock =
                Instantiate(
                    GameManager.instance.GetRandomRockPrefab(),
                    gameObject.transform.position + loc,
                    gameObject.transform.rotation);
        explosionRock.GetComponent<Rigidbody>()
            .AddForce(loc * 1000);
        return explosionRock;
    }
}
