Shader "MyShader/Dota2_OgreShader"
{
    Properties {
       // _MainCol    ("Main Color : 主颜色", Color)                          =(1.0,1.0,1.0,1.0)
        [Header (Texture)][Space(5)]
        _MainTex       ("RGB : 基础颜色 A:环境遮罩", 2d)                          = "white"{}
        _MaskTex       ("R : 高光强度 G: 边缘光强度 B: 高光染色 A:高光次幂", 2d)    = "black"{}
        _NormalTex     ("RGB : 法线贴图 ", 2d)				                     = "bump"{}
        _MatelnessMask ("金属光遮罩", 2d)                                        = "black"{}
        _EmissionMask  ("自发光遮罩", 2d)                                        = "black"{}
        _DiddWarpMask  ("颜色Warp图", 2d)                                        = "gray"{}
        _FresWarpMask  ("菲涅尔Warp图", 2d)                                      = "gray"{}
        _Cubemap       ("环境球", cube)                                          = "_Skybox"{}
        [Space(20)]
       
        [Header (DirDiff)][Space(5)]
        _LightCol      ("光颜色",color)                                          =(1.0,1.0,1.0,1.0)                     
        _SpecPow       ("高光次幂",range(0.0,90.0))                              =40.0                   
        _SpecInt       ("高光强度",range(0.0,10.0))                              =5.0                       
        [Space(20)]
       
        [Header (EnvDiff)][Space(5)]
        _EnvUpCol      ("环境光颜色",color)                                      =(1.0,1.0,1.0,1.0)
        _EnvDiffInt    ("环境漫反射强度",range(0.0,5.0))                          =0.5
        _EnvSpecInt    ("环境镜面反射强度",range(0.0,30.0))                       =10.0 
        [Space(20)]
       
        [Header (RimLight)][Space(5)]
        _RimCol        ("轮廓光颜色",color)                                      =(1.0,1.0,1.0,1.0)
        _RimInt        ("轮廓光强度",range(0.0,3.0))                             =1.0                      
        [Space(20)]
       
        [Header (EmitLight)][Space(5)]
        _EmitInt       ("自发光强度",range(0.0,10.0))                            =1.0                      

        [HideInInspector]
        _Cutoff        ("Alpha cutoff",range(0.0,1.0))                          =0.5           
        
        [HideInInspector]
        _Color         ("Main Color",color)                                     =(1.0,1.0,1.0,1.0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
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
            #pragma target 3.0

            uniform sampler2D     _MainTex       ;
            uniform sampler2D     _MaskTex       ;
            uniform sampler2D     _NormalTex     ;
            uniform sampler2D     _MatelnessMask ;
            uniform sampler2D     _EmissionMask  ;
            uniform sampler2D     _DiddWarpMask  ;
            uniform sampler2D     _FresWarpMask  ;
            uniform samplerCUBE   _Cubemap       ;

            uniform half3         _LightCol      ;
            uniform half          _SpecPow       ;
            uniform half          _SpecInt       ;
            uniform half3         _EnvUpCol      ;   
            uniform half          _EnvDiffInt    ;
            uniform half          _EnvSpecInt    ;   
            uniform half3         _RimCol        ;
            uniform half          _RimInt        ;
            uniform half          _EmitInt       ;

            uniform half          _Cutoff       ;

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

            //输入结构>>>顶点shader>>>输出结构
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;                                                           // 新建输出结构
                    o.pos = UnityObjectToClipPos( v.vertex );                                               // 顶点位置 OS>CS
                    o.uv0 = v.uv0;                                                                          // 传递uv
                    o.posWS = mul(unity_ObjectToWorld ,v.vertex);                                           // 变换顶点位置 OS》WS  
                    o.nDirWS = UnityObjectToWorldNormal(v.normal);                                          // 变换法线方向 OS》WS
                    o.tDirWS = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );   // 变换切线方向 OS》WS
                    o.bDirWS = normalize(cross(o.nDirWS, o.tDirWS) * v.tangent.w);                          // 变换副切线方向 OS》WS          
                    TRANSFER_VERTEX_TO_FRAGMENT(o)                                                          // 投影相关
                return o;                                                                                   // 返回输出结构
            }
            //输出结构>>>像素
            float4 frag(VertexOutput i) : COLOR {
                
                 //贴图采样
                
                //向量准备
                    //法线向量
                    half3 nDirTs = UnpackNormal(tex2D(_NormalTex,i.uv0)).rgb;
                    half3x3 TBN = half3x3(i.tDirWS,i.bDirWS,i.nDirWS);   
                    half3 nDirWS = normalize(mul(nDirTs,TBN));
                    half3 nDirVS = mul(UNITY_MATRIX_V,fixed4(nDirWS,0.0)); 
                    
                    //视线向量
                    half3 vDirWS = normalize(_WorldSpaceCameraPos.xyz - i.posWS.xyz);
                    half3 vrDirWS = reflect(-vDirWS,nDirWS);

                    //光线向量
                    half3 lDirWS =_WorldSpaceLightPos0.xyz;
                    half3 lrDirWS = reflect(-lDirWS,nDirWS);

                //中间量准备
                half ndotl = dot(nDirWS,lDirWS);
                half ndotv = dot(nDirWS,vDirWS);
                half vdotr = dot(vDirWS,lrDirWS);
                //half vdotn = dot(vDirWS,nDirWS);
                
                //纹理采样
                half4 var_MainTex = tex2D(_MainTex,i.uv0);
                half4 var_MaskTex = tex2D(_MaskTex,i.uv0);
                half  var_MatelnessMask = tex2D(_MatelnessMask,i.uv0).r;
                half  var_EmissionMask = tex2D(_EmissionMask,i.uv0).r;
                //float3 var_EmitTex = tex2D(_MainTex,i.uv0).rgb;
                half3 var_FresWarpMask = tex2D(_FresWarpMask,ndotv).rgb;
                half3 var_Cubemap = texCUBElod(_Cubemap,  float4(vrDirWS, lerp(8.0 , 0.0, var_MaskTex.a ))).rgb;

                //提取信息
                half3 baseCol = var_MainTex.rgb;
                half opacity = var_MainTex.a;
                half specInt = var_MaskTex.r;
                half rimInt = var_MaskTex.g;
                half specTint = var_MaskTex.b;
                half specPow = var_MaskTex.a;
                half matellic = var_MatelnessMask;
                half emitInt = var_EmissionMask;
                half3 envCube = var_Cubemap;
                half shadow = LIGHT_ATTENUATION(i);

                half3 fresnel = lerp(var_FresWarpMask,0.0,matellic);
                half fresnelCol = fresnel.r;
                half fresnelRim = fresnel.g;
                half fresnelSpec = fresnel.b;

                //光照模型准备
                    //光源漫反射颜色
                    half3 diffCol = lerp(baseCol,half3(0.0,0.0,0.0),matellic);
                    //光源镜面反射颜色
                    half3 specCol = lerp(baseCol,half3(0.3,0.3,0.3),specTint) * specInt;
                    //光源漫反射
                    half halfLambert = ndotl * 0.5 + 0.5;
                    half3 var_DiffWarpTex = tex2D(_DiddWarpMask,half2(halfLambert,0.1));
                    half3 dirDiff = diffCol * var_DiffWarpTex * _LightCol;
                    //光源镜面反射
                    half phong = pow(max(0.0,vdotr),specPow * _SpecPow);
                    half spec = phong * max(0.0,ndotl);
                    spec = max(spec, fresnelSpec);
                    spec = spec * _SpecInt;
                    half3 dirSpec = specCol * spec * _LightCol;

                    //环境漫反射
                    half3 evnDiff = diffCol * _EnvUpCol * _EnvDiffInt;

                    //环境镜面反射
                    half reflectInt = max(fresnelSpec,matellic) * specInt;
                    half3 evnSpec = specCol * reflectInt * envCube * _EnvSpecInt;

                    //轮廓光
                    half3 rimLight = _RimCol * fresnelRim * rimInt * max(0.0,nDirWS.g) * _RimInt;

                    //自发光
                    half3 emission = diffCol * emitInt * _EmitInt;



                //综合混合
                half3 finalRGB = (dirDiff + dirSpec) * shadow + evnDiff + evnSpec + rimLight + emission;
                clip(opacity - _Cutoff);

                float4 finalRGBA = float4(finalRGB,1);
                
                //透明剪切
                return finalRGBA;
            }
            ENDCG
        }
    }

    FallBack "Legacy Shaders/Transparent/Cutout/VertexLit"
   
}
