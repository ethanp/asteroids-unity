using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private readonly GameObject[] rockPrefabs = new GameObject[11];

    private int score = 0;
    private int livesRemaining = 3;

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI livesRemainingText;
    [SerializeField] private TMPro.TextMeshProUGUI youLostText;

    // TODO: Remove this.
    [SerializeField] private float playBoxSize = 20;


    // NB: `readonly` makes it so it can't be set in the Unity editor.
    public static readonly Bounds gameBounds =
        new Bounds(
            center: Vector3.zero,
            size: Vector3.one * 3000);

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
            Destroy(ship);
        }
        else
        {
            FindEmptyLocationNearPlayer(ship);
        }
    }

    public void ScoreAsteroidHit()
    {
        scoreText.text = "Score: " + ++score;
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
    private bool OverlapsExistingObject(GameObject newObject)
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

    public GameObject GetShip()
    {
        foreach (GameObject gObj in FindObjectsOfType<GameObject>())
            if (gObj.GetComponent<ShipScript>() != null)
                return gObj;
        throw new UnityException("Couldn't find the ship.");
    }

    private Vector3 createRandomLocationNearPlayer()
    {
        Vector3 randomVec = new Vector3(
            Random.Range(-playBoxSize, playBoxSize),
            Random.Range(-playBoxSize, playBoxSize),
            Random.Range(-playBoxSize, playBoxSize));

        Vector3 shipLoc = GetShip().transform.position;

        return shipLoc + randomVec;
    }
}
