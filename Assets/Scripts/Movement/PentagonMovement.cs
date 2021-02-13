using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonMovement : MonoBehaviour
{
    Vector3 _targetPos;

    Animator anim;
    
    [SerializeField]
    GameObject spikes = null;

    Transform obstacleSpawner = null;
    //GameObject pentagon;

    [Range(4f, 7f)]
    public float _speed = 5f;

    bool _isAtTargetPostion;

    private void Awake()
    {
        // get a random position inside camera view
        _targetPos = new Vector2(Random.Range(0f, Camera.main.pixelWidth) / 72f - 5, Random.Range(0f, Camera.main.pixelHeight) / 72f - 5);
        obstacleSpawner = GameObject.FindGameObjectWithTag("EnemySpawner").transform;
        anim = GetComponent<Animator>();
        //pentagon = GetComponent<GameObject>();

    }
    private void Start()
    {
        StartCoroutine(PlayBlink());
    }


    void Update()
    {
        // move towards target position
        if (_isAtTargetPostion == false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _targetPos, _speed * Time.deltaTime);
            if (this.transform.position == _targetPos)
            {
                _isAtTargetPostion = true;
            }
        }
    }

    IEnumerator PlayBlink()
    {
        yield return new WaitUntil(() => _isAtTargetPostion);
        anim.SetTrigger("Blink");

    }

    public void Explode()
    {
        // play explosion

        // instantiate spikes

        Instantiate(spikes, this.transform.position + new Vector3(-0.0311f, 0.1908f), Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z), obstacleSpawner);   // top spike
        Instantiate(spikes, this.transform.position + new Vector3(0.1475f, 0.0417f), Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z - 65), obstacleSpawner);   // right top spike
        Instantiate(spikes, this.transform.position + new Vector3(0.08f, -0.1487f), Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z - 140), obstacleSpawner);   // right bottom spike
        Instantiate(spikes, this.transform.position + new Vector3(-0.1386f, -0.1522f), Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z - 225), obstacleSpawner);   // left bottom spike
        Instantiate(spikes, this.transform.position + new Vector3(-0.2077f, 0.0389f), Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z - 300), obstacleSpawner);   // left top spike
        Destroy(this.gameObject);
    }
}
