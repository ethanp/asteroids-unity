using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{

    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private float timeToSpawn;
    private float lastSpawned = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawned > timeToSpawn)
        {
            lastSpawned = Time.time;
            if (asteroidPrefab != null)
            {
                GameObject newAsteroid = Instantiate(asteroidPrefab);
                positionAsteroid(newAsteroid);
            }
            else
            {
                Debug.LogWarning("asteroidPrefab was null.");
            }
        }

    }

    private void positionAsteroid(GameObject asteroid)
    {
        if (GameManager.instance == null)
        {
            Debug.LogWarning("Found null instance.");
            Destroy(asteroid);
            return;
        }
        asteroid.transform.position = createRandomLocation();

        if (GameManager.instance.OverlapsExistingObject(asteroid))
        {
            Debug.Log("Going to another spot.");
            asteroid.transform.position = createRandomLocation();
        }

        Debug.Log("Found a good spot.");

    }

    private Vector3 createRandomLocation()
    {
        return new Vector3(
            Random.Range(-5, 5),
            Random.Range(-5, 5),
            0);
    }


}
