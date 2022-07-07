// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Vert Doodle"
{
    Properties
    {
       [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _NoiseScale ("Noise Scale", Range(0.0, 0.08)) = 0.01
        _NoiseSnap ("Noise Snap", Range(0.001, 0.01)) = 0.005
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        CGPROGRAM
        
        #pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
        #pragma multi_compile_local _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #include "UnitySprites.cginc"

        struct Input
        {
            float2 uv_MainTex;
            fixed4 color;
        };


        float _NoiseScale;
        float _NoiseSnap;

        inline float snap(float x, float snap) {
            return snap * round(x / snap);
        }

        float3 random3(float3 c) {
	        float j = 4096.0*sin(dot(c,float3(17.0, 59.4, 15.0)));
	        float3 r;
	        r.z = frac(512.0*j);
	        j *= .125;
	        r.x = frac(512.0*j);
	        j *= .125;
	        r.y = frac(512.0*j);
	        return r-0.5;
        }

        void vert (inout appdata_full v, out Input o)
        {
            float _NoiseScale = 0.015;
            float _NoiseSnap = 0.2;
            float time = snap(_Time.y, _NoiseSnap);
            float2 noise = random3(v.vertex.xyz + float3(time, 0.0, 0.0)).xy * _NoiseScale;

            v.vertex.xy += noise;

            #if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif

            
            UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color;// *_Color * _RendererColor;

        }

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = SampleSpriteTexture (IN.uv_MainTex) * IN.color;

            o.Albedo = c.rgb * c.a * 5;
            o.Alpha = c.a;
        }
       
            
        ENDCG
    
}
Fallback "Transparent/VertexLit"
}
