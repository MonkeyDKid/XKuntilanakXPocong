﻿Shader "Tesca/Custom/Mirror" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_ReflectionTex ("Reflection", 2D) = "white" { TexGen ObjectLinear }
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {
        SetTexture[_MainTex] { combine texture }
        SetTexture[_ReflectionTex] { matrix [_ProjMatrix] combine texture * previous }
    }
}

Subshader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
    Pass {
        SetTexture [_MainTex] { combine texture }
    }
}
}