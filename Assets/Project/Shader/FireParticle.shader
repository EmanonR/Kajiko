Shader "Custom/FireParticleURP"
{
    Properties
    {
        _MainTex ("Fire Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (1,0.5,0.1,1)
        _EmissionIntensity ("Emission Intensity", Float) = 3.0
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend", Float) = 1 // One (additive)
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend", Float) = 1 // One (additive)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 100

        Pass
        {
            Blend [_SrcBlend] [_DstBlend]
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color      : COLOR;
                // Matches Custom Vertex Streams list:
                // UV (TEXCOORD0.xy) | UV2 (TEXCOORD0.zw)
                // AnimBlend (TEXCOORD1.x) | Custom1.x (TEXCOORD1.y)
                float4 texcoord0  : TEXCOORD0;
                float4 texcoord1  : TEXCOORD1;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 color       : COLOR;
                float2 uv          : TEXCOORD0;
                float  emissionFade : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            float4 _Color;
            float4 _EmissionColor;
            float _EmissionIntensity;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.texcoord0.xy, _MainTex); // UV
                OUT.color = IN.color;
                OUT.emissionFade = IN.texcoord1.y; // Custom1.x
                return OUT;
            }

            float4 frag (Varyings IN) : SV_Target
            {
                float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);

                // Alpha / shape: driven by texture alpha * vertex color alpha
                // (i.e. your normal Color over Lifetime module). Keep this
                // solid or slow-fading -- this is what stops the "pop to
                // transparent" look.
                float alpha = tex.a * IN.color.a;

                // Base visible color (only matters if using alpha blend
                // instead of additive; with additive this just adds tint)
                float3 baseColor = tex.rgb * _Color.rgb * IN.color.rgb;

                // Emission: fades to black independently via Custom1.x,
                // completely decoupled from alpha above.
                float3 emission = _EmissionColor.rgb * _EmissionIntensity
                                   * IN.emissionFade * tex.rgb;

                float3 finalColor = baseColor + emission;

                return float4(finalColor, alpha);
            }
            ENDHLSL
        }
    }
}
