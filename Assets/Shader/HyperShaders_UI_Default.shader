Shader "HyperShaders/UI/Default" {
	Properties {
		_Perspective ("Perspective", Range(0, 1)) = 0
		_PerspectiveNearPlane ("Perspective Near Plane", Float) = 0
		_AnchorOffset ("Anchor Offset", Vector) = (0,0,0,0)
		_CustomLightDir ("Custom Light Dir", Vector) = (0,0,1,0)
		_CustomLightColor ("Custom Light Color", Vector) = (1,1,1,1)
		[Space] _MainColor ("Main Color", Vector) = (1,1,1,1)
		_ShadowColor ("Shadow Color", Vector) = (0.5,0.5,0.5,1)
		[Space] [Toggle(SF_MAIN_TEXTURE)] _SF_MAIN_TEXTURE ("Main texture", Float) = 0
		_MainTex ("Main Texture", 2D) = "white" {}
		[Space] [Toggle(SF_DIFFUSE)] _SF_DIFFUSE ("Diffuse", Float) = 0
		[Toggle(SF_DIFFUSE_RAMP)] _SF_DIFFUSE_RAMP ("Diffuse ramp", Float) = 0
		_MainColorRampTex ("Diffuse Ramp Map", 2D) = "white" {}
		[Space] [Toggle(SF_RIM_LIGHT)] _SF_RIM_LIGHT ("Rim Light", Float) = 0
		_RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimLightPower ("Rim Light Power", Float) = 1
		[Space] [Toggle(SF_SPECULAR)] _SF_SPECULAR ("Specular", Float) = 0
		_SpecTex ("Specular Texture", 2D) = "white" {}
		_SpecShininess ("Specular Shininess", Range(0, 1)) = 0.5
		_SpecIntensity ("Specular Intensity", Vector) = (1,1,1,1)
		[Space] [Toggle(SF_REFLECTION)] _SF_REFLECTION ("Reflection", Float) = 0
		_ReflectionTex ("Reflection Texture", 2D) = "white" {}
		_ReflectionCube ("Reflection Cube", Cube) = "" {}
		[Space] [Toggle(SF_EMISSION)] _SF_EMISSION ("Emission", Float) = 0
		_EmissionMap ("Emission Map", 2D) = "black" {}
		_EmissionIntensity ("Emission Intensity", Range(0, 1)) = 1
		[Space] [Toggle(SF_COLOR_LERP)] _SF_COLOR_LERP ("Color Lerp", Float) = 0
		_ColorToLerp ("Color To Lerp", Vector) = (1,1,1,1)
		_LerpValue ("Lerp Value", Range(0, 1)) = 0
		[Space] [Toggle(SF_COLOR_FILL)] _SF_COLOR_FILL ("Color Z Fill", Float) = 0
		[Toggle(SF_SCREEN_SPACE_FILL)] _SF_SCREEN_SPACE_FILL ("Screen Space Fill", Float) = 0
		_FillLerpStart ("Fill Lerp Start", Float) = 0
		_FillLerpEnd ("Fill Lerp End", Float) = 0
		_PlaneRotationX ("X Plane Rotation", Float) = 0
		[Toggle(InverseFill)] _InverseFill ("InverseFill", Float) = 0
		_InactiveFillColor ("Inactive Color", Vector) = (0.7,0.7,0.7,1)
		_FillProgress ("Fill Progress", Range(0, 1)) = 0
		[Space] _Stencil ("Stencil ID", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Float) = 8
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Float) = 0
		_ColorMask ("Color Mask", Float) = 15
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}