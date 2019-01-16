Shader "Custom/SpriteDiffuse-Z"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        _AlphaCut("AlphaCut",Range(0,1))=1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Geometry"
            //"IgnoreProjector"="True"
            "RenderType"="TransparentCutOut"
            //"PreviewType"="Plane"
            //"CanUseSpriteAtlas"="True"
        }
        LOD 200
        
        //Cull Off
        //Lighting Off
        ZWrite On // 打开深度检测
        //Blend One OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addShadow alphatest:_AlphaCut
        #pragma multi_compile _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #include "UnitySprites.cginc"

        struct Input
        {
            float2 uv_MainTex;
            fixed4 color;
        };

        void vert (inout appdata_full v, out Input o)
        {
            v.vertex = UnityFlipSprite(v.vertex, _Flip);

            #if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = v.color * _Color * _RendererColor;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = SampleSpriteTexture (IN.uv_MainTex) * IN.color;
            o.Albedo = c.rgb * c.a;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
