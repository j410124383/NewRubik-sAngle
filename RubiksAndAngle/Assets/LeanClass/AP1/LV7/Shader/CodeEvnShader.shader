Shader "Unlit/CodeEvnShader"
{
    Properties {
        _AoTex("AoTex : AO贴图",2d) = "white"{}
        _EvnCol_Up("EvnCol_Up : 颜色",color) = (1.0,1.0,1.0,1.0)
        _EvnCol_Slider("EvnCol_Slider : 颜色",color) = (0.5,0.5,0.5,1.0)
        _EvnCol_Down("EvnCol_Down : 颜色",color) = (0.0,0.0,0.0,1.0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadowsS
            #pragma target 3.0
            //输入参数
            //修饰字
                //uniform 共享于 vert , frag
                //arttibute 仅用vert
                //varying 用于vert，frag传数据

            uniform float3 _EvnCol_Up; //RGB
            uniform float3 _EvnCol_Slider; //RGB
            uniform float3 _EvnCol_Down; //RGB
            uniform sampler2D _AoTex; //标量

            //输入结构
            struct VertexInput {
                float4 vertex : POSITION; //顶点信息
                float4 noemal : NORMAL;   //法线信息
                float2 uv0 : TEXCOORD0;   //将模型uv信息输入进来 0 通道  共4个通道
            };

            //输出结构
            struct VertexOutput {
                float4 pos : SV_POSITION; // 剪裁空间
                float3 nDirWS : TEXCOORD0;  // 世界空间法线方向
                float2 uv : TEXCOORD1;   // 世界空间顶点位置
            };

            //输入结构>>>顶点Shader>>>输出结构
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                     o.pos = UnityObjectToClipPos( v.vertex );            
                     o.nDirWS = UnityObjectToWorldNormal(v.noemal);
                     o.uv = v.uv0;
                return o;
            }

            //输出结构>>>像素
            float4 frag(VertexOutput i) : COLOR {
                //准备向量
                float3 nDir = i.nDirWS;
                //计算各部位遮罩
                float upMask = max(0.0,nDir.g);
                float downMask = max(0.0,-nDir.g);
                float sideMask = 1-upMask-downMask;
                //混合环境颜色
                float3 evnCol = _EvnCol_Up * upMask + _EvnCol_Down * downMask + _EvnCol_Slider * sideMask;
                //采样Occlusion贴图
                float occlusion = tex2D(_AoTex , i.uv);
                //计算环境光照
                float3 evnLighting = evnCol * occlusion;
                //返回结果

                return float4(evnLighting,1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
