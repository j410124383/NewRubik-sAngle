using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetectNormalsAndDepth : PostEffectsBase
{
    public Shader edgeDetectShader ;
    private Material edgeDetectMaterial = null;
    public Material material
    {
        get
        {
            edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, edgeDetectMaterial);
            return edgeDetectMaterial;
        }
    }
    Camera depthCamera;
    [Range(0.0f, 1.0f)]
    public float edgesOnly = 0.0f;

    public Color edgeColor = Color.black;

    public Color backgroundColor = Color.white;

    [Range(0.0f, 3.0f)] public float sampleDistance = 1.0f;
    //控制对深度+法线纹理采样时， 使用的采样距离
    [Range(0.0f, 2.0f)] public float sensitivityDepth = 1.0f;

    [Range(0.0f, 1.0f)] public float sensitivityNormals = 0.5f;

    private void OnValidate()
    {
        sampleDistance = Mathf.Floor(sampleDistance);
        sensitivityNormals = Mathf.Floor(sensitivityNormals * 10f) / 10f;
    }

    void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
        depthCamera = GetComponent<Camera>();
    }

    //[ImageEffectOpaque] 我们只希望对不透明物体进行描边
    //而不希望透明物体也被描边， 因此需要添加该属性
    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            if(backgroundColor != depthCamera.backgroundColor)
            {
                backgroundColor = new Color(depthCamera.backgroundColor.r, depthCamera.backgroundColor.g, depthCamera.backgroundColor.b, 0);
            }

            material.SetFloat("_EdgeOnly", edgesOnly);
            material.SetColor("_EdgeColor", edgeColor);
            material.SetColor("_BackgroundColor", backgroundColor);
            material.SetFloat("_SampleDistance", sampleDistance);
            material.SetVector("_Sensitivity", new Vector4(sensitivityNormals, sensitivityDepth, 0.0f, 0.0f));

            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
