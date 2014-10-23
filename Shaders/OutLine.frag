#version 330

uniform vec4 color;
uniform uint lineStippleFactor;
uniform uint lineStipplePattern;

flat in vec2 startPos2d; /* not interpolated along the line, remains the same along the line */
noperspective in vec2 pos2d; /* not perspectively correct interpolated along the line */

out vec4 outputColor;

void main()
{
	if(lineStippleFactor==uint(0)) /* line stippling off? */
	{
		outputColor=color;
		return;
	}

	int bit=int(floor(distance(pos2d, startPos2d)/lineStippleFactor+0.5))%32;
	if((lineStipplePattern&uint(1<<bit))==uint(0)) discard;

	outputColor=color;
}
