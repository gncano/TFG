Shader "Custom/VertexColorUnlit"
{
    Properties
    {
        _Brightness ("Brightness", Range(0, 3)) = 1.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Lighting Off
            Cull Back
            ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            float _Brightness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Brightness;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return saturate(i.color);
            }
            ENDCG
        }
    }
}