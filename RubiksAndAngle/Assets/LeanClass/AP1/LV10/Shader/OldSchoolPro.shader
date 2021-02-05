
Shader "MyShader/OldSchoolPro" {
    Properties {
        [Header(Texture)]
            _MainTex    ("RGB : 基础颜色 A:环境遮罩", 2D)    = "white"{}
            _NormalTex  ("RGB : 法线贴图 ", 2D)				 = "bump"{}
            _SpecTex    ("RGB : 高光颜色 A:高光次幂", 2D)    = "gary"{}
            _EmitTex    ("RGB : 自发光贴图 ", 2D)				 = "black"{}
            _Cubemap    ("RGB : 环境贴图 ", cube)				  = "_Skybox"{}
        [Header(Diffuse)]
            _MainCol    ("基本色", Color)								 =(0.5,0.5,0.5,1.0)
            _EnvDiffInt ("环境漫反射强度", Range(0,1))		 = 0.2
            _EnvUpCol   ("环境天顶颜色", Color)					 =(1.0,1.0,1.0,1.0)
            _EnvSideCol ("环境水平颜色", Color)				     =(0.5,0.5,0.5,1.0)
            _EnvDownCol ("环境地表颜色", Color)                =(0.0,0.0,0.0,0.0)
        [Header(Specular)]
            _SpecPow    ("高光次幂", Range(1,90))				 = 30
            _EnvSpecInt ("环境镜面反射强度", Range(0,5))   = 0.2
            _FresnelPow ("菲尼尔次幂", Range(0,5))			 = 1
            _CubemapMip ("环境球Mip", Range(1,7))          = 1
        [Header(Emission)]    
            _EmitInt    ("自发光", Range(1,10))					  = 1    
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
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0


            // Texture
            uniform sampler2D _MainTex ;  uniform float4 _MainTex_ST ;
            uniform sampler2D _NormalTex ;
            uniform sampler2D _SpecTex ;
            uniform sampler2D _EmitTex ;
            uniform samplerCUBE _Cubemap ;

            // Diffuse
            uniform float3 _MainCol;
            uniform float _EnvDiffInt;
            uniform float3 _EnvUpCol;
            uniform float3 _EnvSideCol;
            uniform float3 _EnvDownCol;

            // Specular
            uniform float _SpecPow ; 
            uniform float _FresnelPow ;
            uniform float _EnvSpecInt ;
            uniform float _CubemapMip ;

            // Emission
            uniform float _EmitInt; 

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
                    o.nDirWS = UnityObjectToWorldNormal(v.normal); //变换法线方向 OS》WS
                    o.tDirWS = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                    o.bDirWS = normalize(cross(o.nDirWS, o.tDirWS) * v.tangent.w);
                    TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }

			// 三颜色(顶部，侧面，地面)差值环境光方法
			float3 TriColAmbient ( float3 n , float3 uCol , float3 sCol , float3 dCol)
			{
				
				     //环境漫反射
                    float uMask = max(0.0,n.g);
                    float dMask = max(0.0,-n.g);
                    float sMask = 1.0 - uMask - dMask;
                    float3 envCol = uCol * uMask 
										  + dCol * dMask 
										  + sCol * sMask;

				return envCol;
			} 


            float4 frag(VertexOutput i) : COLOR {
                
                //贴图采样
                
                //向量准备
                    //法线向量
                    float3 nDirTs = UnpackNormal(tex2D(_NormalTex,i.uv0)).rgb;
                    float3x3 TBN = float3x3(i.tDirWS,i.bDirWS,i.nDirWS);   
                    float3 nDirWS = normalize(mul(nDirTs,TBN));
                    float3 nDirVS = mul(UNITY_MATRIX_V,fixed4(nDirWS,0.0)); 
                    
                    //视线向量
                    float3 vDirWS = normalize(_WorldSpaceCameraPos.xyz - i.posWS.xyz);
                    float3 vrDirWS = reflect(-vDirWS,nDirWS);

                    //光线向量
                    float3 lDirWS =_WorldSpaceLightPos0.xyz;
                    float3 lrDirWS = reflect(-lDirWS,nDirWS);

                //中间量准备
                float ndotl = dot(nDirWS,lDirWS);
                float vdotr = dot(vDirWS,lrDirWS);
                float vdotn = dot(vDirWS,nDirWS);
                //纹理采样
                float4 var_MainTex = tex2D(_MainTex,i.uv0);
                float4 var_SpecTex = tex2D(_MainTex,i.uv0);
                float3 var_EmitTex = tex2D(_MainTex,i.uv0).rgb;
                float3 var_Cubemap = texCUBElod(_Cubemap,  float4(vrDirWS, lerp(_CubemapMip , 0.0, var_SpecTex.a ))).rgb;

                //光照模型准备
                    //光源漫反射
                    float3 baseCol = var_MainTex.rgb * _MainCol;
                    float lambert = max(0.0,ndotl);

                    //光源镜面反射
                    float3 specCol = var_SpecTex.rgb;
                    float specPow = lerp(1.0,_SpecPow,var_SpecTex.a);
                    float phong = pow(max(0.0,vdotr),specPow);

                    //光源反射混合
                    float shadow = LIGHT_ATTENUATION(i);
                    float3 dirLighting = (baseCol * lambert + specCol*phong)*_LightColor0 * shadow;

                    //环境漫反射
                    //float upMask = max(0.0,nDirWS.g);
                    //float downMask = max(0.0,-nDirWS.g);
                    //float sideMask = 1.0 - upMask - downMask;
                    //float3 envCol = _EnvUpCol * upMask + _EnvDownCol * downMask + _EnvSideCol * sideMask;

					float3 envCol = TriColAmbient(nDirWS,_EnvUpCol,_EnvSideCol,_EnvDownCol); 

                    float3 envDiff = baseCol * envCol * _EnvSpecInt;
                    //环境镜面反射
                    float fresnel = pow (max ( 0.0 , 1.0 - vdotn) , _FresnelPow);
                 
					float3 envSpec = var_Cubemap.rgb * fresnel * _EnvSpecInt;

                    //环境反射混合
                    float occlusion = var_MainTex.a;
                    float3 envLighting = (envDiff + envSpec) * occlusion;

                    //自发光
                    float3 emission = var_EmitTex * _EmitInt;
                //综合混合
                float3 finalRGB = dirLighting * envLighting * emission; 
                //返回值
                fixed4 finalRGBA = fixed4(finalRGB,1.0);
                return finalRGBA;
            }
            ENDCG
        }
    }
    
	FallBack "Diffuse"
    
}
