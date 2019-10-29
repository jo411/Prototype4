Shader "Custom/ConveyorPanner_Working"
{
    Properties
    {
		_myMainTex ("Texture", 2D) = "White"{}
		_myEmission("Glow", Color) = (1,1,1,1)
		_myXSpeed("X Speed", float) = 0
		_myYSpeed("Y Speed", float) = 0
    }

	SubShader{
		
		CGPROGRAM
		#pragma surface Function Lambert

		struct Input {
			float2 uv_myMainTex;
		};

		sampler2D _myMainTex;
		fixed4 _myEmission;
		float _myXSpeed;
		float _myYSpeed;
		float _myXOffset;
		float _myYOffset;

		void Function(Input IN, inout SurfaceOutput o) {
		
			float2 uv_scroller = IN.uv_myMainTex;
			_myXOffset = _myXSpeed * _Time;
			_myYOffset = _myYSpeed * _Time;
			uv_scroller += float2(_myXOffset, _myYOffset);
			
			float4 _scrolledValue = tex2D(_myMainTex, uv_scroller);
			//o.Albedo = tex2D(_myMainTex, IN.uv_myMainTex).rgb;
			o.Albedo += _scrolledValue.rgb;
		}
		
		ENDCG
	}
    FallBack "Diffuse"
}
