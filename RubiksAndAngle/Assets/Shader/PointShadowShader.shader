Shader "MyShader/PointShadowShader"
{
    Properties
    {
        [Header(Texture)]
        _MainTex ("Texture", 2D)                        = "white" {}
        _NormalTex  ("RGB : 法线贴图 ", 2D)				 = "bump"{}
        _EnvTex    ("RGB : 环境颜色贴图 ", 2D)			 = "gary"{}
   
        [Header(Diffuse)]
        _MainCol    ("基本色", Color)					 =(0.5,0.5,0.5,1.0)
        
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
        }

        LOD 200
        //Blend  Off

        Pass {
            Name "FORWARD"

            Tags {  "Quere" = "Transparent"           //调整渲染队列
            "RenderType"="TransparentCutout"  //对应改为Cutout
            "ForceNoShadowCasting" = "True"   //关闭阴影投射
            "IgnoreProjector" = "True"        //不响应投射器
            }

            Blend One One //One/SrcAlpha One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0

            // Texture
            uniform sampler2D _MainTex ;  uniform float4 _MainTex_ST ;
            uniform sampler2D _NormalTex ;
            uniform sampler2D _EnvTex ;

            // Diffuse
            uniform float3 _MainCol;
            
            //输入结构
            struct VertexInput {
                float4 vertex   : POSITION;
                float2 uv0      : TEXCOORD0;
                float4 normal   : NORMAL;
                float4 tangent  : TANGENT;

            };
            //输出结构
            struct VertexOutput {
                float4 pos      : SV_POSITION;
                float2 uv0      : TEXCOORD0;
                float4 posWS    : TEXCOORD1;
                float3 nDirWS   : TEXCOORD2;
                float3 tDirWS   : TEXCOORD3;
                float3 bDirWS   : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                    o.pos = UnityObjectToClipPos( v.vertex );
                    o.uv0 = v.uv0 * _MainTex_ST.xy + _MainTex_ST.zw;
                    o.posWS = mul(unity_ObjectToWorld ,v.vertex);     //变换顶点位置 OS》WS  
                    o.posWS.w = o.pos.z / o.pos.w;
                    o.nDirWS = UnityObjectToWorldNormal(v.normal);    //变换法线方向 OS》WS
                    o.tDirWS = normalize(mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                    o.bDirWS = normalize(cross(o.nDirWS, o.tDirWS) * v.tangent.w);
                    TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }

            fixed4 frag (VertexOutput i) : COLOR 
            {
                
                //向量准备                
                    //法线向量
                    //fixed3 nDirTs = UnpackNormal(tex2D(_NormalTex,i.uv0)).rgb;
                    //fixed3x3 TBN = fixed3x3(i.tDirWS,i.bDirWS,i.nDirWS);   
                    fixed3 nDirWS =  i.nDirWS;  //normalize(mul(nDirTs,TBN));
                    //fixed3 nDirVS = mul(UNITY_MATRIX_V,fixed4(nDirWS,0.0)); 

                    //光线向量
                    fixed3 lDirWS =_WorldSpaceLightPos0.xyz;
                    fixed3 lrDirWS = reflect(-lDirWS,nDirWS);
                    
                
                //中间量准备
                fixed ndotl = dot(nDirWS,lDirWS);
                //float vdotr = dot(vDirWS,lrDirWS);
                //float vdotn = dot(vDirWS,nDirWS);

                // sample the texture
                fixed4 var_MainTex = tex2D(_MainTex,i.uv0);
                fixed4 var_EnvTex = tex2D(_EnvTex,i.uv0);

                //光照模型准备
                    //光源漫反射
                    fixed3 baseCol = var_MainTex.rgb * _MainCol;
                    fixed lambert = max(0.0,ndotl);

                    fixed fMask = step(0.0 , max(0.0, lambert) - 0.7);
                    fixed bMask = step(0.0 , max(0.0, 1 - fMask) - 0.5);
                    fixed dMask = step(0.0 , max(0.0, - nDirWS.g) - 0.5);

                fixed sp_EnvCol = pow(var_EnvTex.r , 2);

                //光源反射混合
                fixed3 dirLighting = (bMask - dMask) * baseCol.r + fMask;

                fixed3 finalRGB = pow(dirLighting , 1);
                fixed4 finalRGBA = fixed4(finalRGB,1.0);
                
                //clip(float4(finalRGBA.a,finalRGBA.g,finalRGBA.b,1.0));

                return finalRGBA;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"

}
