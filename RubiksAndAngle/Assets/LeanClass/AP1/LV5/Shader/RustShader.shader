// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1248,x:33185,y:32791,varname:node_1248,prsc:2|emission-8952-OUT;n:type:ShaderForge.SFN_LightVector,id:804,x:31288,y:32613,varname:node_804,prsc:2;n:type:ShaderForge.SFN_Vector1,id:2089,x:31693,y:32688,varname:node_2089,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:7028,x:31879,y:32614,varname:node_7028,prsc:2|A-1542-OUT,B-2089-OUT;n:type:ShaderForge.SFN_NormalVector,id:219,x:31288,y:32768,prsc:2,pt:False;n:type:ShaderForge.SFN_Reflect,id:4903,x:32062,y:32614,cmnt:rDir  反射光线,varname:node_4903,prsc:2|A-7028-OUT,B-9334-OUT;n:type:ShaderForge.SFN_Dot,id:8518,x:32248,y:32614,varname:node_8518,prsc:2,dt:0|A-4903-OUT,B-605-OUT;n:type:ShaderForge.SFN_ViewVector,id:605,x:32074,y:32751,varname:node_605,prsc:2;n:type:ShaderForge.SFN_Max,id:994,x:32429,y:32614,varname:node_994,prsc:2|A-8518-OUT,B-4110-OUT;n:type:ShaderForge.SFN_Vector1,id:4110,x:32248,y:32785,varname:node_4110,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:8620,x:32429,y:32791,ptovrint:False,ptlb:SpaculePower,ptin:_SpaculePower,varname:_SpaculePower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:40,max:90;n:type:ShaderForge.SFN_Power,id:1373,x:32629,y:32614,varname:node_1373,prsc:2|VAL-994-OUT,EXP-8620-OUT;n:type:ShaderForge.SFN_Dot,id:36,x:31897,y:32933,varname:node_36,prsc:2,dt:0|A-256-OUT,B-1047-OUT;n:type:ShaderForge.SFN_Get,id:256,x:31703,y:32933,varname:node_256,prsc:2|IN-6288-OUT;n:type:ShaderForge.SFN_Set,id:6288,x:31480,y:32613,varname:Ldir,prsc:2|IN-804-OUT;n:type:ShaderForge.SFN_Set,id:1821,x:31480,y:32768,varname:NDir,prsc:2|IN-219-OUT;n:type:ShaderForge.SFN_Get,id:1047,x:31703,y:32997,varname:node_1047,prsc:2|IN-1821-OUT;n:type:ShaderForge.SFN_Set,id:5991,x:32798,y:32614,varname:Phong,prsc:2|IN-1373-OUT;n:type:ShaderForge.SFN_Get,id:1542,x:31693,y:32614,varname:node_1542,prsc:2|IN-6288-OUT;n:type:ShaderForge.SFN_Get,id:9334,x:31879,y:32765,varname:node_9334,prsc:2|IN-1821-OUT;n:type:ShaderForge.SFN_Max,id:6911,x:32074,y:32933,varname:node_6911,prsc:2|A-36-OUT,B-848-OUT;n:type:ShaderForge.SFN_Vector1,id:848,x:31897,y:33096,varname:node_848,prsc:2,v1:0;n:type:ShaderForge.SFN_Set,id:9807,x:32248,y:32933,varname:Lambert,prsc:2|IN-6911-OUT;n:type:ShaderForge.SFN_Add,id:8952,x:32813,y:32999,varname:node_8952,prsc:2|A-1449-OUT,B-1405-OUT;n:type:ShaderForge.SFN_Get,id:1449,x:32565,y:33035,varname:node_1449,prsc:2|IN-9807-OUT;n:type:ShaderForge.SFN_Get,id:1405,x:32565,y:33091,varname:node_1405,prsc:2|IN-5991-OUT;proporder:8620;pass:END;sub:END;*/

Shader "Unlit/RustShader" {
    Properties {
        _SpaculePower ("SpaculePower", Range(1, 90)) = 40
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
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
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
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
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
                float3 Ldir = lightDirection;
                float3 NDir = i.normalDir;
                float Lambert = max(dot(Ldir,NDir),0.0);
                float _SpaculePower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpaculePower );
                float Phong = pow(max(dot(reflect((Ldir*(-1.0)),NDir),viewDirection),0.0),_SpaculePower_var);
                float node_8952 = (Lambert+Phong);
                float3 emissive = float3(node_8952,node_8952,node_8952);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #pragma multi_compile_fog
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
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
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
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
