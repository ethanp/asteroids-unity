using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    [SerializeField] float timeToSpawn;
    float lastSpawned;

    void Start()
    {
        // Create one asteroid immediately on game start.
        lastSpawned = -timeToSpawn;
    }

    void Update()
    {
        if (Time.time - lastSpawned > timeToSpawn)
        {
            lastSpawned = Time.time;
            GameObject newAsteroid = 
                Instantiate(GameManager.instance.GetRandomRockPrefab());
            newAsteroid.transform.localScale =
                Vector3.one * Random.Range(2f, 8f);
            GameManager.instance.FindEmptyLocationNearPlayer(newAsteroid);
        }
    }
}
