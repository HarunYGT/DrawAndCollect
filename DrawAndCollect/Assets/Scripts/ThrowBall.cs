using System.Collections;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject[] BucketPoint;
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject throwBallCenter;
    [SerializeField] private GameManager gameManager;
    int ActiveBallIndex;
    int RandomBucketIndex;
    bool Lock;

    public static int numOfBall;
    public static int ballThrowNum;

    void Start()
    {
        ballThrowNum = 0;
        numOfBall = 0;
    }

    public void GameStart()
    {
        StartCoroutine(BallThrowSystem());
    }
    IEnumerator BallThrowSystem()
    {
        while (true)
        {
            if (!Lock)
            {
                yield return new WaitForSeconds(.5f);

                if (ballThrowNum != 0 && ballThrowNum % 5 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        BallThrowAndSet();
                    }
                    numOfBall = 2;
                    ballThrowNum++;
                }
                else
                {
                    BallThrowAndSet();
                    numOfBall =1;
                    ballThrowNum++;
                }
                yield return new WaitForSeconds(.5f);

                RandomBucketIndex = UnityEngine.Random.Range(0, BucketPoint.Length - 1);
                Bucket.transform.position = BucketPoint[RandomBucketIndex].transform.position;
                Bucket.SetActive(true);
                Lock = true;
                Invoke("CheckBall", 5f);
            }
            else
            {
                yield return null;
            }
        }
    }
    public void Return()
    {
        if (numOfBall == 1)
        {
            numOfBall--;
            Debug.Log(numOfBall);
            Lock = false;
            Bucket.SetActive(false);
            CancelInvoke();
            
        }
        else
        {
            Debug.Log(numOfBall);
            numOfBall--;
        }
    }
    void CheckBall()
    {
        if (Lock)
            gameManager.GameOver();
    }
    float GiveDegree(float v1, float v2)
    {
        return UnityEngine.Random.Range(v1, v2); ;
    }
    Vector3 GivePos(float degree)
    {
        return Quaternion.AngleAxis(degree, Vector3.forward) * Vector3.right; ;
    }
    void BallThrowAndSet()
    {
        Balls[ActiveBallIndex].transform.position = throwBallCenter.transform.position;
        Balls[ActiveBallIndex].SetActive(true);
        Balls[ActiveBallIndex].GetComponent<Rigidbody2D>().AddForce(GivePos(GiveDegree(70, 110)) * 750f);

        if (ActiveBallIndex != Balls.Length - 1)
            ActiveBallIndex++;
        else
            ActiveBallIndex = 0;
    }
}
