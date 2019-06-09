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

    [SerializeField] private float playBoxSize = 20;


    // NB: `readonly` makes it so it can't be set in the Unity editor.
    public static readonly Bounds gameBounds =
        new Bounds(
            center: Vector3.zero,
            size: Vector3.one * 30);

    public void FindEmptyLocation(GameObject gameObject)
    {
        gameObject.transform.position = createRandomLocation();

        // TODO This should be a `while`-loop, but it was infinite-looping.
        if (GameManager.instance.OverlapsExistingObject(gameObject))
        {
            Debug.Log("Going to another spot.");
            gameObject.transform.position = createRandomLocation();
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
            FindEmptyLocation(ship);
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

    private bool OverlapsExistingObject(GameObject newObject)
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
                            Debug.Log("It does intersect.");
                            return true;
                        }
                        Debug.Log("It does NOT intersect.");
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

    private Vector3 createRandomLocation()
    {
        return new Vector3(
            Random.Range(-playBoxSize, playBoxSize),
            Random.Range(-playBoxSize, playBoxSize),
            Random.Range(-playBoxSize, playBoxSize));
    }
}
