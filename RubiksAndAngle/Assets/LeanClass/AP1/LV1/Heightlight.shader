// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33650,y:32795,varname:node_3138,prsc:2|emission-5172-OUT;n:type:ShaderForge.SFN_NormalVector,id:9874,x:31855,y:32704,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:360,x:32095,y:32778,varname:node_360,prsc:2,dt:0|A-9874-OUT,B-9451-OUT;n:type:ShaderForge.SFN_LightVector,id:9451,x:31855,y:32886,varname:node_9451,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3987,x:32285,y:32788,varname:node_3987,prsc:2|A-2036-OUT,B-360-OUT;n:type:ShaderForge.SFN_Vector1,id:2036,x:32247,y:32546,varname:node_2036,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:8759,x:32468,y:32676,varname:node_8759,prsc:2|A-2036-OUT,B-3987-OUT;n:type:ShaderForge.SFN_Relay,id:2158,x:32001,y:33200,varname:node_2158,prsc:2|IN-9874-OUT;n:type:ShaderForge.SFN_Relay,id:4400,x:31834,y:33200,varname:node_4400,prsc:2|IN-9451-OUT;n:type:ShaderForge.SFN_Normalize,id:7856,x:32076,y:33350,varname:node_7856,prsc:2|IN-6048-OUT;n:type:ShaderForge.SFN_Dot,id:5535,x:32305,y:33101,varname:node_5535,prsc:2,dt:0|A-2158-OUT,B-7856-OUT;n:type:ShaderForge.SFN_If,id:9359,x:32607,y:33134,varname:node_9359,prsc:2|A-5535-OUT,B-1991-OUT,GT-4759-OUT,EQ-510-OUT,LT-510-OUT;n:type:ShaderForge.SFN_Vector1,id:510,x:32340,y:32971,varname:node_510,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4759,x:32179,y:32971,varname:node_4759,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:1991,x:32277,y:33270,ptovrint:False,ptlb:size,ptin:_size,varname:_size,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.99,max:1;n:type:ShaderForge.SFN_Vector1,id:3228,x:32376,y:33439,varname:node_3228,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8837,x:32221,y:33439,varname:node_8837,prsc:2,v1:1;n:type:ShaderForge.SFN_If,id:3000,x:32606,y:33450,varname:node_3000,prsc:2|A-7394-OUT,B-826-OUT,GT-8837-OUT,EQ-3228-OUT,LT-3228-OUT;n:type:ShaderForge.SFN_Dot,id:7394,x:32320,y:33547,varname:node_7394,prsc:2,dt:0|A-2158-OUT,B-6012-OUT;n:type:ShaderForge.SFN_Normalize,id:6012,x:32088,y:33634,varname:node_6012,prsc:2|IN-1073-OUT;n:type:ShaderForge.SFN_Max,id:5081,x:32758,y:33301,varname:node_5081,prsc:2|A-9359-OUT,B-3000-OUT;n:type:ShaderForge.SFN_Clamp01,id:9289,x:32937,y:33301,varname:node_9289,prsc:2|IN-5081-OUT;n:type:ShaderForge.SFN_Color,id:5266,x:32937,y:33080,ptovrint:False,ptlb:color,ptin:_color,varname:_color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:3467,x:33252,y:33221,varname:node_3467,prsc:2|A-3816-RGB,B-5266-RGB,T-9289-OUT;n:type:ShaderForge.SFN_Append,id:9514,x:32687,y:32657,varname:node_9514,prsc:2|A-2036-OUT,B-8759-OUT;n:type:ShaderForge.SFN_Tex2d,id:3816,x:32930,y:32697,ptovrint:False,ptlb:node_3816,ptin:_node_3816,varname:_node_3816,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9514-OUT;n:type:ShaderForge.SFN_Fresnel,id:1287,x:32971,y:33546,varname:node_1287,prsc:2|EXP-7447-OUT;n:type:ShaderForge.SFN_Color,id:1693,x:32971,y:33716,ptovrint:False,ptlb:f_color,ptin:_f_color,varname:_f_color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1477,x:33152,y:33611,varname:node_1477,prsc:2|A-1287-OUT,B-1693-RGB;n:type:ShaderForge.SFN_Blend,id:5172,x:33447,y:33380,varname:node_5172,prsc:2,blmd:6,clmp:True|SRC-3467-OUT,DST-1477-OUT;n:type:ShaderForge.SFN_Vector4,id:7302,x:31637,y:33456,varname:node_7302,prsc:2,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Add,id:6048,x:31902,y:33350,varname:node_6048,prsc:2|A-4400-OUT,B-7302-OUT;n:type:ShaderForge.SFN_Add,id:1073,x:31845,y:33639,varname:node_1073,prsc:2|A-4400-OUT,B-8066-OUT;n:type:ShaderForge.SFN_Vector4,id:8066,x:31637,y:33657,varname:node_8066,prsc:2,v1:0.1,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Vector1,id:826,x:32320,y:33694,varname:node_826,prsc:2,v1:0.998;n:type:ShaderForge.SFN_Vector1,id:7447,x:32791,y:33650,varname:node_7447,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:8387,x:32520,y:33881,varname:node_8387,prsc:2,v1:0;proporder:3816-1991-5266-1693;pass:END;sub:END;*/

Shader "API/01/HaftLightShader" {
    Properties {
        _node_3816 ("node_3816", 2D) = "white" {}
        _size ("size", Range(0, 1)) = 0.99
        _color ("color", Color) = (0.5,0.5,0.5,1)
        _f_color ("f_color", Color) = (0.5,0.5,0.5,1)
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
            uniform sampler2D _node_3816; uniform float4 _node_3816_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _size)
                UNITY_DEFINE_INSTANCED_PROP( float4, _color)
                UNITY_DEFINE_INSTANCED_PROP( float4, _f_color)
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
                float node_2036 = 0.5;
                float2 node_9514 = float2(node_2036,(node_2036+(node_2036*dot(i.normalDir,lightDirection))));
                float4 _node_3816_var = tex2D(_node_3816,TRANSFORM_TEX(node_9514, _node_3816));
                float4 _color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _color );
                float3 node_2158 = i.normalDir;
                float3 node_4400 = lightDirection;
                float _size_var = UNITY_ACCESS_INSTANCED_PROP( Props, _size );
                float node_9359_if_leA = step(dot(node_2158,normalize((node_4400+float4(0,0,0,0)))),_size_var);
                float node_9359_if_leB = step(_size_var,dot(node_2158,normalize((node_4400+float4(0,0,0,0)))));
                float node_510 = 0.0;
                float node_3000_if_leA = step(dot(node_2158,normalize((node_4400+float4(0.1,0,0,0)))),0.998);
                float node_3000_if_leB = step(0.998,dot(node_2158,normalize((node_4400+float4(0.1,0,0,0)))));
                float node_3228 = 0.0;
                float4 _f_color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _f_color );
                float3 emissive = saturate((1.0-(1.0-lerp(_node_3816_var.rgb,_color_var.rgb,saturate(max(lerp((node_9359_if_leA*node_510)+(node_9359_if_leB*1.0),node_510,node_9359_if_leA*node_9359_if_leB),lerp((node_3000_if_leA*node_3228)+(node_3000_if_leB*1.0),node_3228,node_3000_if_leA*node_3000_if_leB)))))*(1.0-(pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)*_f_color_var.rgb))));
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
            uniform sampler2D _node_3816; uniform float4 _node_3816_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _size)
                UNITY_DEFINE_INSTANCED_PROP( float4, _color)
                UNITY_DEFINE_INSTANCED_PROP( float4, _f_color)
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
