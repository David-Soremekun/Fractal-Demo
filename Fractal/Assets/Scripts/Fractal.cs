using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    
    //private Fractal child;
    struct FractalPart{
        public Vector3 dir;
        public Quaternion rot;
        public Transform tran;

    }
    public Quaternion deltaRotation;
    FractalPart[][] parts;
    FractalPart rootPart;
    [SerializeField, Range(1, 8)] public int fractalDepth = 7;
    
    [SerializeField]
    Mesh meshData;
    
    [SerializeField] 
    Material material;

    private static Vector3[] directions={Vector3.up,Vector3.right,Vector3.left, Vector3.forward, Vector3.back,Vector3.down};
    private static Quaternion[] rotations={Quaternion.identity, Quaternion.Euler(0.0f,0.0f,-90.0f),Quaternion.Euler(0.0f, 0.0f, 90.0f),
    Quaternion.Euler(90.0f,0.0f,0.0f),Quaternion.Euler(-90.0f,0.0f,0.0f), Quaternion.Euler(180.0f, 0.0f, 180.0f) };
    
    
    FractalPart CreatePart(int index,int childIndex, float scale){
        var go = new GameObject("Fractal Part" + index + " C " + childIndex);
        go.transform.localScale=scale* Vector3.one;
        go.transform.SetParent(transform,false);
        go.AddComponent<MeshFilter>().mesh=meshData;
        go.AddComponent<MeshRenderer>().material = material;
        return new FractalPart {
            dir=directions[childIndex],
            rot=rotations[childIndex],
            tran=go.transform
        };
    }

    void Awake(){
        deltaRotation = Quaternion.Euler(0.0f, 22.5f * Time.deltaTime, 0.0f);
        parts= new FractalPart[fractalDepth][];
        //int length=1;
        for(int i=0, length=1; i< parts.Length; i++,length*=6){
            parts[i]= new FractalPart[length];
            //length*=5;
        }
        float scale=1.0f;
        parts[0][0]=CreatePart(0,0,scale);
        rootPart= parts[0][0];
        rootPart.rot*=deltaRotation;
        rootPart.tran.localRotation= rootPart.rot;
        parts[0][0]= rootPart;

        for (int li = 1; li < parts.Length; li++) {
            scale*=0.5f;
			FractalPart[] levelParts = parts[li];
			for (int fpi = 0; fpi < levelParts.Length; fpi+=6) {
				for (int ci = 0; ci < 6; ci++) {
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
        //if (fractalDepth<=1)
        //{
        //    return;
        //}



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
        deltaRotation = Quaternion.Euler(0.0f, 22.5f * Time.deltaTime, 0.0f);
        for(int li=1; li<parts.Length; li++){
            FractalPart[] parentParts = parts[li - 1];
            FractalPart[] levelParts = parts[li];
            
            

            for(int fpi=0; fpi<levelParts.Length; fpi++){
                
                Transform parentTransform = parentParts[fpi / 6].tran;
                FractalPart part = levelParts[fpi];
                rootPart.rot *= deltaRotation;
                // Responsible for rotating the Fractals
                part.tran.localRotation=parentTransform.localRotation*part.rot;
                
                part.tran.localPosition =
					parentTransform.localPosition + parentTransform.localRotation*
                    (1.5f * part.tran.localScale.x * part.dir);
                levelParts[fpi]= part;
                
                
            }
        }
        transform.Rotate(0, 22.5f * Time.deltaTime, 0);
    }
}
