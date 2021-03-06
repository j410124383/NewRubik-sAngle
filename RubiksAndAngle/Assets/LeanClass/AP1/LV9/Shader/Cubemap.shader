﻿Shader "Unlit/Cubemap"
{
    Properties
    {
        _Cubemap ("Cubemap ：环境球", Cube) = "_Skybox" {}
        _CubemapMip ("CubemapMip ：环境球Mip", Range(0, 7)) = 0
        _NormalMap ("NormalMap ：法线贴图", 2D) = "bump" {}
        _FresenelPow ("FresenelPow ：菲尼尔次幂", Range(0, 5)) = 1
        _EnvSpecInt ("EnvSpecInt ：环境镜面反射强度", Range(0, 5)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0


            uniform sampler2D _NormalMap;
            uniform samplerCUBE _Cubemap; 
            uniform float _CubemapMip;
            uniform float _FresenelPow;
            uniform float _EnvSpecInt;

            //输入结构
            struct VertexInput {      
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv0 : TEXCOORD0;
            };
            //输出结构
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 nDirWS : TEXCOORD1;
                float3 tDirWS : TEXCOORD2;
                float3 bDirWS : TEXCOORD3;
                float3 posWS : TEXCOORD4;
                
            };

           VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                    o.pos = UnityObjectToClipPos( v.vertex );
                    o.posWS = mul(unity_ObjectToWorld,v.vertex);
                    o.uv0 = v.uv0;
                    o.nDirWS = UnityObjectToWorldNormal(v.normal);
                    o.tDirWS = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                    o.bDirWS = normalize(cross(o.nDirWS, o.tDirWS) * v.tangent.w);
                return o;
            }

            float4 frag(VertexOutput i) : COLOR {

                //float3 var_NormalMap = UnpackNormal(tex2D(_NormalMap,i.uv0)).rgb;
                //float3x3 TBN = float3x3(i.tDirWS,i.bDirWS,i.nDirWS);   
                //float3 nDir = normalize(mul(var_NormalMap,TBN));
                //贴图采样
                float3 nDirTs = UnpackNormal(tex2D(_NormalMap,i.uv0)).rgb;
                float3x3 TBN = float3x3(i.tDirWS,i.bDirWS,i.nDirWS);   
                
                //向量准备
                float3 nDirWS = normalize(mul(nDirTs,TBN));
                //float3 nDirVS = mul(UNITY_MATRIX_V,fixed4(nDirWS,0.0));    
                float3 vDirWS = normalize(_WorldSpaceCameraPos.xyz - i.posWS.xyz);
                float3 vrDirWS = reflect(-vDirWS,nDirWS);

                //中间量准备
                //float2 matcapUV = nDirVS.rg * 0.5 + 0.5;
                float ndotv = dot(nDirWS,vDirWS);

                //光照模型准备
                //float3 matcap = tex2D(_Matcap,matcapUV);
                //float fresnel = pow(1.0 - ndotv,_FresnelPower);
                //float3 envSpecLighting = matcap * fresnel * _EnvSpaculer;
                
                float3 cubemap = texCUBElod(_Cubemap,float4(vrDirWS,_CubemapMip));
                float fresnel = pow(1.0 - ndotv,_FresenelPow);
                float3 envSpecLighting = cubemap * fresnel * _EnvSpecInt;

                //返回值
                fixed4 finalRGBA = fixed4(envSpecLighting,1.0);
                return finalRGBA;
            }
            ENDCG
        }
    }
}
