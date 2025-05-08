Shader "DuqueShader/BaseColor"
{
    Properties
    {
        _BaseColor ("Base Color" , Color) = (1,1,1,1)
    }
    SubShader
    {
      Tags
      {
                     "RenderType"="Opaque"
                     "Queue"="Geometry"
                     "RenderPipeline"="UniversalPipeline"
      }
        Pass
        {
            Tags
            {
             "LightMode" = "UniversalForward"  
            }
          
            HLSLPROGRAM

            #pragma vertex vert //parra decirle a unity como vamos a definir
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {

                float4 positionOS : POSITION; // posiciones de los vertices 
            };

            struct v2f //VERTEX TO FRAGMENT 
            {
                float4 positionCS: SV_POSITION;
            };

            float4 _BaseColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.positionCS =TransformObjectToHClip(v.positionOS);
                return o;
                }
            float4 frag ( v2f i) : SV_Target
            {
                return _BaseColor;
                }

            ENDHLSL
        }
    }
    FallBack "Unlit/Color" //esto es un colchon por si no funciona ningun subshader
}
