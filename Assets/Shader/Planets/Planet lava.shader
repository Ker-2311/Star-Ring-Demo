// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Planet lava"
{
	Properties
	{
		_Albedocolor("Albedo color", Color) = (1,1,1,0)
		[NoScaleOffset]_Albedo("Albedo", 2D) = "white" {}
		_Desaturation("Desaturation", Range( 0 , 1)) = 0
		_cloudscolor("clouds color", Color) = (1,1,1,0.547)
		[NoScaleOffset]_CloudsAlpha("Clouds (Alpha)", 2D) = "black" {}
		_Cloudspeed("Cloud speed", Float) = 1
		[NoScaleOffset]_Normalmap("Normal map", 2D) = "bump" {}
		_Normalintensity("Normal intensity", Range( 0 , 1)) = 0
		_Citiescolor("Cities color", Color) = (1,0.815662,0.559,0)
		[NoScaleOffset]_Emissivecities("Emissive (cities)", 2D) = "black" {}
		_Citiesoffset("Cities offset", Float) = 0
		[NoScaleOffset]_watermask("water mask", 2D) = "black" {}
		_Gloss("Gloss", Range( 0.01 , 1)) = 0.81
		[NoScaleOffset]_LookupSunset("Lookup Sunset", 2D) = "white" {}
		_Subatmospherecolor("Sub atmosphere color", Color) = (1,1,1,0)
		_Subatmosphereglobalintensity("Sub atmosphere global intensity", Range( 0 , 10)) = 1
		_Subatmospherepower("Sub atmosphere power", Range( 0.1 , 5)) = 5
		_AmbiantLightcontrol("Ambiant Light control", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite On
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float2 texcoord_0;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 texcoord_1;
		};

		uniform float _Normalintensity;
		uniform sampler2D _Normalmap;
		uniform float4 _cloudscolor;
		uniform sampler2D _CloudsAlpha;
		uniform float _Cloudspeed;
		uniform sampler2D _LookupSunset;
		uniform sampler2D _Albedo;
		uniform float4 _Albedocolor;
		uniform float _Desaturation;
		uniform float _Subatmosphereglobalintensity;
		uniform float _Subatmospherepower;
		uniform float4 _Subatmospherecolor;
		uniform float4 _AmbiantLightcontrol;
		uniform sampler2D _Emissivecities;
		uniform float _Citiesoffset;
		uniform float4 _Citiescolor;
		uniform sampler2D _watermask;
		uniform float _Gloss;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
			float2 temp_cast_0 = (_Citiesoffset).xx;
			o.texcoord_1.xy = v.texcoord.xy * float2( 1,1 ) + temp_cast_0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normalmap = i.uv_texcoord;
			float4 appendResult41 = (float4(( _Cloudspeed / 80.0 ) , 0.0 , 0.0 , 0.0));
			float4 tex2DNode26 = tex2D( _CloudsAlpha, ( float4( i.texcoord_0, 0.0 , 0.0 ) + ( appendResult41 * _Time.x ) ).xy );
			float temp_output_81_0 = ( 1.0 - ( _cloudscolor.a * tex2DNode26.a ) );
			float3 lerpResult195 = lerp( float3(0,0,1) , UnpackScaleNormal( tex2D( _Normalmap, uv_Normalmap ) ,_Normalintensity ) , temp_output_81_0);
			o.Normal = lerpResult195;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult94 = dot( ase_worldlightDir , ase_worldNormal );
			float2 temp_cast_2 = (dotResult94).xx;
			float2 uv_Albedo = i.uv_texcoord;
			float4 lerpResult66 = lerp( ( tex2D( _Albedo, uv_Albedo ) * _Albedocolor ) , _cloudscolor , ( _cloudscolor.a * tex2DNode26.a ));
			float3 desaturateVar142 = lerp( lerpResult66.rgb,dot(lerpResult66.rgb,float3(0.299,0.587,0.114)).xxx,_Desaturation);
			o.Albedo = ( tex2D( _LookupSunset, temp_cast_2 ) * float4( desaturateVar142 , 0.0 ) ).rgb;
			float dotResult247 = dot( ase_worldNormal , ase_worldlightDir );
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNDotV9 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode9 = ( 0.0 + _Subatmosphereglobalintensity * pow( 1.0 - fresnelNDotV9, _Subatmospherepower ) );
			float smoothstepResult192 = smoothstep( 0.0 , 10.0 , fresnelNode9);
			float4 clampResult74 = clamp( ( ( dotResult247 * smoothstepResult192 ) * _Subatmospherecolor ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float clampResult72 = clamp( -( dotResult247 - 0.65 ) , 0.0 , 1.0 );
			float smoothstepResult281 = smoothstep( 0.3 , 0.7 , clampResult72);
			float smoothstepResult308 = smoothstep( 0.2 , 1.0 , ( 1.0 - tex2DNode26.a ));
			float2 uv_watermask = i.uv_texcoord;
			float4 tex2DNode82 = tex2D( _watermask, uv_watermask );
			float4 temp_output_311_0 = ( ( clampResult74 + ( ( ( 1.0 - _AmbiantLightcontrol ) * ( ( ( smoothstepResult281 * ( tex2D( _Emissivecities, i.texcoord_1 ) * _Citiescolor ) ) * smoothstepResult308 ) * ( 1.0 - tex2DNode82.a ) ) ) * 2.0 ) ) + tex2DNode82 );
			float4 lerpResult272 = lerp( temp_output_311_0 , ( float4( ( desaturateVar142 * ( 1.0 - ( -0.5 + dotResult247 ) ) ) , 0.0 ) + temp_output_311_0 ) , ( 0.5 * _AmbiantLightcontrol ));
			float4 clampResult190 = clamp( lerpResult272 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Emission = clampResult190.rgb;
			o.Smoothness = ( ( temp_output_81_0 * _Gloss ) * tex2DNode82 ).r;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows noambient nolightmap  nodynlightmap nodirlightmap nofog vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
