#version 330

uniform mat4 projectionMatrix;
uniform mat4 modelViewMatrix;
uniform vec2 viewPortSize;

in vec3 inPosition;

flat out vec2 startPos2d; /* not interpolated along the line, remains the same along the line */
noperspective out vec2 pos2d; /* not perspectively correct interpolated along the line */

void main()
{
	gl_Position=projectionMatrix*modelViewMatrix*vec4(inPosition, 1.0);

	startPos2d=pos2d=(gl_Position/gl_Position.w).xy*(viewPortSize/2); /* nearly window space coordinates, but never mind the offset and axis direction */
}
