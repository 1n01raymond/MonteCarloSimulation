using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    const float DEG2RAD = 3.14159f/180;
    
    void OnPostRender()
    {
        GL.PushMatrix();
        GL.LoadOrtho();

        // x line
        GL.Begin(GL.LINES);
        GL.Color(Color.black);
        GL.Vertex(new Vector3( 0, 0.5f, 0));
        GL.Vertex(new Vector3( 1, 0.5f, 0));
        GL.End();
        
        // y line
        GL.Begin(GL.LINES);
        GL.Color(Color.black);
        GL.Vertex(new Vector3( 0.5f, 0, 0));
        GL.Vertex(new Vector3( 0.5f, 1, 0));
        GL.End();
        
        // square
        var squreWidth = 100f / Screen.width;
        var squreHeight = 100f / Screen.height;
        
        GL.Begin(GL.LINES);
        GL.Color(Color.black);
        
        GL.Vertex(new Vector3( 0.5f, 0.5f + squreHeight, 0));
        GL.Vertex(new Vector3( 0.5f + squreWidth, 0.5f + squreHeight, 0));
        
        GL.Vertex(new Vector3( 0.5f + squreWidth, 0.5f + squreHeight, 0));
        GL.Vertex(new Vector3(0.5f + squreWidth, 0.5f, 0));
        GL.End();
        
        // circle
        DrawCircle(new Vector2(0.5f, 0.5f), 100f);

        for (int i = 0; i < 100; i++)
        {
            DrawCircle(new Vector2(Random.Range(0.5f, 0.5f + squreWidth), Random.Range(0.5f, 0.5f + squreHeight)), 1f);
        }
        
        GL.PopMatrix();
    }

    private void DrawCircle(Vector2 center, float radius)
    {
        float x = center.x;
        float y = center.y;
        float radiusWidth = radius / Screen.width;
        float radiusHeight = radius / Screen.height;
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        
        for (int i=0; i< 360; i++)
        {
            float degInRad = i*DEG2RAD;
            GL.Vertex(new Vector3(x, y, 0));

            x = center.x + Mathf.Cos(degInRad) * radiusWidth;
            y = center.y + Mathf.Sin(degInRad) * radiusHeight;
            
            GL.Vertex(new Vector3(x, y, 0));
        }
        GL.End();
    }
}
