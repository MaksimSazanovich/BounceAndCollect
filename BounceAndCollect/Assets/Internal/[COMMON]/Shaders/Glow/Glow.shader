// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Glow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = ""
		_GlowColor ("Glow Color", Vector) = (0.2, 0.4, 0.7, 1)
	}

	SubShader
	{
    	Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
    	LOD 100
    
    	ZWrite Off
    	Blend SrcAlpha OneMinusSrcAlpha 
    
    	Pass
    	{  
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            // Юниформ-переменные.
            uniform fixed4 _GlowColor;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            //----------------------------------------Вершинный шейдер----------------------------------------//
            v2f vert(appdata_t v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                
                return o;
            }
            
            //---------------------------------------Пиксельный шейдер----------------------------------------//
            
            // Константы.
            static const float PI    = 3.141592;
            static const float PI_2  = 6.283185;
            static const float PI_16 = 50.26548;
			
            static const fixed MAX_RADIUS = 0.5;
            static const fixed MIN_RADIUS = 0.3;
            static const fixed MIDDLE_RADIUS = 0.4;
			
            // Количество периодов в окружности.
            static const int PERIOD_COUNT = 18;
				
            // Амплитуда радиальных колебаний.
            static const fixed RADIAL_AMPLITUDE = 0.01;
			
            // Преобразовать декартовы координаты пикселя в полярные — с центром в (0.5; 0.5).
            // В возвращаемой координате: x — расстояние, y — угол в диапазоне (-PI; PI).
            half2 getPolarCoords(half2 cartesianCoords)
            {
            	cartesianCoords -= 0.5;
            	half arctg = atan2(cartesianCoords.y, cartesianCoords.x);
            	
		return half2(sqrt(cartesianCoords.x * cartesianCoords.x + cartesianCoords.y * cartesianCoords.y), arctg);
            }
			
            fixed4 frag(v2f input) : SV_Target
            {
            	// Вычислить полярные координаты.
		half2 polarCoords = getPolarCoords(input.texcoord);
				
		// Модифицировать угол.
		float angle = PERIOD_COUNT * polarCoords.y;
		
		// Вычислить расстояние от центра до границы фигуры.
		half shapeRadius = MIDDLE_RADIUS + RADIAL_AMPLITUDE * (sin(angle / 2 + _Time * PI_16) + cos(angle));
				
		// Исключить все пиксели за пределами фигуры.
		if(polarCoords.x > shapeRadius) discard;
				
		// Вычислить интенсивность пикселя.
		fixed intensity = shapeRadius / MAX_RADIUS * sin(polarCoords.x / shapeRadius * PI);
				
                return fixed4(_GlowColor.rgb, intensity);
            }
        ENDCG
	}
    }
}