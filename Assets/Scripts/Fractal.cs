using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    
    private Fractal child;

    [SerializeField, Range(1, 8)]
    public int depth = 4;

    Fractal CreateChild(Vector3 direction,Quaternion rotation)
    {   
        Fractal child = Instantiate(this);
        child.depth = depth - 1;
        child.transform.localPosition = 0.75f * direction;
        child.transform.localRotation = rotation;
        child.transform.localScale = 0.5f * Vector3.one;
        return child;
    }
    void Start()
    {   
        name = "Fractal " + depth;
        if (depth<=1)
        {
            return;
        }



        Fractal a = CreateChild(Vector3.up, Quaternion.identity);
        Fractal b = CreateChild(Vector3.right,Quaternion.Euler(0.0f,0.0f,-90.0f));
        Fractal c = CreateChild(Vector3.left, Quaternion.Euler(0.0f, 0.0f, 90.0f));
        Fractal d = CreateChild(Vector3.forward,Quaternion.Euler(90.0f,0.0f,0.0f));
        Fractal e = CreateChild(Vector3.back,Quaternion.Euler(-90.0f,0.0f,0.0f));
        Fractal f = CreateChild(Vector3.down, Quaternion.Euler(180.0f, 0.0f, 180.0f));


        a.transform.SetParent(transform, false);
        b.transform.SetParent(transform, false);
        c.transform.SetParent(transform, false);
        d.transform.SetParent(transform, false);
        e.transform.SetParent(transform, false);
        f.transform.SetParent(transform, false);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0.0f, 22.5f * Time.deltaTime, 0.0f);
    }
}
