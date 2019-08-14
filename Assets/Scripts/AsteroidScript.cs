using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    Vector3 direction;

    [SerializeField] Material explodedAsteroidMaterial;

    void Start()
    {
        Vector3 notYet = Random.onUnitSphere;
        direction = new Vector3(notYet.x, notYet.y, 0);
    }

    void Update()
    {
        if (!GameManager.gameBounds
                .Contains(
                    gameObject.transform.position))
        {
            Debug.Log("Removing asteroid that went offscreen.");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(direction);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ship"))
        {
            GameManager.instance.ShipDied(collision.gameObject);
            return;
        }

        GameManager.instance.ScoreAsteroidHit();

        if (gameObject.transform.localScale.z > 3)
        {
            GameObject child =
                Instantiate(
                    gameObject,
                    transform.position, 
                    transform.rotation, 
                    transform.parent);

            child.transform.localScale = transform.localScale * .7f;

            child
                .GetComponent<Rigidbody>()
                .AddForce(
                    collision.relativeVelocity * 5);
        }
        Destroy(collision.gameObject);
        Destroy(gameObject);

        StartCoroutine(createExplosion());
    }

    IEnumerator createExplosion()
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

    GameObject createExplosionRock()
    {
        Vector3 loc = Random.onUnitSphere;
        GameObject explosionRock =
                Instantiate(
                    GameManager.instance.GetRandomRockPrefab(),
                    gameObject.transform.position + loc,
                    gameObject.transform.rotation);

        explosionRock.GetComponent<Renderer>().material = 
            explodedAsteroidMaterial;
            
        explosionRock.GetComponent<Rigidbody>()
            .AddForce(loc * 1000);
        return explosionRock;
    }
}
