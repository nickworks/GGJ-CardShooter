Shader "Custom/AestheticBuzz"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HalfTone ("Half Tone", 2D) = "white" {}
        _Vin ("Vin-whatever", 2D) = "white" {}
		_ToneAggresion("Tone Aggression", Float) = 100
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            sampler2D _HalfTone;
            sampler2D _Vin;
			float _ToneAggresion;
			float _Delay;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 scrn = tex2D(_MainTex, i.uv);
				
				fixed4 hlfT = tex2D(_HalfTone, i.uv);
				fixed4 vint = tex2D(_Vin, i.uv);


				fixed4 colF;
				colF.rgb = scrn.rgb + (scrn.rgb * scrn.rgb * scrn.rgb);
				colF.a = 1;


				float cTot = (scrn.r + scrn.g + scrn.b) / 3;

				colF *= ((1 - hlfT.r) / _ToneAggresion > (cTot)) ? .9 : 1;

				fixed4 end = lerp(colF, scrn, (1 - vint.r) / _Delay);

                return end;
            }
            ENDCG
        }
    }
}
