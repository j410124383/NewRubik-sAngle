// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:434,x:32900,y:32772,varname:node_434,prsc:2|emission-7129-OUT;n:type:ShaderForge.SFN_HalfVector,id:3716,x:31879,y:32832,varname:node_3716,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1297,x:31879,y:32972,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:1168,x:32040,y:32916,varname:node_1168,prsc:2,dt:0|A-3716-OUT,B-1297-OUT;n:type:ShaderForge.SFN_Max,id:9347,x:32192,y:32980,varname:node_9347,prsc:2|A-1168-OUT,B-8496-OUT;n:type:ShaderForge.SFN_Vector1,id:8496,x:32040,y:33086,varname:node_8496,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:4177,x:32019,y:33212,ptovrint:False,ptlb:SpaculerPower,ptin:_SpaculerPower,varname:_SpaculerPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:40,max:90;n:type:ShaderForge.SFN_Power,id:3173,x:32366,y:33072,varname:node_3173,prsc:2|VAL-9347-OUT,EXP-4177-OUT;n:type:ShaderForge.SFN_Color,id:2482,x:32247,y:32655,ptovrint:False,ptlb:BaseCol,ptin:_BaseCol,varname:_BaseCol,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:7129,x:32684,y:32936,varname:node_7129,prsc:2|A-9968-OUT,B-3173-OUT;n:type:ShaderForge.SFN_Tex2d,id:9192,x:32247,y:32408,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:20719f118b18e8746bd55a4033b9933b,ntxv:0,isnm:False|UVIN-63-UVOUT;n:type:ShaderForge.SFN_ScreenPos,id:63,x:32079,y:32408,varname:node_63,prsc:2,sctp:0;n:type:ShaderForge.SFN_Lerp,id:9968,x:32550,y:32664,varname:node_9968,prsc:2|A-9192-RGB,B-2482-RGB,T-121-OUT;n:type:ShaderForge.SFN_Slider,id:121,x:32247,y:32873,ptovrint:False,ptlb:IsBase,ptin:_IsBase,varname:_IsBase,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4643178,max:1;proporder:4177-2482-9192-121;pass:END;sub:END;*/

Shader "Unlit/Blinn_PhongShader" {
    Properties {
        _SpaculerPower ("SpaculerPower", Range(1, 90)) = 40
        _BaseCol ("BaseCol", Color) = (1,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _IsBase ("IsBase", Range(0, 1)) = 0.4643178
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _SpaculerPower)
                UNITY_DEFINE_INSTANCED_PROP( float4, _BaseCol)
                UNITY_DEFINE_INSTANCED_PROP( float, _IsBase)
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
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX((sceneUVs * 2 - 1).rg, _MainTex));
                float4 _BaseCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BaseCol );
                float _IsBase_var = UNITY_ACCESS_INSTANCED_PROP( Props, _IsBase );
                float _SpaculerPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpaculerPower );
                float3 emissive = (lerp(_MainTex_var.rgb,_BaseCol_var.rgb,_IsBase_var)+pow(max(dot(halfDirection,i.normalDir),0.0),_SpaculerPower_var));
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _SpaculerPower)
                UNITY_DEFINE_INSTANCED_PROP( float4, _BaseCol)
                UNITY_DEFINE_INSTANCED_PROP( float, _IsBase)
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
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
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
