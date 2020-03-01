using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsCanvasScript : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown("return")) {
            var canvas = GetComponent<Canvas>();
            canvas.enabled = !canvas.enabled;
        }
    }
}
