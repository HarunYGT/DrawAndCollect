using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject line;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    public List<Vector2> fingerPosList;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if(Input.GetMouseButton(0))
        {
            Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(fingerPos,fingerPosList[^1])>.1f) 
                UpdateTheLine(fingerPos);
        }
    }
    void CreateLine()
    {
        line = Instantiate(linePrefab,Vector2.zero,Quaternion.identity);
        lineRenderer = line.GetComponent<LineRenderer>();
        edgeCollider2D =  line.GetComponent<EdgeCollider2D>();
        fingerPosList.Clear();
        fingerPosList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPosList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0,fingerPosList[0]);
        lineRenderer.SetPosition(1,fingerPosList[1]);
        edgeCollider2D.points = fingerPosList.ToArray();
    }

    void UpdateTheLine(Vector2 fingerPos)
    {
        fingerPosList.Add(fingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1,fingerPos);
        edgeCollider2D.points = fingerPosList.ToArray();
    }
}
