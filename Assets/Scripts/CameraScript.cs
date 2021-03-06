﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float distanceBehindShip;
    [SerializeField] float distanceAboveShip;
    [SerializeField] float angleToFaceDownwards;
    [SerializeField] float colorChangeRate;
    [SerializeField] float darkness;

    void Update()
    {
        setBackgroundColorBasedOnGlobalShipPosition();
        updateZoom();
        updatePositionToFollowShip();
        updateRotationToLookAtShip();
    }

    void setBackgroundColorBasedOnGlobalShipPosition()
    {
        Vector3 shipLoc = GameManager.instance.ship_.transform.position;
        GetComponent<Camera>().backgroundColor =
            new Color(
                r: positionToColorValue(shipLoc.x),
                g: positionToColorValue(shipLoc.y),
                b: positionToColorValue(shipLoc.z),
                a: .8f);
    }

    float positionToColorValue(float p)
    {
        float scaled = p / colorChangeRate;
        int integerComponent = Mathf.FloorToInt(scaled);
        float fractionalComponent = scaled - integerComponent;
        float cycling =
            integerComponent % 2 == 0
                ? fractionalComponent
                : 1 - fractionalComponent;
        float dimmed = cycling / darkness;
        return dimmed;
    }

    void updatePositionToFollowShip()
    {
        GameObject ship = GameManager.instance.ship_;
        Vector3 shipLoc = ship.transform.position;
        Vector3 backness = -ship.transform.forward * distanceBehindShip;
        Vector3 upness = ship.transform.up * distanceAboveShip;
        Vector3 distance = (backness + upness) * zoomLevel.Multiplier();
        Vector3 newPosition = shipLoc + distance;

        // This means that we always move a little bit closer to being pointed
        // in the direction that we want to be (with some kind of taylor series
        // [~exponential] backoff, or something like that).
        float followSpeed = Time.deltaTime * 2;

        transform.position =
            Vector3.Lerp(
                a: transform.position,
                b: newPosition,
                t: followSpeed);
    }

    void updateRotationToLookAtShip()
    {
        GameObject ship = GameManager.instance.ship_;
        transform.LookAt(ship.transform, ship.transform.up);
        transform.Rotate(angleToFaceDownwards, 0, 0);
    }


    /** How far the camera is from the user's spaceship. */
    ZoomLevel zoomLevel = new ZoomLevel();

    void updateZoom()
    {
        if (Input.GetKeyDown("z"))
        {
            zoomLevel.CycleNext();
            Debug.Log("zoomMultiplier: " + zoomLevel.Multiplier());
        }
    }

    class ZoomLevel
    {
        static float[] zoomLevels = { 1f, 3f, 5f };

        int currIdx;

        public float Multiplier()
        {
            return zoomLevels[currIdx % zoomLevels.Length];
        }

        public void CycleNext()
        {
            ++currIdx;
        }
    }
}
