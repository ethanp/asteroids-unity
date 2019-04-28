using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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

    private static Bounds bounds(GameObject gameObject)
    {
        return gameObject.GetComponent<Collider>().bounds;
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
