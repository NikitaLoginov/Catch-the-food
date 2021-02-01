using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody targetRb;
    GameManager gameManager;
    float minSpeed = 12;
    float maxSpeed = 16;
    float maxTorque = 10;
    float xRange = 4;
    float ySpawnPos = -2;
    float yLowBoundary = 0;
    int pointValue;

    //screen boundaries

    BoxCollider targetBoxCollider;

    public ParticleSystem explosionParticle;
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetBoxCollider = GetComponent<BoxCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos(); 
    }


    void Update()
    {
        OffScreemBumpCheck();
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Bad_01"))
        {
            pointValue = -10;
        }
        else if (gameObject.CompareTag("Good_01"))
        {
            pointValue = 5;
        }
        else if(gameObject.CompareTag("Good_02"))
        {
            pointValue = 10;
        }
        else if(gameObject.CompareTag("Good_03"))
        {
            pointValue = 15;
        }

        if (gameManager.isGameActive)
        { 
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad_01"))
        { 
            gameManager.GameOver();
        }
    }

    //Helper methods
    //Checks that object doesn't bump offscreen
    private void OffScreemBumpCheck()
    {
        if (transform.position.y < yLowBoundary && transform.position.y > ySpawnPos)
        {
            targetBoxCollider.enabled = false;
        }
        if (transform.position.y >= yLowBoundary || transform.position.y < ySpawnPos)
        {
            targetBoxCollider.enabled = true;
        }
    }
    //Calculating random force
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    //Calculating random torque
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    //Calculating random spawn position
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); // 0 for z axis is removed, because it'd be 0 by default
    }
}
