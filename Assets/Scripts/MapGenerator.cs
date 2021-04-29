using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    public float width = 100;
    public float height = 100;
    [Range(0f,0.1f)] public float density = 0.1f;
    public float margin = 1.5f;

    public bool colorUpdateMode = true;


    public GameObject m1,m2,m3,m4,m5;
    public GameObject plane;
    public GameObject lightPrefab;

    private GameObject[] prefabs;
    private (int, int, int)[] colors;
    List<GameObject> list;


    void Start()
    {
        prefabs = new GameObject[5] { m1, m2, m3, m4, m5 };
        colors = new (int,int,int)[] { (255, 235, 0), (252, 0, 25), (1, 255, 79), (255, 1, 215), (86, 0, 204), (0, 237, 245) };

        list = new List<GameObject>();

        Generate(0);
    }

    void FixedUpdate()
    {
        if (colorUpdateMode)
        {
            float m = Random.Range(0f, 1f);
            if (m > 0.5 && m < 0.700)
            {
                ChangePlaneColor();
                ChangeShroomColor();
            }

        }
    }

    void Generate(float y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                if (Random.Range(0f,1f) > (1-density))
                {
                    int index = Random.Range(0, 5);
                    GameObject tmp = Instantiate(prefabs[index], new Vector3(x * margin, y, z * margin), Quaternion.identity);

                    int rscale = Random.Range(1000, 5000);
                    Vector3 scale = new Vector3(rscale, rscale, rscale);
                    tmp.transform.eulerAngles.Set(-90,0,0);
                    tmp.transform.Rotate(-90, 0, 0);
                    tmp.transform.localScale = scale;
                    
                    int rcolor = Random.Range(0, colors.Length);
                    tmp.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(colors[rcolor].Item1, colors[rcolor].Item2, colors[rcolor].Item3));

                    list.Add(tmp);
                }
            }
        }
    }

    void ChangeShroomColor()
    {
        foreach(GameObject g in list)
        {
            if (Random.Range(0f, 1f) > 0.99)
            {
                int rcolor = Random.Range(0, colors.Length);
                g.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(colors[rcolor].Item1, colors[rcolor].Item2, colors[rcolor].Item3));

            }
        }
    }

    void ChangePlaneColor()
    {
        int rcolor = Random.Range(0, colors.Length);
        plane.GetComponent<Renderer>().material.SetColor("_Color", new Color(colors[rcolor].Item1, colors[rcolor].Item2, colors[rcolor].Item3));
    }

}

