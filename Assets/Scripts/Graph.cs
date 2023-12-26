using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField, Range(0, 2)]
    int function = 0;
    Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        float step = 2f / resolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;

        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            position.x = (i + 0.5f) * step - 1f;
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
            switch (function)
            {
                case 0: 
                    position.y = FunctionLibrary.Wave(position.x, time);
                    break;
                case 1:
                    position.y = FunctionLibrary.MultiWave(position.x, time);
                    break;
                case 2:
                    position.y = FunctionLibrary.Ripple(position.x, time);
                    break;
            }
            
            point.localPosition = position;
        }
    }
}
