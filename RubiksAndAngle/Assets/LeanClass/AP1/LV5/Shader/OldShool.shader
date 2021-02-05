Shader "MyShader/OldShool" {
    Properties {
        _MainCol("Color : 颜色",color) = (1.0,1.0,1.0,1.0)
        _SpecularPow("SpecularPower : 高光次幂",range(1,90)) = 30
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
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

            uniform float3 _MainCol; //RGB
            uniform float _SpecularPow; //标量

            //输入结构
            struct VertexInput {
                float4 vertex : POSITION; //顶点信息
                float4 noemal : NORMAL;   //法线信息
            };

            //输出结构
            struct VertexOutput {
                float4 posCS : SV_POSITION; // 剪裁空间
                float4 posWS : TEXCOORD0;   // 世界空间顶点位置
                float3 nDirWS : TEXCOORD1;  // 世界空间法线方向
            };

            //输入结构>>>顶点Shader>>>输出结构
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                     o.posCS = UnityObjectToClipPos( v.vertex );
                     o.posWS = mul(unity_ObjectToWorld,v.vertex);
                     o.nDirWS = UnityObjectToWorldNormal(v.noemal);
                return o;
            }

            //输出结构>>>像素
            float4 frag(VertexOutput i) : COLOR {
                //准备向量
                float3 nDir = i.nDirWS;
                float3 lDir = _WorldSpaceLightPos0.xyz;
                float3 vDir = normalize(_WorldSpaceCameraPos.xyz - i.posWS);
                float3 hDir = normalize( vDir + lDir );
                //准备中间数据
                float nDotl = dot(nDir,lDir);
                float nDoth = dot(nDir,hDir);
                //光照模型
                float lambert = max(0.0,nDotl);
                float blinnPhong = pow(max(0.0,nDoth),_SpecularPow);
                float3 finalRGB = _MainCol * lambert + blinnPhong;
                //返回结果

                return float4(finalRGB,1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}