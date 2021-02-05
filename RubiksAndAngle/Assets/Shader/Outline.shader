// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34858,y:33432,varname:node_3138,prsc:2|emission-1431-OUT,clip-5808-OUT;n:type:ShaderForge.SFN_NormalVector,id:6764,x:32024,y:32547,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:3725,x:32202,y:32450,varname:node_3725,prsc:2,dt:0|A-7445-OUT,B-6764-OUT;n:type:ShaderForge.SFN_Clamp01,id:7776,x:32390,y:32450,varname:node_7776,prsc:2|IN-3725-OUT;n:type:ShaderForge.SFN_Step,id:716,x:32633,y:32700,varname:node_716,prsc:2|A-7776-OUT,B-5790-OUT;n:type:ShaderForge.SFN_Vector1,id:5790,x:32476,y:32800,varname:node_5790,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:6282,x:32816,y:32759,varname:node_6282,prsc:2|A-716-OUT,B-7704-OUT;n:type:ShaderForge.SFN_Vector1,id:7704,x:32633,y:32867,varname:node_7704,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Step,id:6741,x:32618,y:32138,varname:node_6741,prsc:2|A-5031-OUT,B-7776-OUT;n:type:ShaderForge.SFN_Vector1,id:5031,x:32435,y:32116,varname:node_5031,prsc:2,v1:0.75;n:type:ShaderForge.SFN_Vector1,id:4172,x:32618,y:32060,varname:node_4172,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9557,x:32802,y:32115,varname:node_9557,prsc:2|A-4172-OUT,B-6741-OUT;n:type:ShaderForge.SFN_Add,id:6336,x:33524,y:32414,varname:node_6336,prsc:2|A-9557-OUT,B-8303-OUT,C-6282-OUT;n:type:ShaderForge.SFN_LightVector,id:7445,x:32013,y:32335,varname:node_7445,prsc:2;n:type:ShaderForge.SFN_Subtract,id:2215,x:32981,y:32408,varname:node_2215,prsc:2|A-2811-OUT,B-2438-OUT;n:type:ShaderForge.SFN_Add,id:2438,x:32788,y:32445,varname:node_2438,prsc:2|A-6741-OUT,B-716-OUT;n:type:ShaderForge.SFN_Vector1,id:2811,x:32788,y:32343,varname:node_2811,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:9819,x:32981,y:32568,varname:node_9819,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:8303,x:33175,y:32408,varname:node_8303,prsc:2|A-2215-OUT,B-9819-OUT;n:type:ShaderForge.SFN_ViewVector,id:3203,x:32020,y:33364,varname:node_3203,prsc:2;n:type:ShaderForge.SFN_Dot,id:8071,x:32198,y:33342,varname:node_8071,prsc:2,dt:0|A-6764-OUT,B-3203-OUT;n:type:ShaderForge.SFN_Clamp01,id:8459,x:32382,y:33342,varname:node_8459,prsc:2|IN-8071-OUT;n:type:ShaderForge.SFN_Step,id:4472,x:32760,y:33158,varname:node_4472,prsc:2|A-1137-OUT,B-8459-OUT;n:type:ShaderForge.SFN_Vector1,id:1137,x:32577,y:33136,varname:node_1137,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:1046,x:32760,y:33080,varname:node_1046,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9540,x:32944,y:33135,varname:node_9540,prsc:2|A-1046-OUT,B-4472-OUT;n:type:ShaderForge.SFN_Step,id:2732,x:32763,y:33511,varname:node_2732,prsc:2|A-8459-OUT,B-3133-OUT;n:type:ShaderForge.SFN_Vector1,id:3133,x:32606,y:33611,varname:node_3133,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:8052,x:32946,y:33570,varname:node_8052,prsc:2|A-2732-OUT,B-6000-OUT;n:type:ShaderForge.SFN_Vector1,id:6000,x:32763,y:33678,varname:node_6000,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:9666,x:33712,y:33304,varname:node_9666,prsc:2|A-9540-OUT,B-6493-OUT,C-8052-OUT;n:type:ShaderForge.SFN_Subtract,id:2004,x:33310,y:33314,varname:node_2004,prsc:2|A-7269-OUT,B-5983-OUT;n:type:ShaderForge.SFN_Add,id:5983,x:33117,y:33351,varname:node_5983,prsc:2|A-4472-OUT,B-2732-OUT;n:type:ShaderForge.SFN_Vector1,id:7269,x:33117,y:33249,varname:node_7269,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:2070,x:33310,y:33474,varname:node_2070,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:6493,x:33504,y:33314,varname:node_6493,prsc:2|A-2004-OUT,B-2070-OUT;n:type:ShaderForge.SFN_Add,id:1616,x:34202,y:32842,varname:node_1616,prsc:2|A-6336-OUT,B-9666-OUT,C-5499-OUT;n:type:ShaderForge.SFN_Multiply,id:8866,x:34369,y:33035,varname:node_8866,prsc:2|A-1616-OUT,B-2940-OUT;n:type:ShaderForge.SFN_Vector1,id:2940,x:34202,y:33129,varname:node_2940,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Depth,id:8045,x:33396,y:32905,varname:node_8045,prsc:2;n:type:ShaderForge.SFN_Dot,id:454,x:33555,y:32992,varname:node_454,prsc:2,dt:0|A-8045-OUT,B-2840-X;n:type:ShaderForge.SFN_ObjectPosition,id:2840,x:33396,y:33053,varname:node_2840,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5499,x:33782,y:33065,varname:node_5499,prsc:2|A-454-OUT,B-9125-OUT;n:type:ShaderForge.SFN_Vector1,id:9125,x:33603,y:33202,varname:node_9125,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Set,id:7181,x:32595,y:32445,varname:lDn,prsc:2|IN-7776-OUT;n:type:ShaderForge.SFN_Get,id:3824,x:33687,y:33827,varname:node_3824,prsc:2|IN-7181-OUT;n:type:ShaderForge.SFN_Min,id:5808,x:34212,y:33787,varname:node_5808,prsc:2|A-8649-OUT,B-4257-OUT;n:type:ShaderForge.SFN_Vector1,id:8649,x:34052,y:33739,varname:node_8649,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:9718,x:33708,y:33563,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:0.5;n:type:ShaderForge.SFN_Divide,id:4257,x:33916,y:33807,varname:node_4257,prsc:2|A-9718-A,B-6384-OUT;n:type:ShaderForge.SFN_Set,id:8788,x:32337,y:33683,varname:lDv,prsc:2|IN-8459-OUT;n:type:ShaderForge.SFN_Get,id:6384,x:33687,y:33918,varname:node_6384,prsc:2|IN-8788-OUT;n:type:ShaderForge.SFN_Append,id:3569,x:34431,y:33612,varname:node_3569,prsc:2|A-9718-RGB,B-5808-OUT;n:type:ShaderForge.SFN_Vector1,id:8575,x:34212,y:34008,varname:node_8575,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1431,x:34413,y:33921,varname:node_1431,prsc:2|A-5808-OUT,B-8575-OUT;proporder:9718;pass:END;sub:END;*/

Shader "MyShader/Outline" {
    Properties {
        _Color ("Color", Color) = (0,0,0,0.5)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float node_8459 = saturate(dot(i.normalDir,viewDirection));
                float lDv = node_8459;
                float node_5808 = min(1.0,(_Color_var.a/lDv));
                clip(node_5808 - 0.5);
////// Lighting:
////// Emissive:
                float node_1431 = (node_5808*0.5);
                float3 emissive = float3(node_1431,node_1431,node_1431);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float node_8459 = saturate(dot(i.normalDir,viewDirection));
                float lDv = node_8459;
                float node_5808 = min(1.0,(_Color_var.a/lDv));
                clip(node_5808 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
