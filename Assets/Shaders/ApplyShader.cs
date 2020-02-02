using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShader : MonoBehaviour
{
    public Material mat;
    public float delay = 0;

    private void OnEnable()
    {
        delay = 1;
    }

    // Update is called once per frame
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        //delay += Time.deltaTime * 15;
        mat.SetFloat("_Delay", delay);
        Graphics.Blit(src, dst, mat);
    }
}
