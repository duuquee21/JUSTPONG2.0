Shader "LowPolyShaders/LowPolyPBRShader"
{
    Properties
    {
        _MainTex ("Color Scheme", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="ForwardBase" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" // Incluye la cabecera core.hlsl

            // Estructura de entrada
            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Propiedades del shader
            sampler2D _MainTex;
            float4 _Color;
            half _Glossiness;
            half _Metallic;

            // Shader de vértices
            Varyings vert(Attributes v)
            {
                Varyings o;
                o.uv = v.uv;

                // La posición de los vértices debe ser transformada a clip space
                float4 transformedPosition = UnityObjectToClipPos(v.vertex);  // Asegúrate de usar la función adecuada
                o.vertex = transformedPosition;

                return o;
            }

            // Shader de fragmentos
            half4 frag(Varyings i) : SV_Target
            {
                // Muestra la textura
                half4 texColor = tex2D(_MainTex, i.uv);
                // Aplica el tinte
                half4 color = texColor * _Color;

                // Devuelve el color final con el metal y suavidad
                return color;
            }
            ENDHLSL
        }
    }
    Fallback "UniversalForward"
}
