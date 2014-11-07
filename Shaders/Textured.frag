#version 330

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D sampler;
uniform vec4 light;

void main()
{
	outputColor = texture(sampler, texCoord)*light;
}
