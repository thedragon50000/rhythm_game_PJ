﻿using NoteEditor.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace NoteEditor.GLDrawing
{
    public class GLQuadDrawer : SingletonMonoBehaviour<GLQuadDrawer>
    {
        [SerializeField]
        Material mat = default;
        List<Geometry> drawData = new List<Geometry>();

        int size = 0;
        int maxSize = 0;

        void OnRenderObject()
        {
            GL.Flush();
            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadPixelMatrix();
            GL.Begin(GL.QUADS);

            if (size * 2 < maxSize)
            {
                drawData.RemoveRange(size, maxSize - size);
                maxSize = size;
            }

            for (int i = 0; i < size; i++)
            {
                GL.Color(drawData[i].color);

                foreach (var vertex in drawData[i].vertices)
                {
                    GL.Vertex(vertex);
                }
            }

            GL.End();
            GL.PopMatrix();
            size = 0;
        }

        public static void Draw(Geometry[] quads)
        {
            foreach (var quad in quads)
            {
                Draw(quad);
            }
        }

        public static void Draw(Geometry quad)
        {
            
            if (Instance.size < Instance.maxSize)
            {
                Instance.drawData[Instance.size] = quad;
            }
            else
            {
                Instance.drawData.Add(quad);
                Instance.maxSize++;
            }

            Instance.size++;
        }
    }
}
