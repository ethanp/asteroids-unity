using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This was recommended for singletons in
//
// https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
//
public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
