// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:5323,x:33693,y:32649,varname:node_5323,prsc:2|emission-2911-OUT;n:type:ShaderForge.SFN_Tex2d,id:2201,x:32226,y:32772,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:_NormalMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Transform,id:4264,x:32406,y:32772,varname:node_4264,prsc:2,tffrom:2,tfto:0|IN-2201-RGB;n:type:ShaderForge.SFN_ViewVector,id:5569,x:32226,y:32524,varname:node_5569,prsc:2;n:type:ShaderForge.SFN_Vector1,id:2447,x:32226,y:32665,varname:node_2447,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:1692,x:32392,y:32524,varname:node_1692,prsc:2|A-5569-OUT,B-2447-OUT;n:type:ShaderForge.SFN_Reflect,id:8378,x:32584,y:32524,varname:node_8378,prsc:2|A-1692-OUT,B-4264-XYZ;n:type:ShaderForge.SFN_Slider,id:9249,x:32597,y:32711,ptovrint:False,ptlb:CubemapMip,ptin:_CubemapMip,varname:_CubemapMip,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:7;n:type:ShaderForge.SFN_Cubemap,id:4881,x:32939,y:32610,ptovrint:False,ptlb:node_4881,ptin:_node_4881,varname:_node_4881,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0|DIR-8378-OUT,MIP-9249-OUT;n:type:ShaderForge.SFN_Fresnel,id:2095,x:32939,y:32874,varname:node_2095,prsc:2|NRM-4264-XYZ,EXP-8651-OUT;n:type:ShaderForge.SFN_Slider,id:8651,x:32471,y:32926,ptovrint:False,ptlb:FresenelPow,ptin:_FresenelPow,varname:_FresenelPow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:9777,x:33176,y:32733,varname:node_9777,prsc:2|A-4881-RGB,B-2095-OUT;n:type:ShaderForge.SFN_Slider,id:1432,x:33085,y:32944,ptovrint:False,ptlb:EnvSpecInt,ptin:_EnvSpecInt,varname:_EnvSpecInt,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:5;n:type:ShaderForge.SFN_Multiply,id:2911,x:33399,y:32733,varname:node_2911,prsc:2|A-9777-OUT,B-1432-OUT;proporder:2201-9249-4881-8651-1432;pass:END;sub:END;*/

Shader "Unlit/Cubemap_SF" {
    Properties {
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _CubemapMip ("CubemapMip", Range(0, 7)) = 0
        _node_4881 ("node_4881", Cube) = "_Skybox" {}
        _FresenelPow ("FresenelPow", Range(0, 1)) = 1
        _EnvSpecInt ("EnvSpecInt", Range(0, 5)) = 0.2
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
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform samplerCUBE _node_4881;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _CubemapMip)
                UNITY_DEFINE_INSTANCED_PROP( float, _FresenelPow)
                UNITY_DEFINE_INSTANCED_PROP( float, _EnvSpecInt)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 node_4264 = mul( _NormalMap_var.rgb, tangentTransform ).xyz;
                float _CubemapMip_var = UNITY_ACCESS_INSTANCED_PROP( Props, _CubemapMip );
                float _FresenelPow_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresenelPow );
                float _EnvSpecInt_var = UNITY_ACCESS_INSTANCED_PROP( Props, _EnvSpecInt );
                float3 emissive = ((texCUBElod(_node_4881,float4(reflect((viewDirection*(-1.0)),node_4264.rgb),_CubemapMip_var)).rgb*pow(1.0-max(0,dot(node_4264.rgb, viewDirection)),_FresenelPow_var))*_EnvSpecInt_var);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
