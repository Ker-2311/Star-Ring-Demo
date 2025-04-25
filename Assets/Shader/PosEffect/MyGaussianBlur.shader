// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/MyGaussianBlur"
{
    Properties 
    {  
        _MainTex ("Base (RGB)", 2D) = "white" {}  
        _BlurSize ("Blur Size", Float) = 1.0  
    }  
    SubShader 
    {  
        //在SubShader 块中利用CGINCLUDE 和 ENDCG 来定义一系列代码  
        //这些代码不需要包含在Pass语义块中，在使用时，我们只需要在Pass中指定需要  
        //使用的顶点着色器和片元着色器函数名即可。  
        //使用CGINCLUDE 来管理代码 可以避免我们编写两个完全一样的frag函数  
        //这里相当于只是定义 执行还是在下边的Pass中
        CGINCLUDE  
        #include "UnityCG.cginc"  

        sampler2D _MainTex;    
        half4 _MainTex_TexelSize;  
        float _BlurSize;  

        struct v2f
        {  
            float4 pos : SV_POSITION;  
            half2 uv[5]: TEXCOORD0;  
        };  

		v2f vertBlurVertical(appdata_img v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			
			half2 uv = v.texcoord;
			
			o.uv[0] = uv;
			o.uv[1] = uv + float2(0.0, _MainTex_TexelSize.y * 1.0) * _BlurSize;
			o.uv[2] = uv - float2(0.0, _MainTex_TexelSize.y * 1.0) * _BlurSize;
			o.uv[3] = uv + float2(0.0, _MainTex_TexelSize.y * 2.0) * _BlurSize;
			o.uv[4] = uv - float2(0.0, _MainTex_TexelSize.y * 2.0) * _BlurSize;
					 
			return o;
		}
		
		v2f vertBlurHorizontal(appdata_img v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			
			half2 uv = v.texcoord;
			
			o.uv[0] = uv;
			o.uv[1] = uv + float2(_MainTex_TexelSize.x * 1.0, 0.0) * _BlurSize;
			o.uv[2] = uv - float2(_MainTex_TexelSize.x * 1.0, 0.0) * _BlurSize;
			o.uv[3] = uv + float2(_MainTex_TexelSize.x * 2.0, 0.0) * _BlurSize;
			o.uv[4] = uv - float2(_MainTex_TexelSize.x * 2.0, 0.0) * _BlurSize;
					 
			return o;
		}


        fixed4 fragBlur(v2f i): SV_TARGET
        {
            float weight[3] = {0.4026, 0.2442, 0.0545};

            fixed3 sum = tex2D(_MainTex,i.uv[0]).rgb * weight[0];
            for (int it = 1;it<3;it++)
            {
                sum += tex2D(_MainTex,i.uv[it]).rgb * weight[it];
                sum += tex2D(_MainTex,i.uv[2*it]).rgb * weight[it];
            }
            return fixed4(sum,1.0);
        }
        ENDCG  

        ZTest Always Cull Off ZWrite Off  

        Pass 
        {  
            NAME "GAUSSIAN_BLUR_VERTICAL"  

            CGPROGRAM  

            #pragma vertex vertBlurVertical    
            #pragma fragment fragBlur  

            ENDCG    
        }  

        Pass 
        {    
            NAME "GAUSSIAN_BLUR_HORIZONTAL"  

            CGPROGRAM    

            #pragma vertex vertBlurHorizontal    
            #pragma fragment fragBlur  

            ENDCG  
        }  
    }   
    FallBack "Diffuse"  
}
