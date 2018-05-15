using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour {
    private int[] _scores;

    Vector2[] points;

    float a, b, c;
    float l;


    float x, y;

    // Use this for initialization
    void Start()
    {
        points = new Vector2[3];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(1, -1);
        points[2] = new Vector2(2, 0);
        CalcABC();
        l = CalcLength();
        x = CalcX(0.25f);
        y = CalcY(x);
        print(l);
        print(x);
        print(y);
    }

    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {




    }

    float CalcX(float r)
    {
        return Mathf.Sqrt(((r*l) + Mathf.Pow(Mathf.Sqrt(1 + Mathf.Pow((2*a*points[0].x) + b, 2)),2) - b)/(2*a));
    }

    float CalcY(float x)
    {
        return (a * Mathf.Pow(x, 2)) + (b * x) + c;
    }

    void CalcABC()
    {
        float A1 = -Mathf.Pow(points[0].x, 2) + Mathf.Pow(points[1].x, 2);
        //print(A1);
        float B1 = -points[0].x + points[1].x;
        //print(B1);
        float D1 = -points[0].y + points[1].y;
        //print(D1);
        float A2 = -Mathf.Pow(points[1].x, 2) + Mathf.Pow(points[2].x, 2);
        //print(A2);
        float B2 = -points[1].x + points[2].x;
        //print(B2);
        float D2 = -points[1].y + points[2].y;
        //print(D2);
        float BM = -(B2 / B1);
        //print(BM);
        float A3 = BM * A1 + A2;//Maybe Perenth
        //print(A3);
        float D3 = BM * D1 + D2;
        //print(D3);

        a = D3 / A3;
        b = (D1 - A1 * a) / B1;
        c = points[0].y + (a * Mathf.Pow(points[0].x, 2)) - (b * Mathf.Pow(points[0].x, 2));

    }

    float CalcLength()
    {
        return ( Mathf.Sqrt(Mathf.Pow(2 * a* points[2].x+b,2)+1) * (2 * a * points[2].x + b) + invSinH(2 * a * points[2].x + b) ) / (4 * a) -
            (Mathf.Sqrt(Mathf.Pow(2 * a * points[0].x + b, 2) + 1) * (2 * a * points[0].x + b) + invSinH(2 * a * points[0].x + b)) / (4 * a);
    }

    float invSinH(float x)
    {
        return Mathf.Log(x + Mathf.Sqrt(1 + Mathf.Pow(x, 2)));
    }
}
