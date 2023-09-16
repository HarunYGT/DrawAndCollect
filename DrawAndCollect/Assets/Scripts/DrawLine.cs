using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject line;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    public List<Vector2> fingerPosList;
    [SerializeField] TextMeshProUGUI drawText;

    public List<GameObject> Lines;
    bool isDraw;
    int DrawNum;
    void Start()
    {
        isDraw = false;
        DrawNum = 3;
        drawText.text = DrawNum.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDraw && DrawNum != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(fingerPos, fingerPosList[^1]) > .1f)
                    UpdateTheLine(fingerPos);
            }
        }
        if (Lines.Count != 0 && DrawNum != 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                DrawNum--;
                drawText.text = DrawNum.ToString();
            }
        }

    }
    void CreateLine()
    {
        line = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        Lines.Add(line);
        lineRenderer = line.GetComponent<LineRenderer>();
        edgeCollider2D = line.GetComponent<EdgeCollider2D>();
        fingerPosList.Clear();
        fingerPosList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPosList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPosList[0]);
        lineRenderer.SetPosition(1, fingerPosList[1]);
        edgeCollider2D.points = fingerPosList.ToArray();
    }

    void UpdateTheLine(Vector2 fingerPos)
    {
        fingerPosList.Add(fingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, fingerPos);
        edgeCollider2D.points = fingerPosList.ToArray();
    }
    public void Return()
    {
        if (ThrowBall.numOfBall == 0)
        {
            foreach (var item in Lines)
            {
                Destroy(item.gameObject);
            }
            Lines.Clear();
            DrawNum = 3;
            drawText.text = DrawNum.ToString();
        }
    }
    public void Drawing()
    {
        DrawNum = 3;
        isDraw = true;
    }
    public void NotDraw()
    {
        isDraw = false;
    }
}
