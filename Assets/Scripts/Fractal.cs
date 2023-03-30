using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : RuntimeMesh
{
    
    //private Fractal child;
    struct FractalPart{
        public Vector3 dir;
        public Quaternion rot;
        public Transform tran;

    }
    FractalPart[][] parts;

    [SerializeField, Range(1, 8)] public int fractalDepth = 7;
    Mesh mesh;
    //Serialize(mesh);
    
    [SerializeField] 
    Material material;

    private static Vector3[] directions={Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.up, Vector3.down};
    private static Quaternion[] rotations={Quaternion.identity, Quaternion.Euler(0.0f,0.0f,-90.0f),Quaternion.Euler(0.0f, 0.0f, 90.0f),
    Quaternion.Euler(90.0f,0.0f,0.0f),Quaternion.Euler(-90.0f,0.0f,0.0f),Quaternion.Euler(180.0f, 0.0f, 180.0f)};
    
    
    FractalPart CreatePart(int index,int childIndex, float scale){
        var go = new GameObject("Fractal Part" + index + " C " + childIndex);
        go.transform.localScale=scale* Vector3.one;
        go.transform.SetParent(transform,false);
        go.AddComponent<MeshFilter>().mesh=Mesh;
        go.AddComponent<MeshRenderer>().material = material;
        return new FractalPart {
            dir=directions[childIndex],
            rot=rotations[childIndex],
            tran=go.transform
        };
    }

    void Awake(){
        parts= new FractalPart[fractalDepth][];
        int length=1;
        for(int i=0, length=1; i< parts.Length; i++,length*=5){
            parts[i]= new FractalPart[length];
            //length*=5;
        }
        float scale=1.0f;
        parts[0][0]=CreatePart(0,0,scale);
        for (int li = 1; li < parts.Length; li++) {
            scale*=0.5f;
			FractalPart[] levelParts = parts[li];
			for (int fpi = 0; fpi < levelParts.Length; fpi++) {
				for (int ci = 0; ci < 5; ci++) {
					levelParts[fpi + ci] = CreatePart(li, ci,scale);
				}
			}
		}



    }
    
    /*Fractal CreateChild(Vector3 direction,Quaternion rotation)
    {   
        Fractal child = Instantiate(this);
        child.depth = depth - 1;
        child.transform.localPosition = 0.75f * direction;
        child.transform.localRotation = rotation;
        child.transform.localScale = 0.5f * Vector3.one;
        return child;
    }*/
    void Start()
    {   
        name = "Fractal " + fractalDepth;
        if (fractalDepth<=1)
        {
            return;
        }



        /*Fractal a = CreateChild(Vector3.up, Quaternion.identity);
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
        f.transform.SetParent(transform, false);*/
    }

    // Update is called once per frame
    void Update()
    {
        for(int li=1; li<parts.Length; li++){
            FractalPart[] parentParts= parts[li-1];
            FractalPart[] levelParts = parts[li];

            for(int fpi=0; fpi<levelParts.length; fpi++){
                
                Transform parentTransform = parentParts[fpi / 5].tran;
                FractalPart part = levelParts[fpi];
                // Responsible for rotating the Fractals
                part.transform.localRotation=parentTransform.localRotation*parts.rot;
                
                part.tran.localPosition =
					parentTransform.localPosition + parentTransform.localRotation
                    (1.5f * part.tran.localScale.x * part.dir);
            }
        }
        transform.Rotate(0.0f, 22.5f * Time.deltaTime, 0.0f);
    }
}
