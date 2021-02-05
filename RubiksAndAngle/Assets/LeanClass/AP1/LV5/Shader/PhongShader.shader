// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33206,y:32961,varname:node_3138,prsc:2|emission-6813-OUT;n:type:ShaderForge.SFN_LightVector,id:9243,x:31958,y:32878,varname:node_9243,prsc:2;n:type:ShaderForge.SFN_Vector1,id:3252,x:31958,y:33029,varname:node_3252,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:2023,x:32118,y:32940,varname:node_2023,prsc:2|A-9243-OUT,B-3252-OUT;n:type:ShaderForge.SFN_NormalVector,id:2499,x:32118,y:33119,prsc:2,pt:False;n:type:ShaderForge.SFN_Reflect,id:7298,x:32279,y:33015,cmnt:rDir  反射光线,varname:node_7298,prsc:2|A-2023-OUT,B-2499-OUT;n:type:ShaderForge.SFN_Dot,id:5748,x:32454,y:33083,varname:node_5748,prsc:2,dt:0|A-7298-OUT,B-3745-OUT;n:type:ShaderForge.SFN_ViewVector,id:3745,x:32279,y:33193,varname:node_3745,prsc:2;n:type:ShaderForge.SFN_Max,id:5884,x:32617,y:33174,varname:node_5884,prsc:2|A-5748-OUT,B-6583-OUT;n:type:ShaderForge.SFN_Vector1,id:6583,x:32454,y:33254,varname:node_6583,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:4762,x:32512,y:33397,ptovrint:False,ptlb:SpaculePower,ptin:_SpaculePower,varname:_SpaculePower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:40,max:90;n:type:ShaderForge.SFN_Power,id:1659,x:32868,y:33233,varname:node_1659,prsc:2|VAL-5884-OUT,EXP-4762-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:4489,x:32313,y:32685,varname:node_4489,prsc:2;n:type:ShaderForge.SFN_LightVector,id:3831,x:32313,y:32817,varname:node_3831,prsc:2;n:type:ShaderForge.SFN_Dot,id:3269,x:32473,y:32761,varname:node_3269,prsc:2,dt:0|A-4489-OUT,B-3831-OUT;n:type:ShaderForge.SFN_Dot,id:717,x:32313,y:32438,varname:node_717,prsc:2,dt:0|A-9243-OUT,B-2499-OUT;n:type:ShaderForge.SFN_Multiply,id:5576,x:32588,y:32443,varname:node_5576,prsc:2|A-717-OUT,B-7448-OUT;n:type:ShaderForge.SFN_Vector1,id:7448,x:32422,y:32578,varname:node_7448,prsc:0,v1:0.5;n:type:ShaderForge.SFN_Add,id:7502,x:32735,y:32550,varname:node_7502,prsc:2|A-5576-OUT,B-7448-OUT;n:type:ShaderForge.SFN_Append,id:5999,x:32798,y:32709,varname:node_5999,prsc:0|A-7502-OUT,B-703-OUT;n:type:ShaderForge.SFN_Vector1,id:703,x:32618,y:32728,varname:node_703,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:6813,x:32970,y:33000,varname:node_6813,prsc:2|A-5999-OUT,B-1659-OUT;proporder:4762;pass:END;sub:END;*/

Shader "Shader Forge/PhongShader" {
    Properties {
        _SpaculePower ("SpaculePower", Range(1, 90)) = 40
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
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _SpaculePower)
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                fixed node_7448 = 0.5;
                float _SpaculePower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpaculePower );
                float3 emissive = float3((float2(((dot(lightDirection,i.normalDir)*node_7448)+node_7448),0.1)+pow(max(dot(reflect((lightDirection*(-1.0)),i.normalDir),viewDirection),0.0),_SpaculePower_var)),0.0);
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
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _SpaculePower)
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
