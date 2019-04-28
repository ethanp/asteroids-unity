using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private float timeGunLastFired = 0f;
    [SerializeField] private float backoffBetweenFires;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFire())
        {
            fire();
        }
    }

    private void FixedUpdate()
    {
        handleArrowKeys();
    }

    private bool shouldFire()
    {

        if (Input.GetKeyDown("space")
            && Time.time - timeGunLastFired > backoffBetweenFires)
        {
            timeGunLastFired = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void fire()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = transform.position;
    }

    private void handleArrowKeys()
    {
        if (Input.GetKeyDown("up"))
        {
            // TODO add `forward` force.
        }
        if (Input.GetKeyDown("down"))
        {

        }
        if (Input.GetKeyDown("left"))
        {

        }
        if (Input.GetKeyDown("right"))
        {

        }

    }
}
