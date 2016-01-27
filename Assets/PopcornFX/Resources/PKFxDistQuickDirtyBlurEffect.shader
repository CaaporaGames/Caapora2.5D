Shader "Hidden/PKFx Blur Shader for Distortion Pass" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		_DistortionTex ("Distortion (RGB)", 2D) = "" {}
		_BlurFactor ("Blur factor [ 0 ; 1 ]", Range (0, 1)) = 0.2
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct v2f {
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;

	};
	
	sampler2D	_MainTex;
	sampler2D	_DistortionTex;
	float		_BlurFactor;
	
	v2f vert (appdata_img v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		o.uv.xy = v.texcoord.xy;



		return o;  
	}
		
	half4 frag (v2f i) : COLOR {
		half4 color = tex2D (_MainTex, i.uv) * half(0.325).xxxx;
		
		float2	offsets = float(tex2D (_DistortionTex, i.uv).z * _BlurFactor / 20.0).xx;

		half4 b1 = offsets.xyxy * half4(1,1, -1,-1);
		half4 b2 = offsets.xyxy * half4(1,-1, -1,1);

		half4 uv01 =  i.uv.xyxy + b1;
		half4 uv02 =  i.uv.xyxy + b2;
		half4 uv21 =  i.uv.xyxy + b1 * 1.5;
		half4 uv22 =  i.uv.xyxy + b2 * 1.5;

		color += 0.1125 * tex2D (_MainTex, uv01.xy);
		color += 0.1125 * tex2D (_MainTex, uv02.xy);
		color += 0.1125 * tex2D (_MainTex, uv01.zw);
		color += 0.1125 * tex2D (_MainTex, uv02.zw);

		color += 0.05625 * tex2D (_MainTex, uv21.xy);
		color += 0.05625 * tex2D (_MainTex, uv22.xy);
		color += 0.05625 * tex2D (_MainTex, uv21.zw);
		color += 0.05625 * tex2D (_MainTex, uv22.zw);
		
		return color;
	} 

	ENDCG
	
Subshader {
 Pass {
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
  }
}
	
Fallback off
	
} // shader