using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[ExecuteAlways,RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SplineMeshGenerator : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField, Range(4, 10)] private int steps = 6;
    [SerializeField] private LineRenderer line;
    [SerializeField] private LineRendererSmoother smoother;

    private Mesh mesh;

    private void Start()
    {
        if (Application.IsPlaying(gameObject))
        {
            if(smoother != null) 
                smoother.Smoth();
            
            Generate();
        }
    }

    private void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            Generate();
        }
    }

    private void Generate()
    {
        mesh = new Mesh {
            name = "Spline"
        };

        var vertices = new Vector3[line.positionCount * steps];
        var normals = new Vector3[vertices.Length];
        var triangles = new List<int>();
        
        if(line.positionCount < 2) return;

        for (var i = 0; i < line.positionCount; i++)
        {
            var pos = line.GetPosition(i);
            var trpos = transform.InverseTransformPoint(pos);
            var circle = GenerateCircle(steps, radius, trpos);

            Quaternion rot;
            Vector3 dir;
            
            if (i == line.positionCount - 1)
            {
                dir = (pos - line.GetPosition(i - 1)).normalized;
                rot = Quaternion.LookRotation(-dir);
            } 
            else if (i == 0)
            {
                dir = (pos - line.GetPosition(i + 1)).normalized;
                rot = Quaternion.LookRotation(dir);
            }
            else
            {
                dir = (line.GetPosition(i - 1) - line.GetPosition(i + 1)).normalized;
                rot = Quaternion.LookRotation(dir);
            }
            
            for (var j = 0; j < circle.Length; j++)
            {
                circle[j] = RotatePoint(circle[j], trpos, rot);

                var vertIndex = j + i * steps;
                
                vertices[vertIndex] = circle[j];
                normals[vertIndex] = circle[j] - trpos;
            }

            if(i == line.positionCount - 1) break;

            var arrayOfIndexes = Enumerable.Range(i * steps, steps).ToArray();
            
            triangles.AddRange(ConnectCircles(arrayOfIndexes).ToArray());
        }
        
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals;
        
        GetComponent<MeshFilter>().mesh = mesh;
    }
    
    private int[] ConnectCircles(int[] indexes)
    {
        var triangles = new List<int>();
        for (int i = 0; i < indexes.Length; i++)
        {
            var curIndex = indexes[i];
            
            triangles.Add(curIndex);
            triangles.Add(curIndex + steps);
            var final = curIndex + steps + 1;
            final = final > indexes[^1] + steps ? indexes[^1] + 1 : final;
            triangles.Add(final);

            var isFinal = final == curIndex + 1;
            final = isFinal ? indexes[0] : final;
            if (!isFinal)
            {
                triangles.Add(final);
                triangles.Add(curIndex + 1);
                triangles.Add(curIndex); 
            }
            else
            {
                triangles.Add(curIndex);
                triangles.Add(curIndex + 1);
                triangles.Add(final);
            }
            
        }

        return triangles.ToArray();
    }

    private Vector3 RotatePoint(Vector3 point, Vector3 center, Quaternion rot)
    {
        Matrix4x4 translationToCenterPoint = Matrix4x4.Translate(center);
        Matrix4x4 rotation = Matrix4x4.Rotate(rot);
        Matrix4x4 translationBackToOrigin = Matrix4x4.Translate(-center);

        Matrix4x4 resultMatrix = translationToCenterPoint * rotation * translationBackToOrigin;
        return resultMatrix.MultiplyPoint3x4(point);
    }

    private Vector3[] GenerateCircle(int steps, float radius, Vector3 pos)
    {
        var vert = new Vector3[steps];
        
        for (var curStep = 0; curStep < steps; curStep++)
        {
            var progress = (float) curStep / steps;
            progress *= 2 * Mathf.PI;
            
            var x = Mathf.Cos(progress) * radius;
            var y = Mathf.Sin(progress) * radius;

            vert[curStep] = new Vector3(x, y, 0) + pos;
        }

        return vert;
    }

    private void OnDrawGizmos()
    {
        return;

        
        Gizmos.color = Color.red;

        /*var m = GetComponent<MeshFilter>().sharedMesh;

        int i = 0;
        
        foreach (var vertex in m.vertices)
        {
            Gizmos.DrawSphere(vertex, 0.01f);
            Handles.Label(vertex, i.ToString());
            i++;
        }*/

        
        for (int i = 0; i < line.positionCount; i++)
        {
            var pos = line.GetPosition(i);
            
            Handles.Label(pos,pos.ToString());
        }
    }
    
    private void DebugArray(string name, int[] array)
    {
        var sb = new StringBuilder();
        var s = 0;
        foreach (var i in array)
        {
            s++;
            
            if (s == 1)
            {
                sb.Append("[");
            }
            
            sb.Append(i);
            sb.Append(" ,");
            
            if (s == 3)
            {
                sb.Append("]");
                s = 0;
            }
        }
        
        Debug.Log(name + " " + sb.ToString());
    }
}
