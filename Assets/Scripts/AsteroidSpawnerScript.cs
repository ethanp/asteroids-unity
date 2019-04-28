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
            GameManager.instance.FindPosition(newAsteroid);
        }

    }
}
