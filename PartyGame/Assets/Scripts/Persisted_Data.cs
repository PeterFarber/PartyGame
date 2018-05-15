using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persited_Data: MonoBehaviour {

    private int[] _scores;

    Vector2[] points;

    float a, b, c;


	// Use this for initialization
	void Start () {

        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(1, 1);
        points[2] = new Vector2(1, 0);
        CalcABC();
        print(PassX(0.25f));
    }

    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update () {




    }

    float PassX(float x)
    {
        return a * Mathf.Pow(x, 2) + b * x + c;
    }

    void CalcABC()
    {
        float A1 = -Mathf.Pow(points[0].x, 2) + Mathf.Pow(points[1].x, 2);
        float B1 = -points[0].x + points[1].x;
        float D1 = -points[0].y + points[1].y;
        float A2 = -Mathf.Pow(points[1].x, 2) + Mathf.Pow(points[2].x, 2);
        float B2 = -points[1].x + points[2].x;
        float D2 = -points[1].y + points[2].y;
        float BM = -(B2 / B1);
        float A3 = BM * A1 + A2;//Maybe Perenth
        float D3 = BM * D1 + D2;

        a = D3 / A3;
        b = (D1 - A1 * a) / B1;
        c = points[0].y + (a * Mathf.Pow(points[0].x, 2)) - (b * Mathf.Pow(points[0].x, 2));

    }
}
