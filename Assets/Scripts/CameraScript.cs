using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject ship_;

    [SerializeField] private float backwardsness;
    [SerializeField] private float upwardsness;
    [SerializeField] private float downAngle;

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
                r: Mathf.Abs(Mathf.Sin(Time.time / 10)) / 2,
                g: Mathf.Abs(Mathf.Cos(Time.time / 10)) / 2,
                b: .2f,
                a: .8f);
    }

    private void followShip()
    {
        // TODO Should be a lerp.
        transform.position =
            ship_.transform.position
                - ship_.transform.forward * backwardsness
                + ship_.transform.up * upwardsness;
    }

    private void lookAtShip()
    {
        // TODO Should be a slerp.
        transform.LookAt(ship_.transform, ship_.transform.up);
        transform.Rotate(downAngle, 0, 0);
    }
}
