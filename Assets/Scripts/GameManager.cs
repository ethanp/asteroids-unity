using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool OverlapsExistingObject(GameObject newObject)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject existingObject in allObjects)
            if (existingObject.activeInHierarchy)
                if (bounds(newObject).Intersects(bounds(existingObject)))
                    return true;
        return false;
    }

    private Bounds bounds(GameObject gameObject)
    {
        return gameObject.GetComponent<Collider>().bounds;
    }

    // SINGLETON.
    private static GameManager instance = null;
    public static GameManager Instance()
    {
        if (instance == null)
            instance = new GameManager();
        return instance;
    }
}
