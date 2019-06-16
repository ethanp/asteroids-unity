using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
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
        // resizeToMusic();
        deform();
    }

    private void readFromGlobalMasterMix()
    {
        AudioListener
            .GetSpectrumData(
                samples: spectrumSamples,
                channel: 0, // left + right channels.
                window: FFTWindow.Hamming); // medium quality.
    }

    private void deform()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] originalVertices = mesh.vertices;
        Vector3[] displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            float amt = .1f;
            displacedVertices[i] =
                new Vector3(
                    x: originalVertices[i].x + Random.Range(-amt, amt),
                    y: originalVertices[i].y + Random.Range(-amt, amt),
                    z: originalVertices[i].z + Random.Range(-amt, amt));
        }
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }

    private void resizeToMusic()
    {
        // You can't modify `transform.localScale` in-place, but this works.
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
                // TODO: This should incorporate `Time.deltaTime`.
                t: .4f);

        transform.localScale = localScale;
    }
}
