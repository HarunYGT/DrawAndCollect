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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Balls[ActiveBallIndex].transform.position = throwBallCenter.transform.position;
            Balls[ActiveBallIndex].SetActive(true);
            float degree = Random.Range(70,110);
            Vector3 Pos = Quaternion.AngleAxis(degree,Vector3.forward)*Vector3.right; 
            Balls[ActiveBallIndex].gameObject.GetComponent<Rigidbody2D>().AddForce(Pos*750f);

            if(ActiveBallIndex != Balls.Length-1)
                ActiveBallIndex++;
            else
                ActiveBallIndex=0;

            
            Invoke("RevealTheBucket",.5f);
        }
    }
    void RevealTheBucket()
    {
        RandomBucketIndex = Random.Range(0,BucketPoint.Length-1);
        Bucket.transform.position = BucketPoint[RandomBucketIndex].transform.position;
        Bucket.SetActive(true);

    }

}
