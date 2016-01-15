Shader "Custom/heightmap"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 localpos : TEXCOORD2;
			};

			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.localpos = v.vertex;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			half4 frag (v2f i) : SV_Target
			{
				// sample the texture
				half grayscale = i.localpos.y / 512;
				half4 col = half4(grayscale, grayscale, grayscale, 1);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
