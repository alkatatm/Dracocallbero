// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "azure[Sky]/azure[Moon]"
{
	Properties
	{
		_MoonTexture ("Moon Texture", 2D) = "white" {}
		_Saturation("Saturation", Range(0.5,2)) = 0.5
		_Penunbra("Penunbra", Range(0,4)) = 2
		_Shadow("Shadow", Range(0,0.1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "IgnoreProjector"="True" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex     : SV_POSITION;
				float3 WorldPos   : TEXCOORD0;
				float3 nDirection : NORMAL;
			};

			sampler2D _MoonTexture;
			#define   _pi	3.14159265359f
			fixed3    _SunDir;

			float4    _MoonColor;
			float     _LightIntensity;
			float     _Saturation;
			float     _Penunbra;
			float     _Shadow;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex                = UnityObjectToClipPos(v.vertex);
//				float4x4 inverseMatrix  = _World2Object;
						 o.nDirection   = normalize(float3(mul(float4(v.normal, 0.0), (float4x4)unity_WorldToObject).xyz));;
				         o.WorldPos     = v.vertex.xyz;
				return o;
			}
			
			fixed4 frag (v2f IN) : SV_Target
			{
				float3 pos = normalize(IN.WorldPos);
				float2 uv  = float2(atan2(pos.z, pos.x)+1.5, -acos(pos.y)) / float2(2.0*_pi, _pi);

//				float3   lightDirection  = normalize(float3(_WorldSpaceLightPos0.xyz));
				float3   lightColor      = _MoonColor * max(0.0, dot(IN.nDirection, normalize(_SunDir)));
				float4	 finalLightColor = float4(lightColor, 1) + _Shadow;// + UNITY_LIGHTMODEL_AMBIENT;

				// sample the texture
				fixed4 moonSampler = tex2D(_MoonTexture, uv) * (pow(finalLightColor,_Penunbra) * _LightIntensity );
				return pow(moonSampler,_Saturation);
			}
			ENDCG
		}
	}
}
