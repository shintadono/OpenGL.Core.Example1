#version 330

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2DRect sampler;
uniform vec4 textColor;

void main()
{
	outputColor = vec4(textColor.rgb, textColor.a*texture(sampler, texCoord).r);
}