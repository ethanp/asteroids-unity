﻿using System.Collections;
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
            GameObject newAsteroid = Instantiate(asteroidPrefab);
            positionAsteroid(newAsteroid);
        }

    }

    private void positionAsteroid(GameObject asteroid)
    {
        do asteroid.transform.position = createRandomLocation();
        while (GameManager.Instance().OverlapsExistingObject(asteroid));

    }

    private Vector3 createRandomLocation()
    {
        return new Vector3(
            Random.Range(0, 10),
            0,
            Random.Range(0, 10));
    }


}
