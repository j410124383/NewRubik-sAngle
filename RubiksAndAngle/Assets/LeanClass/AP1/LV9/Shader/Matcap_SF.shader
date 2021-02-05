// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:8827,x:33366,y:32929,varname:node_8827,prsc:2|emission-2460-OUT;n:type:ShaderForge.SFN_Transform,id:4709,x:31945,y:32978,varname:node_4709,prsc:2,tffrom:2,tfto:0|IN-369-RGB;n:type:ShaderForge.SFN_Tex2d,id:369,x:31778,y:32978,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:_NormalMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Transform,id:7265,x:32117,y:32870,varname:node_7265,prsc:2,tffrom:0,tfto:3|IN-4709-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:7773,x:32280,y:32870,varname:node_7773,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7265-XYZ;n:type:ShaderForge.SFN_RemapRange,id:5392,x:32454,y:32870,varname:node_5392,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-7773-OUT;n:type:ShaderForge.SFN_Tex2d,id:2813,x:32636,y:32870,ptovrint:False,ptlb:Matcap,ptin:_Matcap,varname:_Matcap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a4259037a0bba7b41884b33f9653b20e,ntxv:0,isnm:False|UVIN-5392-OUT;n:type:ShaderForge.SFN_Fresnel,id:2692,x:32454,y:33093,varname:node_2692,prsc:2|NRM-4709-XYZ,EXP-2936-OUT;n:type:ShaderForge.SFN_Slider,id:2936,x:32120,y:33190,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:_FresnelPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:8491,x:32817,y:33077,varname:node_8491,prsc:2|A-2813-RGB,B-2692-OUT;n:type:ShaderForge.SFN_Slider,id:4982,x:32738,y:33258,ptovrint:False,ptlb:EnvSpaculer,ptin:_EnvSpaculer,varname:_EnvSpaculer,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Multiply,id:2460,x:33048,y:33077,varname:node_2460,prsc:2|A-8491-OUT,B-4982-OUT;proporder:369-2813-2936-4982;pass:END;sub:END;*/

Shader "Unlit/Matcap_SF" {
    Properties {
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _Matcap ("Matcap", 2D) = "white" {}
        _FresnelPower ("FresnelPower", Range(0, 10)) = 1
        _EnvSpaculer ("EnvSpaculer", Range(0, 5)) = 1
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
            uniform sampler2D _Matcap; uniform float4 _Matcap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelPower)
                UNITY_DEFINE_INSTANCED_PROP( float, _EnvSpaculer)
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
                float3 node_4709 = mul( _NormalMap_var.rgb, tangentTransform ).xyz;
                float2 node_5392 = (mul( UNITY_MATRIX_V, float4(node_4709.rgb,0) ).xyz.rgb.rg*0.5+0.5);
                float4 _Matcap_var = tex2D(_Matcap,TRANSFORM_TEX(node_5392, _Matcap));
                float _FresnelPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelPower );
                float _EnvSpaculer_var = UNITY_ACCESS_INSTANCED_PROP( Props, _EnvSpaculer );
                float3 emissive = ((_Matcap_var.rgb*pow(1.0-max(0,dot(node_4709.rgb, viewDirection)),_FresnelPower_var))*_EnvSpaculer_var);
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
