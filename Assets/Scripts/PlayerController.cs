using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed = 12.0f;
    [SerializeField]
    int hp = 3;

    public GameController gameController;

    private Vector3 newPos;
    private float operationInterval = 0.23f;//連続操作対策
    private float time = 0.0f;

    private MeshRenderer playerMesh;
    private float blinkTime = 1.0f; //ダメージを受けた時の点滅時間
    private float blinkInterval = 0.1f; //点滅の周期時間
    private float bTime = 0.0f; //
    private float bTime2 = 0.0f;
    private bool isDamaged = false;

    public GameObject stage;

    //行動履歴を格納しておくスタック。行動を戻すときに使う
    private Stack<int> OpeHist = new Stack<int>();
    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int STRIGHT = 2;

    public GameObject destroyEffect;
    private Vector3 effectPos;


    private AudioSource audio;
    public AudioClip destroySound;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip getCoinSound;
   
    public GameObject mainCamera;
    public GameObject UIController;//現在の距離（スコア）を表すUI




    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
        playerMesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if (isDamaged)
        {
            Blink();  //ダメージを受けたら点滅して一定時間操作不可になる
        }
        else
        {
            Move(); //点滅中は動けない
        }
    }

    //後戻り機能ありの操作方法
    void Move()
    {
        time += Time.deltaTime;//連続操作できないようにする
        if (time > operationInterval)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //右のブロックにいるときは右に移動せず前に移動する
                if (transform.position.x < 0.6f)
                {
                    MoveRight();
                    UIController.GetComponent<UIController>().IncreaseDist();
                    audio.PlayOneShot(jumpSound);
                }
                else
                {
                    MoveStraigt();
                    UIController.GetComponent<UIController>().IncreaseDist();
                    audio.PlayOneShot(jumpSound);
                }
                time = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //左のブロックにいるときは左に移動せず前に移動する
                if (transform.position.x > -0.4f)
                {
                    MoveLeft();
                    UIController.GetComponent<UIController>().IncreaseDist();
                    audio.PlayOneShot(jumpSound);
                }
                else
                {
                    MoveStraigt();
                    UIController.GetComponent<UIController>().IncreaseDist();
                    audio.PlayOneShot(jumpSound);
                }

                time = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveStraigt();
                UIController.GetComponent<UIController>().IncreaseDist();
                audio.PlayOneShot(jumpSound);
                time = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))//行動の巻き戻し
            {

                var opeNum = OpeHist.Pop();//最後の行動履歴を削除

                if (opeNum == LEFT)
                {
                    transform.DOJump(new Vector3(transform.position.x - 1.0f, 0, 0), 1.2f, 1, operationInterval).SetEase(Ease.Linear);//jumpアニメーション
                    audio.PlayOneShot(jumpSound);

                    Vector3 newPos = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z - 1);
                    stage.transform.DOMove(newPos, operationInterval);
                    time = 0f;
                }
                else if (opeNum == RIGHT)
                {
                    transform.DOJump(new Vector3(transform.position.x + 1.0f, 0, 0), 1.2f, 1, operationInterval).SetEase(Ease.Linear);//jumpアニメーション
                    audio.PlayOneShot(jumpSound);

                    Vector3 newPos = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z - 1);
                    stage.transform.DOMove(newPos, operationInterval);
                    time = 0f;
                }
                else if (opeNum == STRIGHT)
                {
                    transform.DOJump(new Vector3(transform.position.x, 0, 0), 1.2f, 1, operationInterval).SetEase(Ease.Linear);//jumpアニメーション
                    audio.PlayOneShot(jumpSound);

                    Vector3 newPos = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z - 1);
                    stage.transform.DOMove(newPos, operationInterval);
                    time = 0f;
                }

                UIController.GetComponent<UIController>().DecreaseDist();

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "BlackBlock")//黒ブロックを踏んだ時の処理
        {
            PlayerDamaged();
        }

        if (other.gameObject.tag == "Coin")
        {
            GetCoin();
        }

    }


    void MoveRight()
    {
        transform.DOJump(new Vector3(transform.position.x + 1.0f, 0, 0), 1.2f, 1, operationInterval).SetEase(Ease.Linear);//jumpアニメーションしながら移動

        //stageを動かしplayerが動いているかのように見せる
        Vector3 newPos = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z + 1);
        stage.transform.DOMove(newPos, operationInterval);
        
        //行動履歴に追加
        OpeHist.Push(LEFT);
    }

    void MoveLeft()
    {
        transform.DOJump(new Vector3(transform.position.x + -1.0f, 0, 0), 1.2f, 1, operationInterval).SetEase(Ease.Linear);//jumpアニメーション

        Vector3 newPos = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z + 1);
        stage.transform.DOMove(newPos, operationInterval);

        OpeHist.Push(RIGHT);
    }

    void MoveStraigt()
    {
        transform.DOJump(new Vector3(transform.position.x, 0, 0), 1.2f, 1, operationInterval).SetEase(Ease.Linear);//jumpアニメーション

        Vector3 newPos = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z + 1);
        stage.transform.DOMove(newPos, operationInterval);

        OpeHist.Push(STRIGHT);
    }


    //playerがダメージ受けた時の処理
    void PlayerDamaged()
    {
        mainCamera.GetComponent<CameraController>().ShakeCamera();//被ダメージのカメラの揺れ演出
        isDamaged = true;
        audio.PlayOneShot(damageSound);
    }


    void Blink()//一定時間(blinkTime)の間点滅させる
    {
        bTime += Time.deltaTime;
        if (blinkInterval > bTime)
        {
            playerMesh.enabled = !playerMesh.enabled;
            bTime = 0f;
        }

        bTime2 += Time.deltaTime;
        if (bTime2 > blinkTime)
        {
            playerMesh.enabled = true;
            isDamaged = false;
            bTime2 = 0.0f;
        }
    }


    void PlayerDestroy()
    { 
        effectPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Instantiate(destroyEffect, effectPos, Quaternion.identity);
        //削除すると音が鳴らなくなる対策
        AudioSource.PlayClipAtPoint(destroySound, mainCamera.transform.position);
        Destroy(gameObject);
    }

    void GetCoin()
    {
        gameController.GetComponent<GameController>().AddCoinScore();
        audio.PlayOneShot(getCoinSound);
    }

}
