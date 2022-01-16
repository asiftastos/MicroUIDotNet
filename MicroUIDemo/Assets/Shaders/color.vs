﻿#version 450 core

layout (location = 0) in vec2 aPos;

uniform mat4 ortho;

void main()
{
	gl_Position = ortho * vec4(aPos, -1.0f, 1.0f);
}