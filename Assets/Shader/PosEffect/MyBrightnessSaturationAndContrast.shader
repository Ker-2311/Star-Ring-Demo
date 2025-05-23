Shader "Unlit/MyBrightnessSaturationAndContrast"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}  
        _Brightness ("Brightness", Float) = 1  
        _Saturation("Saturation", Float) = 1  
        _Contrast("Contrast", Float) = 1  
    }
    SubShader
    {

        Pass
        {
            ZTest Always Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            sampler2D _MainTex;    
            half _Brightness;  
            half _Saturation;  
            half _Contrast;  

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 renderTex = tex2D(_MainTex, i.uv);
                
                //亮度
                fixed3 finalColor = renderTex.rgb * _Brightness;

                //饱和度
                fixed luminance = 0.2125 * renderTex.r + 0.7154 * renderTex.g + 0.0721 * renderTex.b;
                fixed3 luminanceColor = fixed3(luminance,luminance,luminance);
                finalColor = lerp(luminanceColor,finalColor,_Saturation);

                //对比度
                fixed3 avgColor = fixed3(0.5,0.5,0.5);
                finalColor = lerp(avgColor,finalColor,_Contrast);

                return fixed4(finalColor,renderTex.a);
            }
            ENDCG
        }
    }
}
