Shader "Graph/Point Surface"
{

    Properties
    {
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
    }

    SubShader
    {

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        // ConfigureSurface refers to a method used to configure the shader
        #pragma surface ConfigureSurface Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        // Color points based on their world position
        struct Input
        {
            float3 worldPos;
        };


        float _Smoothness;

        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface)
        {
            // Base the color of the cube on the world position
            // Halve the position and then add ½ to fit the graph's -1 to 1 domain
            // Ignore the z coordinate blue channel
            // clamp all components to 0 – 1 using saturate
            surface.Albedo.rg = saturate(input.worldPos.xy * 0.5 + 0.5);

            // Smoothness comes from a slider variable
            surface.Smoothness = _Smoothness;
        }

        ENDCG
    }

    FallBack "Diffuse"
}
