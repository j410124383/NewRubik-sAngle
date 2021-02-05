Shader "Test/NewOutline1"
{
    Properties
    {

        _Color("Main Color",Color) = (1.0,1.0,1.0,1.0)
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]
        _OutlineColor("Outline Color",Color) = (1.0,1.0,1.0,1.0)
        _OutlineWidth("Outline width",Range(1.0,5.0)) = 1.01
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    
    //输入结构
    struct appdata
    {
        float4 vertex : POSITION;
        float4 normal : NORMAL;
    };

    //输出结构
    struct v2f 
    {
        float4 pos : POSITION;
        float4 normal : NORMAL;
    };

    float  _OutlineWidth;
    float4 _OutlineColor;

    v2f vert (appdata v) 
    {
        //VertexOutput o = (VertexOutput)0;
        //o.pos = UnityObjectToClipPos( v.vertex );
        
        //模型外扩
        v.vertex.xyz *= _OutlineWidth;

        //返回模型数据
        v2f o;
        o.pos = UnityObjectToClipPos( v.vertex );
        //o.color = _OutlineColor;
        return o;
    }


    ENDCG
    
    SubShader
    {      

        //外描边贴图
        Pass
        {
            ZWrite off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            //输入颜色
            half4 frag(v2f i):COLOR
            {

                return _OutlineColor;
            }
            
                  
            ENDCG
        }





        //法线贴图
        Pass
        {
            Tags{"Queue" = "Geometry"}
			Tags{"RenderType" = "Opaque"}
			//Tags{"LightMode" = "ShadowCollector"}
            ZWrite On

            Material
            {
                Diffuse[_Color]
                Ambient[_Color]
            }

            Lighting off 
			//SeparateSpecular On
            SetTexture[_MainTex]
            {

                ConstantColor[_Color]
				combine constant lerp(texture) previous
            }

            SetTexture[_MainTex]
            {
			 
               Combine previous * texture
            }


        }


		
    }
}
