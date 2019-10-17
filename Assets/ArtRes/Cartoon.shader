// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "taecg/Cartoon"
{
	Properties
	{
		[Header(Base)]_Color("Color(RGB)", Color) = (1,1,1,1)
		_MainTex("MainTex(RGB=Diffuse,A=Mask)", 2D) = "white" {}
		_MaskClip("MaskClip", Range( 0 , 1)) = 0
		[Header(Outline)]_OutlineColor("Outline Color", Color) = (0,0,0,0)
		_OutlineWidth("Outline Width", Range( 0 , 0.05)) = 0.005
		[Header(Ramp Toon)]_RampTex("RampTex", 2D) = "white" {}
		[Header(Specular)]_SpecularColor("Specular Color", Color) = (1,1,1,0)
		_Gloss("Gloss", Range( 0.01 , 1)) = 0.1
		[Header(Fresnel)]_FresnelColor("Fresnel Color", Color) = (0,0,0,0)
		_FresnelScale("Fresnel Scale", Range( 0 , 10)) = 1
		_FresnelPower("Fresnel Power", Range( 0 , 5)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float outlineVar = _OutlineWidth;
			v.vertex.xyz += ( v.normal * outlineVar );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _OutlineColor.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 2.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
		};

		uniform float _MaskClip;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _RampTex;
		uniform float4 _SpecularColor;
		uniform float _Gloss;
		uniform float4 _FresnelColor;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform float _OutlineWidth;
		uniform half4 _OutlineColor;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += 0;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult5_g11 = dot( ase_worldNormal , ase_worldlightDir );
			float2 appendResult84 = (float2((dotResult5_g11*0.5 + 0.5) , 0.0));
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 normalizeResult4_g10 = normalize( ( ase_worldViewDir + ase_worldlightDir ) );
			float dotResult101 = dot( normalizeResult4_g10 , normalize( WorldNormalVector( i , float3(0,0,1) ) ) );
			float fresnelNdotV113 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode113 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV113, _FresnelPower ) );
			o.Emission = ( ( _Color * tex2DNode1 * tex2D( _RampTex, appendResult84 ) ) + ( _SpecularColor * ( round( pow( max( dotResult101 , 0.0 ) , ( _Gloss * 128.0 ) ) ) * 0.6 ) ) + ( _FresnelColor * fresnelNode113 ) ).rgb;
			o.Alpha = 1;
			clip( tex2DNode1.a - _MaskClip );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
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
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
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
/*ASEBEGIN
Version=15401
415;322;1847;954;-407.9684;515.8686;1.070001;True;False
Node;AmplifyShaderEditor.CommentaryNode;112;-1145.969,-178.412;Float;False;1803.549;545.1788;Specular;12;98;97;100;105;101;104;102;103;107;109;111;110;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;98;-1095.97,182.7668;Float;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;100;-879.726,186.8492;Float;False;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;97;-938.8233,90.8095;Float;False;Blinn-Phong Half Vector;-1;;10;91a149ac9d615be429126c95e20753ce;0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;101;-618.8231,138.8097;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;105;-458.8228,154.8096;Float;False;Property;_Gloss;Gloss;8;0;Create;True;0;0;False;0;0.1;0;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;102;-442.8228,58.80939;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-154.8226,122.8096;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;128;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;55;-297.2549,-848.1489;Float;False;950.8058;633.0221;Diffuse;6;1;82;84;87;91;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;118;-146.31,443.9197;Float;False;813.482;470.9289;Fresnel;5;115;113;116;114;117;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PowerNode;103;5.177294,58.80939;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;87;-277.3922,-406.177;Float;False;Half Lambert Term;-1;;11;86299dc21373a954aa5772333626c9c1;0;1;3;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;84;-42.67673,-411.3414;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RoundOpNode;107;165.3347,68.90987;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-96.31002,799.8486;Float;False;Property;_FresnelPower;Fresnel Power;11;0;Create;True;0;0;False;0;1;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-91.21811,713.7669;Float;False;Property;_FresnelScale;Fresnel Scale;10;0;Create;True;0;0;False;0;1;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;111;233.3774,-128.4122;Float;False;Property;_SpecularColor;Specular Color;7;0;Create;True;0;0;False;1;Header(Specular);1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;27;-11.14664,1129.16;Float;False;654.6169;361.9737;外描边;3;35;28;30;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;91;131.6996,-799.5629;Float;False;Property;_Color;Color(RGB);0;0;Create;False;0;0;False;1;Header(Base);1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;289.1722,72.99401;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;113;213.0827,688.4717;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;84.07898,-621.6092;Float;True;Property;_MainTex;MainTex(RGB=Diffuse,A=Mask);1;0;Create;False;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;82;93.06525,-424.1567;Float;True;Property;_RampTex;RampTex;6;0;Create;True;0;0;False;1;Header(Ramp Toon);c1e7451f137d0463a93dcfbd91978df4;c1e7451f137d0463a93dcfbd91978df4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;116;232.0707,493.9197;Float;False;Property;_FresnelColor;Fresnel Color;9;0;Create;True;0;0;False;1;Header(Fresnel);0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;30;46.55325,1371.425;Float;False;Property;_OutlineWidth;Outline Width;5;0;Create;True;0;0;False;0;0.005;0;0;0.05;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;500.172,587.9099;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;88;2041.888,-847.2679;Float;False;957.6924;336.2692;程序明暗阴影;7;12;69;67;18;10;11;9;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;485.1661,-669.9383;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;86;2031.973,-442.2509;Float;False;556.8113;363.4425;Properities;1;89;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;490.578,-27.70917;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;28;88.36655,1190.202;Half;False;Property;_OutlineColor;Outline Color;4;0;Create;True;0;0;False;1;Header(Outline);0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;2396.484,-770.8969;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;2085.162,-277.4201;Float;False;Property;_MaskClip;MaskClip;2;0;Create;True;0;0;True;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OutlineNode;35;422.4646,1242.221;Float;False;0;False;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FloorOpNode;9;2537.673,-764.0429;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;67;2845.582,-689.2617;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;12;2687.175,-731.487;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;108;922.1782,-272.4952;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;69;2681.099,-625.9991;Float;False;Constant;_Float2;Float 2;8;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;2091.887,-709.6076;Float;False;Property;_Steps;Steps;3;1;[IntRange];Create;True;0;0;False;0;3;0;2;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;18;2120.063,-797.2678;Float;False;Half Lambert Term;-1;;12;86299dc21373a954aa5772333626c9c1;0;1;3;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1592.121,-314.6141;Float;False;True;0;Float;;0;0;Unlit;taecg/Cartoon;False;False;False;False;False;True;True;True;True;False;True;True;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0.01;0,0,0,0;VertexScale;False;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;85;-1;0;True;89;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;100;0;98;0
WireConnection;101;0;97;0
WireConnection;101;1;100;0
WireConnection;102;0;101;0
WireConnection;104;0;105;0
WireConnection;103;0;102;0
WireConnection;103;1;104;0
WireConnection;84;0;87;0
WireConnection;107;0;103;0
WireConnection;109;0;107;0
WireConnection;113;2;114;0
WireConnection;113;3;115;0
WireConnection;82;1;84;0
WireConnection;117;0;116;0
WireConnection;117;1;113;0
WireConnection;19;0;91;0
WireConnection;19;1;1;0
WireConnection;19;2;82;0
WireConnection;110;0;111;0
WireConnection;110;1;109;0
WireConnection;11;0;18;0
WireConnection;11;1;10;0
WireConnection;35;0;28;0
WireConnection;35;1;30;0
WireConnection;9;0;11;0
WireConnection;67;0;12;0
WireConnection;67;1;69;0
WireConnection;12;0;9;0
WireConnection;12;1;10;0
WireConnection;108;0;19;0
WireConnection;108;1;110;0
WireConnection;108;2;117;0
WireConnection;0;2;108;0
WireConnection;0;10;1;4
WireConnection;0;11;35;0
ASEEND*/
//CHKSM=E606006890F7AB8F8630A13F3CEEC7D39B1F76C8