// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33763,y:32827,varname:node_3138,prsc:2|emission-9152-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:2940,x:32365,y:32897,varname:node_2940,prsc:2;n:type:ShaderForge.SFN_LightVector,id:651,x:32365,y:32739,varname:node_651,prsc:2;n:type:ShaderForge.SFN_Dot,id:5847,x:32526,y:32807,varname:node_5847,prsc:2,dt:0|A-651-OUT,B-2940-OUT;n:type:ShaderForge.SFN_RemapRange,id:2295,x:32728,y:32807,varname:node_2295,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-5847-OUT;n:type:ShaderForge.SFN_Append,id:3713,x:32944,y:32860,varname:node_3713,prsc:2|A-2295-OUT,B-1-OUT;n:type:ShaderForge.SFN_Tex2d,id:2203,x:33119,y:32860,ptovrint:False,ptlb:RampTex,ptin:_RampTex,varname:_RampTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3713-OUT;n:type:ShaderForge.SFN_Slider,id:1,x:32608,y:33036,ptovrint:False,ptlb:RampType,ptin:_RampType,varname:_RampType,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:4201,x:33313,y:32955,varname:node_4201,prsc:2|A-2203-RGB,B-4609-OUT;n:type:ShaderForge.SFN_Slider,id:4609,x:32913,y:33107,ptovrint:False,ptlb:EvnReflect,ptin:_EvnReflect,varname:_EvnReflect,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Max,id:1025,x:33319,y:33171,varname:node_1025,prsc:2|A-5847-OUT,B-7950-OUT;n:type:ShaderForge.SFN_Vector1,id:7950,x:33135,y:33233,varname:node_7950,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:9152,x:33540,y:33024,varname:node_9152,prsc:2|A-4201-OUT,B-2259-OUT;n:type:ShaderForge.SFN_Power,id:2259,x:33505,y:33193,varname:node_2259,prsc:2|VAL-1025-OUT,EXP-6518-OUT;n:type:ShaderForge.SFN_Slider,id:6518,x:33094,y:33396,ptovrint:False,ptlb:SpecularPow,ptin:_SpecularPow,varname:_SpecularPow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:30,max:90;proporder:2203-1-4609-6518;pass:END;sub:END;*/

Shader "Shader Forge/FakeEvnReflectShader" {
    Properties {
        _RampTex ("RampTex", 2D) = "white" {}
        _RampType ("RampType", Range(0, 1)) = 0
        _EvnReflect ("EvnReflect", Range(0, 1)) = 0.5
        _SpecularPow ("SpecularPow", Range(1, 90)) = 30
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
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _RampTex; uniform float4 _RampTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _RampType)
                UNITY_DEFINE_INSTANCED_PROP( float, _EvnReflect)
                UNITY_DEFINE_INSTANCED_PROP( float, _SpecularPow)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float node_5847 = dot(lightDirection,viewReflectDirection);
                float _RampType_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RampType );
                float2 node_3713 = float2((node_5847*0.5+0.5),_RampType_var);
                float4 _RampTex_var = tex2D(_RampTex,TRANSFORM_TEX(node_3713, _RampTex));
                float _EvnReflect_var = UNITY_ACCESS_INSTANCED_PROP( Props, _EvnReflect );
                float _SpecularPow_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpecularPow );
                float3 emissive = ((_RampTex_var.rgb*_EvnReflect_var)+pow(max(node_5847,0.0),_SpecularPow_var));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma target 3.0
            uniform sampler2D _RampTex; uniform float4 _RampTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _RampType)
                UNITY_DEFINE_INSTANCED_PROP( float, _EvnReflect)
                UNITY_DEFINE_INSTANCED_PROP( float, _SpecularPow)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float3 finalColor = 0;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
