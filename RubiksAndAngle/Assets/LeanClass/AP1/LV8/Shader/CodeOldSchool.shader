Shader "MyShader/LO8/CodeOldSchool"
{
    Properties
    {
        _BaseCol         ("BaseCol : 基础色" , Color)               = (0.5,0.5,0.5,1)
        _LightCol        ("LightCol : 光颜色", Color)               = (0.5,0.5,0.5,1)
        _SpaculePower    ("SpaculePower ： 高光次幂", Range(1, 90)) = 40
        _Occlusion       ("Occlusion ： AO图", 2D) = "white" {}
        _EnvInt          ("EnvInt : 环境光强度", Range(0, 1))       = 1
        _Env_UPCol       ("Env_UPCol : 环境天顶颜色", Color)        = (1.0,1.0,1.0,1.0)
        _Env_SliderCol   ("Env_SliderCol ：环境水平颜色", Color)    = (0.5,0.5,0.5,1)
        _Env_DownCol     ("Env_DownCol : 环境地表颜色", Color)      = (0,0,0,1)
    }
    SubShader
    {
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
          
            // include文件
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadowsS
            #pragma target 3.0

            //输入参数
            uniform float3  _BaseCol;       
            uniform float3  _LightCol;      
            uniform float  _SpaculePower;   
            uniform sampler2D _Occlusion;     
            uniform float  _EnvInt;        
            uniform float3  _Env_UPCol;      
            uniform float3  _Env_SliderCol;  
            uniform float3 _Env_DownCol;     
           
           
           
            //输入结构
            struct VertexInput {
                float4 vertex : POSITION;    //顶点信息
                float4 noemal : NORMAL;      //法线信息
                float2 uv0    : TEXCOORD0;   //将模型uv信息输入进来 0 通道  共4个通道
                
           
            };

            //输出结构
            struct VertexOutput {
                float4 pos  : SV_POSITION;  // 剪裁空间
                float2 uv0    : TEXCOORD0;    // UV信息
                float4 posWS  : TEXCOORD1;    // 世界空间顶点位置
                float3 nDirWS : TEXCOORD2;    // 世界空间法线方向
                LIGHTING_COORDS(3,4)          // 投影坐标
            };

            //输入结构>>>顶点Shader>>>输出结构
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;                   //新建输出结构   
                    o.pos = UnityObjectToClipPos( v.vertex );      //变换顶点位置 OS》CS       
                    o.uv0 = v.uv0;                                 //传递UV数据
                    o.posWS = mul(unity_ObjectToWorld ,v.vertex);     //变换顶点位置 OS》WS  
                    o.nDirWS = UnityObjectToWorldNormal(v.noemal); //变换法线方向 OS》WS
                   TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;//返回输出结构
            }

            //输出结构>>>像素
            float4 frag(VertexOutput i) : COLOR {
                //准备向量
                float3 nDir = normalize(i.nDirWS);
                float3 lDir = _WorldSpaceLightPos0.xyz;
                float3 vDir = normalize(_WorldSpaceCameraPos.xyz - i.posWS.xyz);
                float3 rDir = reflect(-lDir,nDir);

                //准备点积结果
                float nDotl = dot(nDir,lDir);
                float vDotr = dot(vDir,rDir);

                //光照模型(直接光)
                float shadow  = LIGHT_ATTENUATION(i);
                float lambert = max(0.0,nDotl);
                float phong   = pow(max(0.0,vDotr),_SpaculePower);
                float3 dirLighting = (_BaseCol * lambert + phong) *_LightCol * shadow;

                //光照模型(环境光)
                float upMask = max(0.0,nDir.g);
                float downMask = max(0.0,-nDir.g);
                float sideMask = 1.0 - upMask - downMask;
                //混合环境光
                float3 envMask = _Env_UPCol * upMask +  _Env_DownCol * downMask + _Env_SliderCol * sideMask;
                float occlusion = tex2D(_Occlusion , i.uv0);
                float3 envLighting = _BaseCol * envMask * occlusion;

                //返回结果
                float3 finalRGB = dirLighting + envLighting;
                return float4(finalRGB,1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
