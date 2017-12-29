Shader "MyShader/OutLineShader"
{
	Properties
	{
		_MainTex ("MainTexture", 2D) = "white" {}
		_OutLineColor("OutLineColor",COLOR)=(1,1,1,1)
		_BackColor("BackColor",COLOR)=(1,1,1,1)
		_EdgeFactor("EdgeFactor",Range(-5,20))=3
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct a2v
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv[9] : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			fixed4 _OutLineColor;
			fixed4 _BackColor;
			float _EdgeFactor;
			v2f vert (a2v v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv[0]=v.uv+_MainTex_TexelSize.xy*fixed2(-1,1);
				o.uv[1]=v.uv+_MainTex_TexelSize.xy*fixed2(0,1);
				o.uv[2]=v.uv+_MainTex_TexelSize.xy*fixed2(1,1);
				o.uv[3]=v.uv+_MainTex_TexelSize.xy*fixed2(-1,0);
				o.uv[4]=v.uv+_MainTex_TexelSize.xy*fixed2(0,0);
				o.uv[5]=v.uv+_MainTex_TexelSize.xy*fixed2(1,0);
				o.uv[6]=v.uv+_MainTex_TexelSize.xy*fixed2(-1,-1);
				o.uv[7]=v.uv+_MainTex_TexelSize.xy*fixed2(0,-1);
				o.uv[8]=v.uv+_MainTex_TexelSize.xy*fixed2(1,-1);
				return o;
			}
			float sobel(v2f v)
			{
				const half GX[9]={-1,-2,-1,
								0,0,0,
								1,2,1
								};
				const half GY[9]={
									-1,0,1,
									-2,0,2,
									-1,0,1
								};
				half partColor;
				half edgeX=0;
				half edgeY=0;
				for(int i=0;i<9;i++)
				{
					partColor=Luminance(tex2D(_MainTex,v.uv[i]));
					edgeX+=partColor*GX[i];
					edgeX+=partColor*GY[i];
				}
				return 1-abs(edgeX)-abs(edgeY);
			}
			fixed Luminance(fixed4 col)
			{
				return col.r*0.46+col.g*0.54+col.b*0.25;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				float  edge=sobel(i);
				fixed4 withEdgeColor=lerp(_OutLineColor,tex2D(_MainTex,i.uv[0]),edge);
				fixed4 onlyEdgeColor=lerp(_OutLineColor,_BackColor,edge);
				fixed4 col=lerp(withEdgeColor,onlyEdgeColor,_EdgeFactor);
				return col;
			}
			ENDCG
		}
	}
}
