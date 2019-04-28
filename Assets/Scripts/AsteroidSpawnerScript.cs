using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{

    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private float timeToSpawn;
    private float lastSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawned > timeToSpawn)
        {
            GameObject newAsteroid = Instantiate(asteroidPrefab);
            // TODO find a good location to put it.
        }
        
    }
}
