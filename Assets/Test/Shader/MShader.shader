Shader "Unlit/MShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", color) = (1,1,1,1)
		_ValueOne( "Value1" , float) = 1
		_ValueTwo( "Value2" , float) = 1
		_ValueThree( "Value3" , float) = 1
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
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;

			float _ValueOne;
			float _ValueTwo;
			float _ValueThree;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) ;
				fixed4 white = fixed4(1,1,1,1);

				float v1 = max( 0 , 1 - abs(_ValueOne - 0.5) * 3);
				float v2 = max( 0 , 1 - abs(_ValueTwo - 0.5) * 3);
				float v3 = max( 0 , 1 - abs(_ValueThree - 0.8) * 3);

				col.r = lerp( 1 , col.r , v1 * v2 * v2 );
				col.g = lerp( 1 , col.r , v1 * v3 * v3 );
				col.b = lerp( 1 , col.r , v3 * v2 * v1);


				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col * _Color;
			}
			ENDCG
		}
	}
}
