// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2228,x:33831,y:33467,varname:node_2228,prsc:2|emission-4299-OUT;n:type:ShaderForge.SFN_LightVector,id:1546,x:31352,y:32677,varname:node_1546,prsc:2;n:type:ShaderForge.SFN_Vector1,id:9785,x:31757,y:32752,varname:node_9785,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:912,x:31943,y:32678,varname:node_912,prsc:2|A-2924-OUT,B-9785-OUT;n:type:ShaderForge.SFN_NormalVector,id:3214,x:31352,y:32832,prsc:2,pt:False;n:type:ShaderForge.SFN_Reflect,id:6958,x:32126,y:32678,cmnt:rDir  反射光线,varname:node_6958,prsc:2|A-912-OUT,B-1215-OUT;n:type:ShaderForge.SFN_Dot,id:9196,x:32312,y:32678,varname:node_9196,prsc:2,dt:0|A-6958-OUT,B-3226-OUT;n:type:ShaderForge.SFN_ViewVector,id:3226,x:32138,y:32815,varname:node_3226,prsc:2;n:type:ShaderForge.SFN_Max,id:5458,x:32493,y:32678,varname:node_5458,prsc:2|A-9196-OUT,B-4218-OUT;n:type:ShaderForge.SFN_Vector1,id:4218,x:32312,y:32849,varname:node_4218,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:1136,x:32493,y:32855,ptovrint:False,ptlb:SpaculePower,ptin:_SpaculePower,varname:_SpaculePower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:40,max:90;n:type:ShaderForge.SFN_Power,id:9029,x:32693,y:32678,varname:node_9029,prsc:2|VAL-5458-OUT,EXP-1136-OUT;n:type:ShaderForge.SFN_Dot,id:288,x:31961,y:32997,varname:node_288,prsc:2,dt:0|A-4398-OUT,B-7586-OUT;n:type:ShaderForge.SFN_Get,id:4398,x:31767,y:32997,varname:node_4398,prsc:2|IN-3143-OUT;n:type:ShaderForge.SFN_Set,id:3143,x:31544,y:32677,varname:lDir,prsc:2|IN-1546-OUT;n:type:ShaderForge.SFN_Set,id:3671,x:31544,y:32832,varname:nDir,prsc:2|IN-3214-OUT;n:type:ShaderForge.SFN_Get,id:7586,x:31767,y:33061,varname:node_7586,prsc:2|IN-3671-OUT;n:type:ShaderForge.SFN_Set,id:1882,x:32862,y:32678,varname:Phong,prsc:2|IN-9029-OUT;n:type:ShaderForge.SFN_Get,id:2924,x:31757,y:32678,varname:node_2924,prsc:2|IN-3143-OUT;n:type:ShaderForge.SFN_Get,id:1215,x:31943,y:32829,varname:node_1215,prsc:2|IN-3671-OUT;n:type:ShaderForge.SFN_Max,id:5549,x:32138,y:32997,varname:node_5549,prsc:2|A-288-OUT,B-5072-OUT;n:type:ShaderForge.SFN_Vector1,id:5072,x:31961,y:33160,varname:node_5072,prsc:2,v1:0;n:type:ShaderForge.SFN_Set,id:4281,x:32312,y:32997,varname:Lambert,prsc:2|IN-5549-OUT;n:type:ShaderForge.SFN_Get,id:3804,x:32801,y:33020,varname:node_3804,prsc:2|IN-2224-OUT;n:type:ShaderForge.SFN_Get,id:9899,x:32801,y:33084,varname:node_9899,prsc:2|IN-1882-OUT;n:type:ShaderForge.SFN_Multiply,id:4163,x:32312,y:33125,varname:node_4163,prsc:2|A-5549-OUT,B-9552-OUT;n:type:ShaderForge.SFN_Vector3,id:9552,x:32138,y:33226,varname:node_9552,prsc:2,v1:0.5,v2:0.5,v3:0.5;n:type:ShaderForge.SFN_Set,id:2224,x:32472,y:33125,varname:LambertCol,prsc:2|IN-4163-OUT;n:type:ShaderForge.SFN_Add,id:7841,x:32984,y:33020,varname:node_7841,prsc:2|A-3804-OUT,B-9899-OUT;n:type:ShaderForge.SFN_Color,id:2330,x:32984,y:32816,ptovrint:False,ptlb:LightCol,ptin:_LightCol,varname:_LightCol,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9849,x:33179,y:33000,varname:node_9849,prsc:2|A-2330-RGB,B-7841-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:9164,x:33179,y:32826,varname:node_9164,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4540,x:33333,y:32934,varname:node_4540,prsc:2|A-9164-OUT,B-9849-OUT;n:type:ShaderForge.SFN_Set,id:3913,x:33522,y:32934,varname:Lighting,prsc:2|IN-4540-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4203,x:30969,y:33794,varname:node_4203,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-6154-OUT;n:type:ShaderForge.SFN_Max,id:6615,x:31151,y:33794,varname:node_6615,prsc:2|A-4203-OUT,B-3288-OUT;n:type:ShaderForge.SFN_Vector1,id:3288,x:30969,y:33983,varname:node_3288,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4046,x:30969,y:34084,varname:node_4046,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:7226,x:31151,y:33960,varname:node_7226,prsc:2|A-4203-OUT,B-4046-OUT;n:type:ShaderForge.SFN_Max,id:3275,x:31342,y:33960,varname:node_3275,prsc:2|A-7226-OUT,B-3288-OUT;n:type:ShaderForge.SFN_Subtract,id:542,x:31443,y:33793,varname:node_542,prsc:2|A-1249-OUT,B-6615-OUT;n:type:ShaderForge.SFN_Vector1,id:1249,x:31286,y:33860,varname:node_1249,prsc:2,v1:1;n:type:ShaderForge.SFN_Subtract,id:5434,x:31590,y:33871,varname:node_5434,prsc:2|A-542-OUT,B-3275-OUT;n:type:ShaderForge.SFN_Color,id:9304,x:31361,y:33126,ptovrint:False,ptlb:Evn_UP,ptin:_Evn_UP,varname:_Evn_UP,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:1544,x:31361,y:33360,ptovrint:False,ptlb:Evn_Slider,ptin:_Evn_Slider,varname:_Evn_Slider,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:1118,x:31361,y:33578,ptovrint:False,ptlb:Evn_Down,ptin:_Evn_Down,varname:_Evn_Down,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:8588,x:31855,y:33605,varname:node_8588,prsc:2|A-9304-RGB,B-6615-OUT;n:type:ShaderForge.SFN_Multiply,id:6000,x:31855,y:33782,varname:node_6000,prsc:2|A-1544-RGB,B-5434-OUT;n:type:ShaderForge.SFN_Multiply,id:3028,x:31855,y:33964,varname:node_3028,prsc:2|A-1118-RGB,B-3275-OUT;n:type:ShaderForge.SFN_Add,id:5184,x:32033,y:33751,varname:node_5184,prsc:2|A-8588-OUT,B-6000-OUT;n:type:ShaderForge.SFN_Add,id:3488,x:32209,y:33843,varname:node_3488,prsc:2|A-5184-OUT,B-3028-OUT;n:type:ShaderForge.SFN_Tex2d,id:7467,x:32616,y:33625,ptovrint:False,ptlb:AoTex,ptin:_AoTex,varname:_AoTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5837,x:32804,y:33817,varname:node_5837,prsc:2|A-7467-RGB,B-468-OUT;n:type:ShaderForge.SFN_Get,id:6154,x:30786,y:33794,varname:node_6154,prsc:2|IN-3671-OUT;n:type:ShaderForge.SFN_Set,id:215,x:32992,y:33781,varname:EvnLight,prsc:2|IN-5837-OUT;n:type:ShaderForge.SFN_Get,id:9262,x:32992,y:33587,varname:node_9262,prsc:2|IN-215-OUT;n:type:ShaderForge.SFN_Get,id:6052,x:32992,y:33654,varname:node_6052,prsc:2|IN-3913-OUT;n:type:ShaderForge.SFN_Add,id:4299,x:33393,y:33592,varname:node_4299,prsc:2|A-9262-OUT,B-6052-OUT;n:type:ShaderForge.SFN_Multiply,id:4982,x:32389,y:33972,varname:node_4982,prsc:2|A-3488-OUT,B-5807-OUT;n:type:ShaderForge.SFN_Slider,id:5807,x:32077,y:34059,ptovrint:False,ptlb:EvnCol,ptin:_EvnCol,varname:_EvnCol,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:468,x:32619,y:33953,varname:node_468,prsc:2|A-9552-OUT,B-4982-OUT;proporder:1136-2330-9304-1544-1118-7467-5807;pass:END;sub:END;*/

Shader "Unlit/OldSchoolPlusShader" {
    Properties {
        _SpaculePower ("SpaculePower", Range(1, 90)) = 40
        _LightCol ("LightCol", Color) = (0.5,0.5,0.5,1)
        _Evn_UP ("Evn_UP", Color) = (1,1,1,1)
        _Evn_Slider ("Evn_Slider", Color) = (0.5,0.5,0.5,1)
        _Evn_Down ("Evn_Down", Color) = (0,0,0,1)
        _AoTex ("AoTex", 2D) = "white" {}
        _EvnCol ("EvnCol", Range(0, 1)) = 1
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _AoTex; uniform float4 _AoTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _SpaculePower)
                UNITY_DEFINE_INSTANCED_PROP( float4, _LightCol)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_UP)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_Slider)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_Down)
                UNITY_DEFINE_INSTANCED_PROP( float, _EvnCol)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
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
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _AoTex_var = tex2D(_AoTex,TRANSFORM_TEX(i.uv0, _AoTex));
                float3 node_9552 = float3(0.5,0.5,0.5);
                float4 _Evn_UP_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Evn_UP );
                float3 nDir = i.normalDir;
                float node_4203 = nDir.g;
                float node_3288 = 0.0;
                float node_6615 = max(node_4203,node_3288);
                float4 _Evn_Slider_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Evn_Slider );
                float node_3275 = max((node_4203*(-1.0)),node_3288);
                float4 _Evn_Down_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Evn_Down );
                float _EvnCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _EvnCol );
                float3 EvnLight = (_AoTex_var.rgb*(node_9552*((((_Evn_UP_var.rgb*node_6615)+(_Evn_Slider_var.rgb*((1.0-node_6615)-node_3275)))+(_Evn_Down_var.rgb*node_3275))*_EvnCol_var)));
                float4 _LightCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _LightCol );
                float3 lDir = lightDirection;
                float node_5549 = max(dot(lDir,nDir),0.0);
                float3 LambertCol = (node_5549*node_9552);
                float _SpaculePower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SpaculePower );
                float Phong = pow(max(dot(reflect((lDir*(-1.0)),nDir),viewDirection),0.0),_SpaculePower_var);
                float3 Lighting = (attenuation*(_LightCol_var.rgb*(LambertCol+Phong)));
                float3 emissive = (EvnLight+Lighting);
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _AoTex; uniform float4 _AoTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _SpaculePower)
                UNITY_DEFINE_INSTANCED_PROP( float4, _LightCol)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_UP)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_Slider)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_Down)
                UNITY_DEFINE_INSTANCED_PROP( float, _EvnCol)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
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
                float attenuation = LIGHT_ATTENUATION(i);
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
