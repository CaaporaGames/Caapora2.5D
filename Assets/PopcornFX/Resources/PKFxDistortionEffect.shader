Shader "Hidden/PKFxDistortion Shader" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_DistortionTex ("Distortion (RGB)", 2D) = "black" {}
}

	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc"
	
			uniform sampler2D _MainTex;
			uniform sampler2D _DistortionTex;
			
			uniform float4 _MainTex_TexelSize;
			
			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			v2f vert( appdata_img v )
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
				return o;
			}
			
			float4 SampleColorAt(float2 uvOffset)
			{
				return tex2D(_MainTex, uvOffset);
			}
			
			float4 frag (v2f i) : COLOR
			{
				float4 _DistBlurFactor = float4(15.0f, 15.0f, 0.75f, 1.0f);
				float4 baseOffset = (tex2D(_DistortionTex, i.uv));
				baseOffset *= _DistBlurFactor;
			
				float4 color1 = SampleColorAt(baseOffset.xy * 0.01 + i.uv);
				float4 color2 = SampleColorAt(baseOffset.xy * 0.0125 + i.uv);
				float4 color3 = SampleColorAt(baseOffset.xy * 0.015 + i.uv);
				float4 color4 = SampleColorAt(baseOffset.xy * 0.0175 + i.uv);

				return float4(lerp(color1.x, color2.x, 0.5f), lerp(color2.y, color3.y, 0.5f), color4.z, 1);
			}
ENDCG
		}
	}

Fallback off

}
