// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1319,x:33510,y:32820,varname:node_1319,prsc:2|emission-9522-OUT;n:type:ShaderForge.SFN_NormalVector,id:173,x:31629,y:32904,prsc:2,pt:False;n:type:ShaderForge.SFN_ComponentMask,id:8474,x:31797,y:32904,varname:node_8474,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-173-OUT;n:type:ShaderForge.SFN_Max,id:9279,x:31979,y:32904,varname:node_9279,prsc:2|A-8474-OUT,B-825-OUT;n:type:ShaderForge.SFN_Vector1,id:825,x:31797,y:33093,varname:node_825,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8982,x:31797,y:33194,varname:node_8982,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:5972,x:31979,y:33070,varname:node_5972,prsc:2|A-8474-OUT,B-8982-OUT;n:type:ShaderForge.SFN_Max,id:7841,x:32170,y:33070,varname:node_7841,prsc:2|A-5972-OUT,B-825-OUT;n:type:ShaderForge.SFN_Subtract,id:1880,x:32271,y:32903,varname:node_1880,prsc:2|A-7824-OUT,B-9279-OUT;n:type:ShaderForge.SFN_Vector1,id:7824,x:32114,y:32970,varname:node_7824,prsc:2,v1:1;n:type:ShaderForge.SFN_Subtract,id:345,x:32418,y:32981,varname:node_345,prsc:2|A-1880-OUT,B-7841-OUT;n:type:ShaderForge.SFN_Color,id:9713,x:32189,y:32236,ptovrint:False,ptlb:Evn_UP,ptin:_Evn_UP,varname:_Evn_UP,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:8090,x:32189,y:32470,ptovrint:False,ptlb:Evn_Slider,ptin:_Evn_Slider,varname:_Evn_Slider,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:6508,x:32189,y:32688,ptovrint:False,ptlb:Evn_Down,ptin:_Evn_Down,varname:_Evn_Down,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:6968,x:32683,y:32715,varname:node_6968,prsc:2|A-9713-RGB,B-9279-OUT;n:type:ShaderForge.SFN_Multiply,id:9707,x:32683,y:32892,varname:node_9707,prsc:2|A-8090-RGB,B-345-OUT;n:type:ShaderForge.SFN_Multiply,id:396,x:32683,y:33074,varname:node_396,prsc:2|A-6508-RGB,B-7841-OUT;n:type:ShaderForge.SFN_Add,id:4247,x:32861,y:32861,varname:node_4247,prsc:2|A-6968-OUT,B-9707-OUT;n:type:ShaderForge.SFN_Add,id:8627,x:33037,y:32953,varname:node_8627,prsc:2|A-4247-OUT,B-396-OUT;n:type:ShaderForge.SFN_Tex2d,id:2043,x:33037,y:32690,ptovrint:False,ptlb:node_2043,ptin:_node_2043,varname:_node_2043,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9522,x:33247,y:32917,varname:node_9522,prsc:2|A-2043-RGB,B-8627-OUT;proporder:9713-8090-6508-2043;pass:END;sub:END;*/

Shader "Unlit/EvnShader" {
    Properties {
        _Evn_UP ("Evn_UP", Color) = (1,1,1,1)
        _Evn_Slider ("Evn_Slider", Color) = (0.5,0.5,0.5,1)
        _Evn_Down ("Evn_Down", Color) = (0,0,0,1)
        _node_2043 ("node_2043", 2D) = "white" {}
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
            uniform sampler2D _node_2043; uniform float4 _node_2043_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_UP)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_Slider)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Evn_Down)
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
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _node_2043_var = tex2D(_node_2043,TRANSFORM_TEX(i.uv0, _node_2043));
                float4 _Evn_UP_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Evn_UP );
                float node_8474 = i.normalDir.g;
                float node_825 = 0.0;
                float node_9279 = max(node_8474,node_825);
                float4 _Evn_Slider_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Evn_Slider );
                float node_7841 = max((node_8474*(-1.0)),node_825);
                float4 _Evn_Down_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Evn_Down );
                float3 emissive = (_node_2043_var.rgb*(((_Evn_UP_var.rgb*node_9279)+(_Evn_Slider_var.rgb*((1.0-node_9279)-node_7841)))+(_Evn_Down_var.rgb*node_7841)));
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
