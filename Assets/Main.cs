using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// 인하대학교 소프트웨어융합공학과
// 12184010 최순형

public class Main : MonoBehaviour
{
    private const float DEG2RAD = 3.14159f/180;
    
    [SerializeField] private Text sampleCountInput;
    [SerializeField] private Text resultText;
    [SerializeField] private Material mat;

    private List<Vector2> samplePosList = new List<Vector2>();
    private List<Vector2> samplePosInCircleList = new List<Vector2>();

    private float squareWidth;
    private float squareHeight;
    private float circleRadius = 100f;
    
    void Awake()
    {   
        squareWidth = 100f / Screen.width;
        squareHeight = 100f / Screen.height;
    }
    
    void OnPostRender()
    {
        GL.PushMatrix();
        mat.SetPass (0);
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
        DrawSquare(new Vector2(0.5f + squareWidth / 2, 0.5f + squareHeight / 2), squareWidth, squareHeight, Color.black);
        
        // circle
        DrawCircle(new Vector2(0.5f, 0.5f), circleRadius);

        // samples
        foreach (var pos in samplePosList)
            DrawSquare(pos, 0.001f, 0.001f, Color.red);
        
        foreach (var pos in samplePosInCircleList)
            DrawSquare(pos, 0.001f, 0.001f, Color.blue);
        
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

    private void DrawSquare(Vector2 center, float width, float height, Color color)
    {
        GL.Begin(GL.LINES);
        GL.Color(color);
        
        GL.Vertex(new Vector3( center.x - width / 2, center.y + height / 2, 0));
        GL.Vertex(new Vector3( center.x + width / 2, center.y + height / 2, 0));
        
        GL.Vertex(new Vector3( center.x + width / 2, center.y + height / 2, 0));
        GL.Vertex(new Vector3( center.x + width / 2, center.y - height / 2, 0));
        
        GL.Vertex(new Vector3( center.x + width / 2, center.y - height / 2, 0));
        GL.Vertex(new Vector3( center.x - width / 2, center.y - height / 2, 0));
        
        GL.Vertex(new Vector3( center.x - width / 2, center.y - height / 2, 0));
        GL.Vertex(new Vector3( center.x - width / 2, center.y + height / 2, 0));
        
        
        GL.End();
    }

    public void OnClickDraw()
    {
        int sampleCount = 1;
        int sampleInCircle = 0;
        try
        {
            sampleCount = Int32.Parse(sampleCountInput.text);
        }
        catch (Exception e)
        {
            
        }
        
        samplePosList.Clear();
        samplePosInCircleList.Clear();
        
        for (int i = 0; i < sampleCount; i++)
        {
            var samplePos = new Vector2(Random.Range(0.501f, 0.499f + squareWidth), Random.Range(0.501f, 0.499f + squareHeight));

            var sampleDist = Vector2.Distance(new Vector2(0.5f * Screen.width, 0.5f * Screen.height),
                new Vector2(samplePos.x * Screen.width, samplePos.y * Screen.height));
           
            if (sampleDist < circleRadius)
            {
                samplePosInCircleList.Add(samplePos);
                sampleInCircle++;
            }
            else
            {
                samplePosList.Add(samplePos);
            }
        }

        resultText.text = samplePosInCircleList.Count + " / " + sampleCount;
    }
}
