using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Graph : MonoBehaviour
{

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        float step = 2f / resolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;

        points = new Transform[resolution * resolution];
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
            if (x == resolution){
                x = 0;
                z += 1;
            }
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            point.localPosition = position;
            point.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Vector3 position = point.localPosition;

            FunctionLibrary.Function f =  FunctionLibrary.GetFunction(function);
            position.y = f(position.x, position.z, time);
            
            point.localPosition = position;
        }
    }
}
