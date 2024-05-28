using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;

    public GameObject lineGravity;
    public GameObject lineNoGravity;

    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;

    public bool gravity_ = true;

    public AudioSource audioSource;

    void Start()
    {
        linePrefab = lineGravity;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
                audioSource.Play();
        }
            if (Input.GetMouseButton(0))
            {
                
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                {
                    UpdateLine(tempFingerPos);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                audioSource.Stop();
            
                if(gravity_)
                    gravity();
                Debug.Log("up");
            }
    }
    int x = 0;
    int lineNum = 0;
    
    void CreateLine()
    {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            currentLine.name = $"Line {x}";
            x++;
            lineNum++;

        if (lineNum > 3)
        { 
            Destroy(GameObject.Find($"Line {x - 4}"));
            lineNum--;
        }

            lineRenderer = currentLine.GetComponent<LineRenderer>();
            edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
            fingerPositions.Clear();
            fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lineRenderer.SetPosition(0, fingerPositions[0]);
            lineRenderer.SetPosition(1, fingerPositions[1]);
            edgeCollider.points = fingerPositions.ToArray();
    }

    PolygonCollider2D poligonCol;

    [SerializeField] int lineLenght;

    void UpdateLine(Vector2 newFingerPos)
    {
        if (lineRenderer.positionCount < lineLenght)
        {
            fingerPositions.Add(newFingerPos);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
            edgeCollider.points = fingerPositions.ToArray();
        }
       
    }

    Rigidbody2D rb;
    Vector3 WeightCentre;

    void gravity()
    {
        GameObject pom = GameObject.Find($"Line {x - 1}");
        pom.AddComponent<Rigidbody2D>();
        rb = pom.GetComponent<Rigidbody2D>();
        rb.gravityScale = 5;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        lineRenderer.GetPosition(1);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            WeightCentre += lineRenderer.GetPosition(i);
        }
        WeightCentre = WeightCentre / lineRenderer.positionCount;

        rb.centerOfMass = WeightCentre;
        WeightCentre = new Vector3(0, 0, 0);
    }
}
