using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicSizeCubeScript : MonoBehaviour
{

    [SerializeField] private int myBandLo;
    [SerializeField] private int numBands;

    private const int MINIMUM_QUALITY = 64;
    private readonly float[] spectrumSamples = new float[MINIMUM_QUALITY];

    void Update()
    {
        AudioListener // Read from global master mix.
            .GetSpectrumData(
                samples: spectrumSamples,
                channel: 0, // left + right channels.
                window: FFTWindow.Hamming); // medium quality.

        // You can't set `transform.localScale` directly, but this works.
        Vector3 localScale = transform.localScale;
        localScale.y = 
            spectrumSamples.ToList()
                .Skip(myBandLo)
                .Take(numBands)
                .Sum() * 10;
        transform.localScale = localScale;
    }
}
