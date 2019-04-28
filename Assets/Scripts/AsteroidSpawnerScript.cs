using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    private float lastSpawned;

    private void Start()
    {
        // Create one asteroid immediately on game start.
        lastSpawned = -timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawned > timeToSpawn)
        {
            lastSpawned = Time.time;
            GameObject newAsteroid = 
                Instantiate(GameManager.instance.GetRandomRockPrefab());
            newAsteroid.transform.localScale =
                Vector3.one * Random.Range(2f, 8f);
            positionAsteroid(newAsteroid);
        }

    }

    private void positionAsteroid(GameObject asteroid)
    {
        asteroid.transform.position = createRandomLocation();

        // TODO This should be a `while`-loop, but it was infinite-looping.
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
