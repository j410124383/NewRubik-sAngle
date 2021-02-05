#ifndef MY_CGINC
#define MY_CGINC

	// 三颜色(顶部，侧面，地面)差值环境光方法
	float3 TriColAmbient ( float3 n , float3 uCol , float3 sCol , float3 dCol)
	{
		
		     //环境漫反射
            float uMask = max(0.0,n.g);
            float dMask = max(0.0,-n.g);
            float sMask = 1.0 - uMask - dMask;
            float3 envCol = uCol * uMask 
								  + dCol * dMask 
								  + sCol * sMask;
		return envCol;
	} 

#endif