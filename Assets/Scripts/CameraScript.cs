using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject ship_;

    void Start()
    {
        ship_ = GameManager.instance.GetShip();
    }

    void Update()
    {
        updateBackgroundColor();
        followShip();
        lookAtShip();
    }

    private void updateBackgroundColor()
    {
        GetComponent<Camera>().backgroundColor =
            new Color(
                r: Mathf.Abs(Mathf.Sin(Time.time / 20)),
                g: Mathf.Abs(Mathf.Cos(Time.time / 20)),
                b: .2f,
                a: .8f);
    }

    private void followShip()
    {
        // TODO Should be a lerp.
        transform.position =
            ship_.transform.position
                - ship_.transform.forward * 30
                + ship_.transform.up * 20;
    }

    private void lookAtShip()
    {
        // TODO Should be a slerp.
        transform.LookAt(ship_.transform, ship_.transform.up);
        transform.Rotate(5, 0, 0);
    }
}
