// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "azure[Sky]/azure[Fog]"
{
//	Properties
//	{
//		
//	}
	SubShader
	{
		Cull Front
		ZWrite Off
		ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile HDR_ON HDR_OFF
			#include "UnityCG.cginc"

			uniform float3    _Br;
			uniform float3    _Br2;
			uniform float3    _Bm;
			uniform float3    _Brm;    //Br + Bm
			uniform float3    _mieG;
			uniform float     _SunIntensity;
			uniform float     _MoonIntensity;
			uniform float     _Kr;
			uniform float     _Km;
			uniform float     _Altitude;
			uniform float     _pi316;
			uniform float     _pi14;
			uniform float     _pi;

			uniform float     _Exposure;
			uniform float     _SkyLuminance;
			uniform float     _SkyDarkness;
			uniform float     _SunsetPower;
			uniform float4    _SunsetColor;

			uniform float     _ColorCorrection;
			uniform float     _LinearFog;
			
			uniform float3    _SunDir;
			uniform float3    _MoonDir;
			uniform float4x4  _MoonMatrix;

			uniform float4    _MoonBrightColor;

			uniform float4    _GroundCloseColor;
			uniform float4    _GroundFarColor;
			uniform float     _FarColorDistance;
			uniform float     _FarColorIntensity;

			uniform float4    _NormalFogColor;
			uniform float     _NormalFogDistance;
			uniform float     _ScatteringFogDistance;
			uniform float     _BlendFogDistance;
			uniform float4    _GlobalColor;

			uniform sampler2D _MainTex;
			uniform sampler2D _CameraDepthTexture;
			uniform float4x4  _FrustumCorners;
			uniform float4    _MainTex_TexelSize;

//			uniform float     _DenseFogDistance;
			uniform float4    _DenseFogColor;
			uniform float     _DenseFogIntensity;
			uniform float     _DenseFogAltitude;

			struct appdata
			{
				float4 vertex   : POSITION;
			    float4 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 Position        : SV_POSITION;
    			float3 Fade            : TEXCOORD0; // sunFade,  mix.
    			float3 moonPos         : TEXCOORD1;
    			float2 uv 	           : TEXCOORD2;
				float4 interpolatedRay : TEXCOORD3;
				float2 uv_depth        : TEXCOORD4;
			};

			v2f vert (appdata v)
			{
				v2f o;
    			UNITY_INITIALIZE_OUTPUT(v2f, o);
    			
    			half index = v.vertex.z;
				v.vertex.z = 0.1;
				o.uv       = v.texcoord.xy;
				o.uv_depth = v.texcoord.xy;
				#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1-o.uv.y;
				#endif
				o.interpolatedRay   = _FrustumCorners[(int)index];
				o.interpolatedRay.w = index;
    			
    			o.Position = UnityObjectToClipPos(v.vertex);
    			
    			o.Fade.x = saturate( _SunDir.y+0.25 );                             		 	     			// Fade the sun ("daysky") when cross the horizon.
			    o.Fade.y = saturate(clamp(1.0 - _SunDir.y,0.0,0.5));                          	 			// Mix sunset"(fex)" with daysky"(1-fex)".
			    o.Fade.z = (-_SunDir.y + _Altitude) * ((_MoonDir.y+0.25) + _Altitude);                      //Fade the moon Bright when is day or cross the horizon

				return o;
			}

			fixed4 frag (v2f IN) : SV_Target
			{
			   //-------------------------------------------------------------------------------------------------------
			   //-------------------------------------------Directions--------------------------------------------------
			   float  depth       = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture,IN.uv_depth)));
               float3 viewDir     = normalize(depth * IN.interpolatedRay);
			   float sunCosTheta  = dot( viewDir, _SunDir );
			   viewDir            = normalize(depth * IN.interpolatedRay) + float3(0.0,_Altitude,0.0); // Change the horizon altitude.
			   float FadeW        = saturate(dot(-_MoonMatrix[2].xyz,viewDir));                        //Fade the other side moon bright

			   //-------------------------------------------------------------------------------------------------------
			   //-------------------------------------------Extinction--------------------------------------------------
			   float  zenith = acos(saturate(viewDir.y));
			   float  z      = (cos(zenith) + 0.15 * pow(93.885 - ((zenith * 180.0) / _pi), -1.253));
			   float  SR     = _Kr  / z;
			   float  SM     = _Km  / z;
			   float3 fex    = exp(-(_Br*SR  + _Bm*SM));  // Original fex calculation.
			   float3 fex2   = exp(-(_Br2*SR + _Bm*SM)); // Fex calculation with rayleigh coefficient == 3. For the sunset.

			   //-------------------------------------------------------------------------------------------------------
			   //-----------------------------------------Sun Scattering------------------------------------------------
			   //float  rayPhase = 1.0 + pow(cosTheta,2.0);                          // Preetham rayleigh phase function.
			   float  rayPhase = 2.0 + 0.5 * pow(sunCosTheta,2.0);                   // Rayleigh phase function based on the Nielsen's paper.
			   float  miePhase = _mieG.x / pow(_mieG.y - _mieG.z * sunCosTheta,1.5); // The Henyey-Greenstein phase function.
			    
			   float3 BrTheta  = _pi316 * _Br * rayPhase;
			   float3 BmTheta  = _pi14  * _Bm * miePhase;
			   float3 BrmTheta = (BrTheta + BmTheta * 2.0) / (_Brm * 0.75);        // Brm is "Br+Bm", and the sum is already made in the Control Script.

			   float3 inScatter = BrmTheta * _SunIntensity * (1.0 - fex);
			   inScatter *= saturate((lerp( _SunsetPower , pow(2000.0 * BrmTheta * fex2,0.5),IN.Fade.y) * 0.05));
			   inScatter *= _SkyLuminance * _SunsetColor.rgb;
			   inScatter *= pow((1-fex),_SkyDarkness);
			   inScatter *= IN.Fade.x; // Sun fade in the horizon.


			   //-------------------------------------------------------------------------------------------------------
			   //--------------------------------------------Night Sky--------------------------------------------------
			   float  nightIntensity = 0.25;
			   float3 nightSky   = saturate((pow( 1-fex2, 2.0) * nightIntensity) * (1-IN.Fade.x)); // Defaut night sky color
			   		  nightSky  *= saturate(pow((1-fex2),_SkyDarkness));
			          nightSky  *= _SkyLuminance;

			   float3 groundColor = lerp(_GroundCloseColor, _GroundFarColor, (viewDir.y + _FarColorDistance));
			          nightSky    = saturate(lerp(groundColor, nightSky, saturate(dot(viewDir.y + _FarColorIntensity, float3(0,1,0)))) * (1-fex));
//			          nightSky    = saturate(lerp(groundColor, nightSky, viewDir.y + _LerpNightSkyDistance) * (1-fex));

			   float3 moonBright  = saturate( (_MoonBrightColor.rgb * _MoonIntensity) * pow(dot(viewDir, _MoonDir),5.0)  * IN.Fade.z ) * 3 * FadeW;
			   nightSky += moonBright;

			   //*******************************************************************************************************
			   //-------------------------------------------------------------------------------------------------------
			   //------------------------------------------Sky finalization---------------------------------------------
			   float3 finalSky = (inScatter + nightSky);
		   	   ////////////////
			   // tonemaping //
			   #ifndef HDR_ON
			   finalSky = saturate( 1.0 - exp( -_Exposure * finalSky ) );
			   #endif
			   //////////////////////
			   // Color Correction //
			   finalSky = pow(finalSky,_ColorCorrection);

//			   return float4(finalSky,1.0);


			   ///////////////
			   // Apply Fog //
			   float  Mask       =    saturate( lerp(1.0, 0.0, depth) * _ProjectionParams.z );																			    
			   float3 screen     =    tex2D(_MainTex, IN.uv); // Original scene

			   float3 normalFog    =  lerp(screen,_NormalFogColor, Mask);
			          normalFog    =  lerp(screen,normalFog, pow(saturate(depth * _NormalFogDistance),_LinearFog));
			          normalFog    =  pow(normalFog,_ColorCorrection);
			   
			   
			   float3 inScatteringFog =    lerp(screen,finalSky, Mask);                  						                             // Creating the fog color.
			          inScatteringFog =    lerp(screen, inScatteringFog * _GlobalColor, pow(saturate(depth * _ScatteringFogDistance),_LinearFog)); // Mixing the fog with the scene, according to the depth.
			   
			   float3 finalFog = lerp(normalFog, inScatteringFog, pow(saturate(depth * _BlendFogDistance),_LinearFog));

			   //Dense Fog
			   float  denseFogAltitude =viewDir.y - _DenseFogAltitude;
			   float3 denseFog = lerp(_DenseFogColor, finalFog, saturate(pow(denseFogAltitude,5)));
			   denseFog = lerp(finalFog, denseFog, saturate(pow(depth * 25,0.25)) * _DenseFogIntensity);
			   
			   return float4(denseFog,1.0);
//			   return float4(Mask,Mask,Mask,1.0);             // To see the mask
//			   return float4(depth*10,depth*10,depth*10,1.0); // To see the depth

			}
			ENDCG
		}
	}
}
