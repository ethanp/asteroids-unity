using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicSizeCubeScript : MonoBehaviour
{

    [SerializeField] private int myBandLo;
    [SerializeField] private int numBands;
    [SerializeField] private int scale;

    private const int MINIMUM_QUALITY = 64;
    private readonly float[] spectrumSamples = new float[MINIMUM_QUALITY];

    void Update()
    {
        readFromGlobalMasterMix();
        resizeToMusic();
    }

    private void readFromGlobalMasterMix()
    {
        AudioListener
            .GetSpectrumData(
                samples: spectrumSamples,
                channel: 0, // left + right channels.
                window: FFTWindow.Hamming); // medium quality.
    }

    private void resizeToMusic()
    {
        // You can't set `transform.localScale` directly, but this works.
        Vector3 localScale = transform.localScale;

        float amplitude =
            spectrumSamples
                .ToList()
                .Skip(myBandLo)
                .Take(numBands)
                .Sum();

        float targetSize = Mathf.Log(amplitude * scale + 1, 2);

        localScale.y =
            Mathf.Lerp(
                a: localScale.y,
                b: targetSize,
                t: .4f);

        transform.localScale = localScale;
    }
}
