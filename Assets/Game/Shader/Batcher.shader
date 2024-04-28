Shader "MyShader/Batcher" {
  //show values to edit in inspector
  Properties{
    //[PerRendererData]
    _Color ("Color", Color) = (0, 0, 0, 1)
  }

  SubShader{
    //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
    Tags{ "RenderType"="Opaque" "Queue"="Geometry"}
    Pass{
      Tags {"LightMode"="ForwardBase"}
      CGPROGRAM
      //allow instancing
      #pragma multi_compile_instancing
      //shader functions
      #pragma vertex vert
      #pragma fragment frag

      //use unity shader library
      #include "UnityCG.cginc"
      #include "UnityLightingCommon.cginc"
      
      //per vertex data that comes from the model/parameters
      struct appdata{
        float4 vertex : POSITION;
        UNITY_VERTEX_INPUT_INSTANCE_ID
      };

      //per vertex data that gets passed from the vertex to the fragment function
      struct v2f{
        float4 position : SV_POSITION;
        fixed4 diff : COLOR0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
      };

      UNITY_INSTANCING_BUFFER_START(Props)
      UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
      UNITY_INSTANCING_BUFFER_END(Props)

      v2f vert(appdata_base v){
        v2f o;
        //setup instance id
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_TRANSFER_INSTANCE_ID(v, o);

        half3 worldNormal = UnityObjectToWorldNormal(v.normal);
        half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
        
        //calculate the position in clip space to render the object
        o.position = UnityObjectToClipPos(v.vertex);
        o.diff = (nl * _LightColor0);
        o.diff.rgb += ShadeSH9(half4(worldNormal,1));
        return o;
      }

      fixed4 frag(v2f i) : SV_TARGET{
          //setup instance id
          UNITY_SETUP_INSTANCE_ID(i);
          //get _Color Property from buffer
          fixed4 color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
          color *= i.diff;
        //Return the color the Object is rendered in
        return color;
      }

      ENDCG
    }
    Pass 
		{
			Name "CastShadow"
			Tags { "LightMode" = "ShadowCaster" }
	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"
	
			struct v2f 
			{ 
				V2F_SHADOW_CASTER;
			};
	
			v2f vert( appdata_base v )
			{
				v2f o;
				TRANSFER_SHADOW_CASTER(o)
				return o;
			}
	
			float4 frag( v2f i ) : COLOR
			{
				SHADOW_CASTER_FRAGMENT(i*5)
			}
			ENDCG
		}
  }
}