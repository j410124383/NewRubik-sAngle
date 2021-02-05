
Shader "Shader Forge/ScreenMark" {
    Properties {
        _MianCol ("MianCol", Color) = (0.5,0.5,0.5,1)
        _Circle ("X: CenterX, Y: CnterY, Z: Raidus",vector) = (0.5,0.5,0.5,0)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0

            uniform half4 _MianCol;
            uniform half4 _Circle;

            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            half4 frag(VertexOutput i) : COLOR {

                half2 sceneUVs = (i.projPos.xy / i.projPos.w);
                half screenRate = (_ScreenParams.r/_ScreenParams.g);
                
                half screenX_Rate = 1 - abs(max(0,min(_Circle.r,1)) * 0.5 );
                half screenY_Rate = 1 - abs(max(0,min(_Circle.g,1)) * 0.5 );

                half alpha = min(_MianCol.a, step( max(0,min(_Circle.b,1)) * 2 * max(screenX_Rate, screenY_Rate), distance(half2( (max(0,min(_Circle.r + 0.03,1)) * screenRate), max(0,min(_Circle.g,1))), half2((screenRate * sceneUVs.r),sceneUVs.g))) );
                
                half4 finalRGBA = half4(_MianCol.rgb,alpha);
                
                return finalRGBA;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
