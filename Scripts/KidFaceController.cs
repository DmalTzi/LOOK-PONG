using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidFaceController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BallObj;
    public BalloonController BallScr;
    string[] mood = { "FaceHappy", "FaceCry", "FaceNormal", "FaceAngry" };
    public GameObject[] KidFacePrefab;
    

    public void Start()
    {
        int child = transform.childCount;
        Debug.Log(KidFacePrefab.Length);
        if (child <= 0)
        {
            CreateOne();
            Debug.Log("Create  ONe");
        }
        showNormal();
    }

    // Update is called once per frame
    public void Update()
    {
        if(BallScr.isPop){
            showCry();
        }else if(BallScr.isBlow){
            if(BallScr.checkState()){
                ShowHappy();
            }else{
                showNormal();
        }
        }
        
    }

    private void Getout(string moodSelect)
    {
        for (int i = 0; i < mood.Length; i++)
        {
            if (mood[i] != moodSelect)
            {
                gameObject.transform.GetChild(0).Find(mood[i]).transform.position = new Vector3(transform.position.x, transform.position.y - 400, transform.position.z);
            }
        }
    }

     public void CreateOne(){
        int random = Random.Range(0, KidFacePrefab.Length);
         Instantiate(KidFacePrefab[random], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
     }


    public void DeleteOne()
    {
        Debug.Log("destory");
        Destroy(transform.GetChild(0).gameObject);
    }

    public void ShowHappy()
    {
        transform.GetChild(0).transform.Find("FaceHappy").transform.position = new Vector3(transform.position.x, transform.position.y + 0, transform.position.z);
        Getout("FaceHappy");

    }

    public void showNormal()
    {
        transform.GetChild(0).transform.Find("FaceNormal").transform.position = new Vector3(transform.position.x, transform.position.y + 0, transform.position.z);
        Getout("FaceNormal");

    }

    public void showCry()
    {
        transform.GetChild(0).transform.Find("FaceCry").transform.position = new Vector3(transform.position.x, transform.position.y + 0, transform.position.z);
        Getout("FaceCry");
    }

    public void showAngry()
    {
        Debug.Log("showAngry");
        transform.GetChild(0).transform.Find("FaceAngry").transform.position = new Vector3(transform.position.x, transform.position.y + 0, transform.position.z);
        Getout("FaceAngry");
    }


}
