using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class MusicSizeCubeScript : MonoBehaviour
{

    [SerializeField] int myBandLo;
    [SerializeField] int numBands;
    [SerializeField] int scale;

    const int MINIMUM_QUALITY = 64;
    readonly float[] spectrumSamples = new float[MINIMUM_QUALITY];

    void Update()
    {
        readFromGlobalMasterMix();
        // resizeToMusic();
        deform();
    }

    void readFromGlobalMasterMix()
    {
        AudioListener
            .GetSpectrumData(
                samples: spectrumSamples,
                channel: 0, // left + right channels.
                window: FFTWindow.Hamming); // medium quality.
    }

    void deform()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] originalVertices = mesh.vertices;
        Vector3[] displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            float amt = .5f;
            displacedVertices[i] =
                new Vector3(
                    x: originalVertices[i].x + Random.Range(-amt, amt),
                    y: originalVertices[i].y + Random.Range(-amt, amt),
                    z: originalVertices[i].z + Random.Range(-amt, amt));
        }
        mesh.vertices = displacedVertices;
        // TODO: What does this do?
        mesh.RecalculateNormals();
    }

    void resizeToMusic()
    {
        float amplitude =
            spectrumSamples
                .ToList()
                .Skip(myBandLo)
                .Take(numBands)
                .Sum();

        float targetSize = Mathf.Log(amplitude * scale + 1, 2);

        // Hack: You can't modify `transform.localScale` in-place, but this works.
        Vector3 localScale = transform.localScale;
        localScale.y =
            Mathf.Lerp(
                a: localScale.y,
                b: targetSize,
                // TODO: This should incorporate `Time.deltaTime`.
                t: .4f);
        transform.localScale = localScale;
    }
}
