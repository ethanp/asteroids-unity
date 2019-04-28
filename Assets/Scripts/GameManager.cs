using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private GameObject[] rockPrefabs = new GameObject[11];

    public static readonly Bounds gameBounds = 
        new Bounds(Vector3.zero, Vector3.one* 10);

    public bool OverlapsExistingObject(GameObject newObject)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject existingObject in allObjects)
        {
            Debug.Log("Checking collision with " + existingObject);
            if (newObject != existingObject)
                if (existingObject.activeInHierarchy)
                    if (existingObject.GetComponent<Collider>() != null)
                    {
                        Debug.Log("Checking bounds.");
                        if (bounds(newObject).Intersects(bounds(existingObject)))
                        {
                            Debug.Log("Matched.");
                            return true;
                        }
                        Debug.Log("Didn't match.");
                    }
        }
        return false;
    }

    public GameObject GetRandomRockPrefab()
    {
        return rockPrefabs[Random.Range(0, 10)];
    }

    private static Bounds bounds(GameObject gameObject)
    {
        return gameObject.GetComponent<Collider>().bounds;
    }

    void Start()
    {
        Debug.Log("Loading rock prefabs.");
        // Load rock prefabs.
        for (int i = 1; i <= 11; i++)
            rockPrefabs[i - 1] =
                Resources.Load(
                    "Prefabs/Rock" + i)
                    as GameObject;
   }

    // This is recommended here:
    //
    // https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    //
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Persist between scenes.
        DontDestroyOnLoad(gameObject);
    }
}
