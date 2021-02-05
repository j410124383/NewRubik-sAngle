Shader "MyShadow/LineShadow"
{
    Properties
    {
        _MainTex    ("Texture", 2D)           = "white" {}
        _MainCol    ("基本色", Color)          =(0.5,0.5,0.5,1.0)
        _ShadowCol  ("阴影色", Color)          =(0.5,0.5,0.5,1.0)
        _rot ("Rotation", Range(0,360))       = 1.0
		_OffsetSize ("Offset", Range(0,0.05)) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            
            uniform sampler2D _MainTex ;  uniform float4 _MainTex_ST ;
            uniform fixed4 _MainCol;
            uniform fixed4 _ShadowCol;
            uniform fixed _rot;
            uniform fixed _OffsetSize;


            struct VertexInput
            {
                float4 vertex   : POSITION;
                float2 uv0      : TEXCOORD0;
                float4 normal   : NORMAL;
            };

            struct VertexOutput
            {
                float4 pos      : SV_POSITION;
                float2 uv0   : TEXCOORD0;
                float4 posWS    : TEXCOORD1;
                float3 nDirWS   : TEXCOORD2;
            };

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv0 = v.uv0 * _MainTex_ST.xy + _MainTex_ST.zw;
                o.posWS = mul(unity_ObjectToWorld ,v.vertex);  
                o.nDirWS = UnityObjectToWorldNormal(v.normal); //变换法线方向 OS》WS
 	
                return o;
            }

            fixed4 frag (VertexOutput i) : Color
            {
                //向量准备
                    //法线向量
                    fixed3 nDirWS = i.nDirWS;
                    
                    //光线向量
                    fixed3 lDirWS =_WorldSpaceLightPos0.xyz;
 
                //中间量准备
                fixed ndotl = dot(nDirWS,lDirWS);

                //纹理采样
                fixed4 var_MainTex = tex2D (_MainTex, i.uv0);

                //光照模型准备
                fixed3 baseCol = var_MainTex.rgb * _MainCol;
                fixed lambert = max(0.0,ndotl);
    
                fixed fMask = step(0.0, lambert - 0.5);
                fixed uMask = step(0.0, max(0.0,nDirWS.g) - 0.5);         
                fixed bMask = 1 - (fMask + uMask);

                fixed3 envCol = (fMask + uMask) * baseCol  +  bMask * _ShadowCol;
                
                fixed3 dirLighting = envCol;

                fixed3 finalRGB = dirLighting; 

                fixed4 finalRGBA = fixed4(finalRGB, 1.0);
                return finalRGBA;
            }
            ENDCG
        }
    }
}
