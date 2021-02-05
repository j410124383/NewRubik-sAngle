// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32764,y:32706,varname:node_3138,prsc:2|custl-8003-OUT,olwid-1867-A,olcol-1867-RGB;n:type:ShaderForge.SFN_Fresnel,id:1374,x:32066,y:33006,varname:node_1374,prsc:2|EXP-6994-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6994,x:31839,y:33076,ptovrint:False,ptlb:FresnelExp,ptin:_FresnelExp,varname:_FresnelExp,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:1072,x:32249,y:32986,varname:node_1072,prsc:2|A-631-RGB,B-1374-OUT,C-5837-OUT;n:type:ShaderForge.SFN_Color,id:631,x:32066,y:32788,ptovrint:False,ptlb:FresnelCol,ptin:_FresnelCol,varname:_FresnelCol,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:5837,x:32066,y:33190,ptovrint:False,ptlb:OutlineColExp,ptin:_OutlineColExp,varname:_OutlineColExp,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:1867,x:32491,y:33057,ptovrint:False,ptlb:OutlineCol,ptin:_OutlineCol,varname:_OutlineCol,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:8003,x:32491,y:32839,varname:node_8003,prsc:2|A-7283-RGB,B-1072-OUT;n:type:ShaderForge.SFN_Color,id:7283,x:32307,y:32704,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:1867-6994-631-5837-7283;pass:END;sub:END;*/

Shader "Shader Forge/FOutline" {
    Properties {
        _OutlineCol ("OutlineCol", Color) = (0.5,0.5,0.5,1)
        _FresnelExp ("FresnelExp", Float ) = 0
        _FresnelCol ("FresnelCol", Color) = (0.5,0.5,0.5,1)
        _OutlineColExp ("OutlineColExp", Float ) = 0
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _OutlineCol)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                float4 _OutlineCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OutlineCol );
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlineCol_var.a,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float4 _OutlineCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OutlineCol );
                return fixed4(_OutlineCol_var.rgb,0);
            }
            ENDCG
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelExp)
                UNITY_DEFINE_INSTANCED_PROP( float4, _FresnelCol)
                UNITY_DEFINE_INSTANCED_PROP( float, _OutlineColExp)
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
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _FresnelCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelCol );
                float _FresnelExp_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelExp );
                float _OutlineColExp_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OutlineColExp );
                float3 finalColor = (_Color_var.rgb+(_FresnelCol_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp_var)*_OutlineColExp_var));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
