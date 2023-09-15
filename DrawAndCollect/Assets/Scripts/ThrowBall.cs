using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject[] BucketPoint;
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject throwBallCenter;
    int ActiveBallIndex;
    int RandomBucketIndex;
    bool Lock;
    
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
                Balls[ActiveBallIndex].transform.position = throwBallCenter.transform.position;
                Balls[ActiveBallIndex].SetActive(true);
                float degree = UnityEngine.Random.Range(70, 110);
                Vector3 Pos = Quaternion.AngleAxis(degree, Vector3.forward) * Vector3.right;
                Balls[ActiveBallIndex].GetComponent<Rigidbody2D>().AddForce(Pos * 750f);

                if (ActiveBallIndex != Balls.Length - 1)
                    ActiveBallIndex++;
                else
                    ActiveBallIndex = 0;
                
                yield return new WaitForSeconds(.5f);

                RandomBucketIndex = UnityEngine.Random.Range(0, BucketPoint.Length - 1);
                Bucket.transform.position = BucketPoint[RandomBucketIndex].transform.position;
                Bucket.SetActive(true);
                Lock = true;
            }
            else
            {
                yield return null;
            }
        }
    }
    public void Return()
    {
        Lock = false;
        Bucket.SetActive(false);
    }
    
}
