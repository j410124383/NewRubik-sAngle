using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetection : PostEffectsBase
{
    public Shader edgeDetectShader;
    private Material edgeDetectMaterial = null;

    public Material material
    {
        get
        {
            edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, edgeDetectMaterial);
            return edgeDetectMaterial;
        }
    }

    [Range(0.0f, 1.0f)] public float edgesOnly = 0.0f;//边缘线强度,当值为0边缘将会叠加在原渲染图像上；当值为1时，则会只显示边缘，不显示原渲染图像。
    [Range(0.1f,3.0f)] public float outlineWidth = 1.0f;
    [ColorUsage(false)]public Color outlineColor = Color.red;//描边颜色


    [HideInInspector] public Color backgroundColor = Color.white;//背景颜色


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
             material.SetFloat("_EdgeOnly", edgesOnly);
             material.SetColor("_EdgeColor", outlineColor);
             material.SetColor("_BackgroundColor", backgroundColor);
             material.SetFloat("_OutlineWidth", outlineWidth);
            // material.SetFloat("_Width", width);
            // material.SetFloat("_Iterations", iterations);
            // material.SetColor("_Color", edgeColor);

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}