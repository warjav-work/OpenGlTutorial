﻿using GLFW;
using OpenGLTutorial.GameLoop;
using OpenGLTutorial.Rendering.Cameras;
using OpenGLTutorial.Rendering.Display;
using OpenGLTutorial.Rendering.Shaders;
using System;
using System.Numerics;
using static OpenGLTutorial.OpenGL.GL;

namespace OpenGLTutorial
{
    class TestGame : Game
    {
        uint vao;
        uint vbo;
        Shader shader;
        Camera2D cam;

        public TestGame(int initialWindowWidth, int initialWindowHeigth, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeigth, initialWindowTitle)
        {

        }
        protected override void Initalize()
        {

        }

        protected unsafe override void LoadContent()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;

                                    uniform mat4 projection;
                                    uniform mat4 model;
    
                                    void main() 
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";

            string fragmentShader = @"#version 330 core
                                    out vec4 FragColor;
                                    in vec4 vertexColor;

                                    void main() 
                                    {
                                        FragColor = vertexColor;
                                    }";


            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();

            // Create VAO and VBO
            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);

            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] vertices =
            {
                -0.5f, 0.5f, 1f, 0f, 0f, // top left -> vec2(-0.5,0.5) aPosition vec3(1,0,0) aColor
                0.5f, 0.5f, 0f, 1f, 0f,// top right -> vec2(0.5,0.5) aPosition vec3(0,1,0) aColor
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left -> vec2(-0.5,-0.5) aPosition vec3(0,0,1) aColor

                0.5f, 0.5f, 0f, 1f, 0f,// top right
                0.5f, -0.5f, 0f, 1f, 1f, // bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left
            };

            // Fill VBO with data
            fixed (float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }
            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);
            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            cam = new Camera2D(DisplayManager.WindowSize / 2f, 2.5f);

        }
        protected override void Update()
        {

        }

        protected override void Render()
        {
            glClearColor(0, 0, 0, 0);
            glClear(GL_COLOR_BUFFER_BIT);

            Vector2 position = new Vector2(400, 300);
            Vector2 scale = new Vector2(150, 100);
            //float rotation = MathF.PI / 4f;
            float rotation = MathF.Sin(GameTime.TotalElapsedSeconds) * MathF.PI * 2;

            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);

            shader.SetMatrix4x4("model", sca * rot * trans);

            shader.Use();
            shader.SetMatrix4x4("projection", cam.GetProjectionMatrix());

            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);

            Glfw.SwapBuffers(DisplayManager.Window);
        }

    }
}
