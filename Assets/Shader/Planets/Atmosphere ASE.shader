// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Atmosphere ASE"
{
	Properties
	{
		_Atmospheresize("Atmosphere size", Range( 0 , 10)) = 0
		_Atmospherecolor("Atmosphere color", Color) = (1,1,1,0)
		_Atmospherecircle("Atmosphere circle", Range( 0 , 1)) = 0
		_AtmosphereSharpness("Atmosphere Sharpness", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Overlay"  "Queue" = "Overlay+0" "IsEmissive" = "true"  }
		Cull Front
		ZWrite On
		Blend One One
		BlendOp Add
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha noshadow noambient nofog vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform float4 _Atmospherecolor;
		uniform float _AtmosphereSharpness;
		uniform float _Atmospherecircle;
		uniform float _Atmospheresize;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ase_vertexNormal * _Atmospheresize );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			float dotResult16 = 0.9f;//dot( ase_worldlightDir , i.worldNormal );
			float dotResult26 = dot( i.worldNormal , i.viewDir );
			float temp_output_32_0 = pow( (0.0 + (-dotResult26 - 0.0) * (( 15.0 * _AtmosphereSharpness ) - 0.0) / (1.0 - 0.0)) , ( 15.0 * _AtmosphereSharpness ) );
			float lerpResult41 = lerp( ( dotResult16 * temp_output_32_0 ) , temp_output_32_0 , _Atmospherecircle);
			float clampResult43 = clamp( lerpResult41 , 0.0 , 1.0 );
			o.Emission = ( _Atmospherecolor * clampResult43 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	
}
