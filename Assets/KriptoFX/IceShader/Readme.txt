This project contains two ice shader .
Shader for the foreground and background . Such a division is related to problems of mixing transparent objects.
Walls / floor / and other static objects, use the shader "IceBack", for everything else use "IceFront".

Shader settings :
Main Color - color coating of ice.
Specular Color - color highlight.
Shininess - radius of the flare.
Reflection Color - color radiance.
Refletstion Strength - the intensity of the reflection.
Base (RGB) Emission Tex (A) - shine texture with alpha channel .
Material opatsity - transparency of ice.
Reflection Cubemap - shine reflection CubeMap.
Normalmap - normal map and height ice .
FPOW Fresnel - glow thickness relative to the angle .
R0 Fresnel - viewing angle relative CubeMap.
Cutoff - force freezing surface , Range 0 to 1.
Light strength - the luminescence intensity.