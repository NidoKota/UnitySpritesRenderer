Shader "Unlit/SpritsRender"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

        Cull Back
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "Default"
            
            HLSLPROGRAM
            
            #define MAX_COUNT 64
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UVTransform.hlsl"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ClipRect;
            
            float4 _SpritePositions[MAX_COUNT];
            float _SpriteRotations[MAX_COUNT];
            float4 _SpriteScales[MAX_COUNT];
            float4 _SpriteTiles[MAX_COUNT];
            float4 _SpriteOffsets[MAX_COUNT];
            float _SpriteAlphas[MAX_COUNT];
            int _SpriteCount;

            v2f vert(appdata_t v)
            {
                v2f OUT;

                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                OUT.color = v.color;

                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                IN.texcoord -= 0.5f;
                
                half4 srcOneMinusSrcBlend = (half4)0;
                int isFrontAlphaNotZeroFirst = 1;
                
                for (int i = 0; i < _SpriteCount; i++)
                {
                    const float2 currentSpritePosition = _SpritePositions[i].xy * 0.5f;
                    const float currentSpriteRotation = radians(_SpriteRotations[i]);
                    const float2 currentSpriteScale = _SpriteScales[i].xy;
                    const float2 currentSpriteTile = _SpriteTiles[i].xy;
                    const float2 currentSpriteOffset = _SpriteOffsets[i].xy;
                    const float currentSpriteAlpha = _SpriteAlphas[i];
                    
                    const uvTransform tra = {currentSpritePosition, currentSpriteRotation, currentSpriteScale};
                    const float2 uv = getInTransformUV(tra, IN.texcoord) * currentSpriteTile + currentSpriteOffset;

                    const float4 sample = tex2D(_MainTex, uv) * float4(1, 1, 1, clamp(currentSpriteAlpha, 0, 1)) * isInTransform(tra, IN.texcoord);
                    
                    const float3 backColor = srcOneMinusSrcBlend.rgb;
                    const float3 frontColor = sample.rgb;
                    const float backAlpha = srcOneMinusSrcBlend.a;
                    const float frontAlpha = sample.a;
                    
                    const float4 blend = float4(frontColor * frontAlpha + backColor * (1.f - frontAlpha), frontAlpha + backAlpha * (1.f - frontAlpha));
                    
                    const int isFrontAlphaNotZero = frontAlpha != 0;
                    
                    srcOneMinusSrcBlend = sample * isFrontAlphaNotZeroFirst + blend * (1 - isFrontAlphaNotZeroFirst);
                    isFrontAlphaNotZeroFirst *= (1 - isFrontAlphaNotZero);
                }

                return srcOneMinusSrcBlend;
            }

            ENDHLSL
        }
    }
}