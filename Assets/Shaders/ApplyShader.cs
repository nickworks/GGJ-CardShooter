using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShader : MonoBehaviour
{
    public Material mat;
    public float delay = 0;

    public float xStrength = 1;
    public float zStrength = 1;

    private void OnEnable()
    {
        delay = 1;
    }

    // Update is called once per frame
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        mat.SetFloat("_offsetX", transform.position.x * xStrength);
        mat.SetFloat("_offsetY", transform.position.z * zStrength);
        //delay += Time.deltaTime * 15;
        mat.SetFloat("_Delay", delay);
        Graphics.Blit(src, dst, mat);
    }
}
