﻿using OpenGLTutorial.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTutorial.Rendering.Cameras
{
    class Camera2D
    {
        public Vector2 FocusPosition { get; set; }
        public float Zoom { get; set; }

        public Camera2D(Vector2 focusPosition, float zoom)
        {
            this.FocusPosition = focusPosition;
            this.Zoom = zoom;
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            float left = FocusPosition.X - DisplayManager.WindowSize.X / 2f;
            float rigth = FocusPosition.X + DisplayManager.WindowSize.X / 2f;
            float top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2f;
            float bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2f;

            Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(left, rigth, bottom, top, 0.01f, 100f);
            Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(Zoom);

            return orthoMatrix * zoomMatrix;
        }


    }
}
