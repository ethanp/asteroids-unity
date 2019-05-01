using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSizeCubeScript : MonoBehaviour
{

    [SerializeField] private int myBandLo;
    [SerializeField] private int myBandHi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[64]; // fastest/minimum quality.

        AudioListener // Read from global master mix.
            .GetSpectrumData(
                samples: spectrum,
                channel: 0, // left + right channels.
                window: FFTWindow.Hamming); // medium quality.

        float sum = 0;
        for (int idx = myBandLo; idx <= myBandHi; idx++)
        {
            Debug.Log(spectrum[idx]);
            sum += spectrum[idx];
        }

        var localScale = transform.localScale;
        localScale.y = sum;
        transform.localScale = localScale;
    }
}
