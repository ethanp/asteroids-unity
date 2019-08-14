﻿using System.Collections;
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

    /** Background color slowly varies over time. */
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
        Vector3 ship = ship_.transform.position;
        Vector3 back = -ship_.transform.forward * backwardsness;
        Vector3 up = ship_.transform.up * upwardsness;
        Vector3 newPosition = ship + back + up;

        float followSpeed = Time.deltaTime * 2;

        transform.position =
            Vector3.Lerp(
                a: transform.position,
                b: newPosition,
                t: followSpeed);
    }

    private void lookAtShip()
    {
        transform.LookAt(ship_.transform, ship_.transform.up);
        transform.Rotate(downAngle, 0, 0);
    }
}
