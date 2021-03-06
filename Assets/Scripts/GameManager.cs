﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    readonly GameObject[] rockPrefabs = new GameObject[11];

    int score = 0;
    int livesRemaining = 3;

    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI livesRemainingText;
    [SerializeField] TMPro.TextMeshProUGUI youLostText;

    // TODO: Remove this.
    [SerializeField] float asteroidSpawnRange = 20;


    // NB: `readonly` makes it so it can't be set in the Unity editor.
    public static readonly Bounds gameBounds =
        new Bounds(
            center: Vector3.zero,
            size: Vector3.one * 3000);

    public GameObject ship_;
    public AudioManagerScript audioManager_;

    // TODO(feature: asteroids spawn near player): We gotta change this!
    public void FindEmptyLocationNearPlayer(GameObject gameObject)
    {
        gameObject.transform.position = createRandomLocationNearPlayer();

        // TODO This should be a `while`-loop, but it was infinite-looping.
        if (GameManager.instance.OverlapsExistingObject(gameObject))
        {
            Debug.Log("Going to another spot.");
            gameObject.transform.position = createRandomLocationNearPlayer();
        }
        var msg = "Done looking for spots.";
        Debug.Log(msg);
    }

    public void ShipDied(GameObject ship)
    {
        livesRemainingText.text = "Lives Remaining: " + --livesRemaining;
        if (livesRemaining == 0)
        {
            youLostText.alpha = 255;
            audioManager_.PlayUserLost();
            Destroy(ship);
        }
        else
        {
            audioManager_.PlayShipExplosion();
            FindEmptyLocationNearPlayer(ship);
        }
    }

    public void ScoreAsteroidHit()
    {
        scoreText.text = "Score: " + ++score;
        audioManager_.PlayAsteroidExplosion();
    }

    public GameObject GetRandomRockPrefab()
    {
        return rockPrefabs[Random.Range(0, 10)];
    }


    static Bounds bounds(GameObject gameObject)
    {
        return gameObject.GetComponent<Collider>().bounds;
    }

    void Start()
    {
        ship_ = getShip();
        audioManager_ = getAudioManager();

        // Load rock prefabs.
        for (int i = 1; i <= 11; i++)
            rockPrefabs[i - 1] =
                Resources.Load(
                    "Prefabs/Rock" + i)
                    as GameObject;

        // TODO FIXME: Load bullets. I don't think the path is right.
        // The formatting is definitely working though.
        var bulletPrefabs = new List<GameObject>(100);
        for (int i = 1; i <= 100; i++)
        {
            try
            {
                var str = string.Format("Audio/Bullets_01-{0:D2}", i);
                Debug.Log("Str: " + str);
                var rsrc = Resources.Load(str) as GameObject;
                Debug.Log("rsrc: " + rsrc);
                bulletPrefabs.Add(rsrc);
            }
            catch
            {
                // TODO: Print out the exception.
                Debug.LogWarning("An exception was caught.");
                continue;
            }
        }
    }

    // This is recommended here:
    //
    // https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    //
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject); // Persist between scenes.
    }

    // TODO This is buggy and I don't know why.
    bool OverlapsExistingObject(GameObject newObject)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject existingObject in allObjects)
        {
            if (newObject != existingObject)
            {
                Debug.Log("Checking collision with " + existingObject);
                if (existingObject.activeInHierarchy)
                {
                    if (existingObject.GetComponent<Collider>() != null)
                    {
                        Debug.Log("Checking bounds.");
                        if (bounds(newObject).Intersects(bounds(existingObject)))
                        {
                            Debug.Log("It does intersect.");
                            return true;
                        }
                        Debug.Log("It does NOT intersect.");
                    }
                } else
                {
                    Debug.Log("Actually, it's disabled.");
                }
            }
        }
        return false;
    }

    GameObject getShip()
    {
        // Assumes that the ShipScript is _only_ attached to the ship.
        foreach (GameObject gObj in FindObjectsOfType<GameObject>())
            if (gObj.GetComponent<ShipScript>() != null)
                return gObj;

        throw new UnityException("Couldn't find the ship.");
    }

    AudioManagerScript getAudioManager()
    {
        foreach (GameObject gObj in FindObjectsOfType<GameObject>())
        {
            var script = gObj.GetComponent<AudioManagerScript>();
            if (script != null)
                return script;
        }

        throw new UnityException("Couldn't find the music.");
    }

    Vector3 createRandomLocationNearPlayer()
    {
        Vector3 randomVec = new Vector3(
            Random.Range(-asteroidSpawnRange, asteroidSpawnRange),
            Random.Range(-asteroidSpawnRange, asteroidSpawnRange),
            Random.Range(-asteroidSpawnRange, asteroidSpawnRange));

        Vector3 shipLoc = ship_.transform.position;

        return shipLoc + randomVec;
    }
}
