sampler uImage : register(s0);
float fade;

float4 FilterMyShader(float2 coords : TEXCOORD0) : COLOR0
{
    float4 current = tex2D(uImage, coords);
    float brightness = (current.r + current.g + current.b) / 3;
    float3 sum = current.rgb;
    return float4(
        (float3(brightness, brightness, brightness) * (1 - fade)) + (sum * fade),
        current.w
    );
}

technique Technique1
{
    pass FilterMyShader
    {
        PixelShader = compile ps_3_0 FilterMyShader();
    }
}