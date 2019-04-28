using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{


    public ICollection<Bounds> GetExistingColliderBounds()
    {
        ICollection<Bounds> ret = new List<Bounds>();
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
            if (go.activeInHierarchy)
                ret.Add(go.GetComponent<Collider>().bounds);
        return ret;
    }

    // SINGLETON.
    private static GameManagerScript instance = null;
    public static GameManagerScript Instance()
    {
        if (instance == null)
            instance = new GameManagerScript();
        return instance;
    }
}
