// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33522,y:32555,varname:node_3138,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:8735,x:31483,y:32829,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:1256,x:31483,y:33020,varname:node_1256,prsc:2;n:type:ShaderForge.SFN_Dot,id:6837,x:31634,y:32943,varname:node_6837,prsc:2,dt:0|A-8735-OUT,B-1256-OUT;n:type:ShaderForge.SFN_Vector1,id:4953,x:31618,y:33153,varname:node_4953,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1818,x:31799,y:33041,varname:node_1818,prsc:2|A-6837-OUT,B-4953-OUT;n:type:ShaderForge.SFN_Add,id:8292,x:31975,y:33137,varname:node_8292,prsc:2|A-1818-OUT,B-4953-OUT;n:type:ShaderForge.SFN_NormalVector,id:5267,x:31547,y:32893,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:3529,x:31547,y:33084,varname:node_3529,prsc:2;n:type:ShaderForge.SFN_Dot,id:4802,x:31698,y:33007,varname:node_4802,prsc:2,dt:0|A-5267-OUT,B-3529-OUT;n:type:ShaderForge.SFN_Vector1,id:4819,x:31682,y:33217,varname:node_4819,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3611,x:31863,y:33105,varname:node_3611,prsc:2|A-4802-OUT,B-4819-OUT;n:type:ShaderForge.SFN_Add,id:3889,x:32039,y:33201,varname:node_3889,prsc:2|A-3611-OUT,B-4819-OUT;n:type:ShaderForge.SFN_NormalVector,id:105,x:32920,y:32597,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:1912,x:32920,y:32764,varname:node_1912,prsc:2;n:type:ShaderForge.SFN_Dot,id:7439,x:33079,y:32686,varname:node_7439,prsc:2,dt:0|A-105-OUT,B-1912-OUT;n:type:ShaderForge.SFN_Vector1,id:9973,x:33079,y:32854,varname:node_9973,prsc:2,v1:0.5;pass:END;sub:END;*/

Shader "Shader Forge/DotShader" {
    Properties {
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
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float3 finalColor = 0;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
