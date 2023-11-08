using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour
{
    public bool isPlay;
    public bool isBlow;
    private Vector3 originalScale;
    public bool isPop;

    
    public float Speed = 0;
    public float mouseSensitivity = 100;
    public float PerfectOutline;
    public float RedZoneOutline = 0;
    public Image img;

    public Text ScoreText;
    public int score = 0;

    public Text TimeText;
    public float time = 60;
    public Text PsiText;

    public KidFaceController Kid;

    AudioSource audioSource;
    public AudioClip PopSound;
    public AudioClip BlowSound;

   public void Start() {
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        originalScale = transform.localScale;
        PerfectOutline = Random.Range(1f,2f);
        RedZoneOutline = randomRedZone(PerfectOutline);
        isPlay = true;
        isBlow = true;
        isPop = false;
        img = GetComponent<Image>();
        Debug.Log("Game Start");
   }

   public void Update() {
    time -= Time.deltaTime;
    if(time <= 0f){
            isPlay = false;
            SceneManager.LoadScene("GAMEOVER");
        Debug.Log("IS Game Over?");
        return;
        }else{
            TimeText.text = "Time : "+time.ToString("0");
        }
    if(isPlay){
        if(isBlow){
            Speed += Input.GetAxis("Mouse Y") * mouseSensitivity * 0.001f;
            Speed = Mathf.Max(Speed, 0f);
            PsiText.text = Mathf.Round(Speed * 10).ToString()+"\nPSI";

            float scaleFactor = 0.001f * Speed;
            transform.localScale = new Vector3(transform.localScale.x + scaleFactor, transform.localScale.y + scaleFactor, 0);

            if (transform.localScale.x >= RedZoneOutline){
                checkPop();
            }
            

            if(Input.GetKeyDown(KeyCode.Space)){
                    if(transform.localScale.x >= PerfectOutline - .05 && transform.localScale.x <= PerfectOutline + .05)
                    {
                        score += 3;
                        ScoreText.text = score.ToString();
                        Kid.ShowHappy();
                    }
                    else if(transform.localScale.x >= PerfectOutline - .15 && transform.localScale.x <= PerfectOutline + .15) {
                        score += 1;
                        ScoreText.text = score.ToString();
                        Kid.showNormal();
                    }
                    else
                    {
                        Kid.showAngry();
                    }
                /*if(checkState()){
                    Debug.Log("perfect");
                    score += 1;
                    ScoreText.text = score.ToString();
                }else{
                    Debug.Log("Bad");
                        Kid.showAngry();
                }*/
                isBlow = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.R)){
            if(!isBlow){
                    Kid.DeleteOne();
                    Kid.CreateOne();
                    NewBall();
            }
        }
    }
   }

    public bool checkState(){
        return transform.localScale.x >= PerfectOutline -.05 && transform.localScale.x <= PerfectOutline +.05;
    }

    public void checkPop(){
            score -= 1;
            ScoreText.text = score.ToString();
            isBlow = false;
            Speed =  0;
            img.enabled = false;
            isPop = true;
        audioSource.PlayOneShot(PopSound);

    }

    private float randomRedZone(float min){
        while (RedZoneOutline <= min+.1f){
            RedZoneOutline = Random.Range(1f,2f);
        }
        return RedZoneOutline;
    }

    public void NewBall(){
        Kid.showNormal();
        transform.localScale = originalScale;
        isBlow = true;
        isPop = false;
        PerfectOutline = Random.Range(1f,2f);
        RedZoneOutline = randomRedZone(PerfectOutline);
        img.enabled = true;
        Speed = 0;
    }
}
