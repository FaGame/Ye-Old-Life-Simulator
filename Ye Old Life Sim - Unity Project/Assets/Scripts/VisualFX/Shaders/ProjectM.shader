Shader "Custom/ProjectM" 
{
Properties 
{
	_GlowColor ("Glow Color", Color) = (1.0,0.75,0.8,1.0)
	_Coefficient ("Coefficient", Range(0.01,5.0)) = 1.0
	_Power ("Power", Range(0.01,5.0)) = 2.0
}

Category 
{
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	SubShader 
	{
		Pass 
		{
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _GlowColor;
			float _Coefficient;
			float _Power;
			
			struct appdata_t 
			{
				float4 vertex : POSITION;
			    float3 normal : NORMAL;
			};

			struct v2f 
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				fixed4 color : COLOR;
			};
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				//o.color = v.color;
				return o;
			}

			sampler2D_float _CameraDepthTexture;
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 viewCameraToVertex = normalize(ObjSpaceViewDir(i.vertex));
				float intensity = pow(_Coefficient + dot(i.normal, viewCameraToVertex), _Power);
				return fixed4(_GlowColor.xyz, intensity);
			}
			ENDCG 
		}
	}	
}
}
