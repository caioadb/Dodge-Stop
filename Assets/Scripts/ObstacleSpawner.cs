using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] _figures = null;
    [SerializeField]
    Transform _playerPos = null;
    [SerializeField]
    GameObject _dangerSign = null;

    public bool[] _isBeingUsed = new bool[4];

    [Range (0.01f, 4f)]
    public float _delayBetweenPatterns;

    public List<PatternList> patterns;

    private void Start()
    {
        StartCoroutine(ChooseObject());
    }
    
    /*
     Determines which figure will be instantiated
     */
    IEnumerator ChooseObject()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            _delayBetweenPatterns = Mathf.Clamp(4 - ScoreContainer.scoreScript.score / 240f, 0.5f, 4f);

            int chosen = Random.Range(0, patterns.Count);
            int posChosen = (int)patterns[chosen].pattern[0];

            while (_isBeingUsed[posChosen])   // make sure to not spawn a figure while it has a pattern being spawned
            {
                if(_isBeingUsed[0] && _isBeingUsed[1] && _isBeingUsed[2] && _isBeingUsed[3])    // if every pattern is being used, wait until one becomes available
                {
                    yield return new WaitUntil(() => !_isBeingUsed[0] || !_isBeingUsed[1] || !_isBeingUsed[2] || !_isBeingUsed[3]);
                }
                chosen = Random.Range(0, patterns.Count);
                posChosen = (int)patterns[chosen].pattern[0];
            }

            _isBeingUsed[posChosen] = true;
            StartCoroutine(SpawnObjects(chosen));
            yield return new WaitForSeconds(_delayBetweenPatterns);
        }
    }

    /*
     Determines position & rotation
     and instantiates figure
     */
    IEnumerator SpawnObjects(int chosen)
    {

        GameObject toSpawn = _figures[(int)patterns[chosen].pattern[0]];    // object to be spawned by this coroutine
        Vector3 position = CalculatePosition((int)patterns[chosen].pattern[0]); // position where object will be spawned
        position.z = 2f;

        SpawnDangerSign(position);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < patterns[chosen].pattern[1]; i++)
        {
           
            Vector3 rotation = CalculateRotation((int)patterns[chosen].pattern[0], position); // rotation of object spawned
            Instantiate(toSpawn, position, Quaternion.Euler(rotation), this.transform); 
            yield return new WaitForSeconds(patterns[chosen].pattern[2]);

        }

        int posChosen = (int)patterns[chosen].pattern[0];
        _isBeingUsed[posChosen] = false;   // allow figure to be chosen again

        yield return null;

    }
    /*
     Calculates a spawn position based on type of shape
     Returns Vector2 with calculated position
     */
    Vector2 CalculatePosition (int type)
    {
        float x=0, y=0;
        float spawnPos = Camera.main.orthographicSize + 1;

        if (type == 1)  // if spawning squares, choose between four positions
        {
            int position = Random.Range(0, 4);
            switch (position)
            {
                case 0: // spawn middle from bottom, go up
                    x = 0;
                    y = -spawnPos;
                    break;
                case 1: // spawn middle from left, go right
                    x = -spawnPos;
                    y = 0;
                    break;
                case 2: // spawn middle from top, go down
                    x = 0;
                    y = spawnPos;
                    break;
                case 3: // spawn middle from right, go left
                    x = spawnPos;
                    y = 0;
                    break;
                default:
                    Debug.LogError("random range out of bounds CalculatePosition.");
                    break;
            }
        }
        else    // if spawning anything else
        {
            int position = Random.Range(0, 4);
            switch (position)
            {
                case 0: // spawn from bottom
                    x = Random.Range(-5f, 5f);
                    y = -spawnPos;
                    break;
                case 1: // spawn from left
                    x = -spawnPos;
                    y = Random.Range(-5f, 5f);
                    break;
                case 2: // spawn from top
                    x = Random.Range(-5f, 5f);
                    y = spawnPos;
                    break;
                case 3: // spawn from right
                    x = spawnPos;
                    y = Random.Range(-5f, 5f);
                    break;
                default:
                    Debug.LogError("random range out of bounds CalculatePosition.");
                    break;
            }
        }

        return new Vector2(x, y);
    }

    /*
     Calculates the quaternion rotation depending on
     figure type and position
     */
    Vector3 CalculateRotation (int type, Vector2 position)
    {
        float spawnPos = Camera.main.orthographicSize + 1;
        Vector3 rotationQuaternion = new Vector3(0, 0, 0);

        if (type == 1)  // if spawning a square
        {
            if (position.x == 0) // if it is coming from bottom or top
            {
                if (position.y > 0) // if coming from top middle
                {
                    rotationQuaternion = new Vector3(0, 0, 180);    // make vector2.up point downward
                }
                else    // if coming from bottom middle
                {
                    rotationQuaternion = new Vector3(0, 0, 0);    // make vector2.up point upward   
                }
            }
            else    // if it is coming from right or left
            {
                if (position.x > 0) // if coming from right middle
                {
                    rotationQuaternion = new Vector3(0, 0, 90);    // make vector2.up point left
                }
                else    // if coming from left middle
                {
                    rotationQuaternion = new Vector3(0, 0, 270);    // make vector2.up point right   
                }
            }
        }
        else if (type == 0 || type == 2)     // if spawning a triangle or a pentagon
        {
            if (position.y == spawnPos) // if coming from top
            {
                rotationQuaternion = new Vector3(0, 0, Random.Range(170, 190)); // make vector2.up point to a random direction pointing down
            }
            else if (position.y == -spawnPos)     // if coming from bottom
            {
                rotationQuaternion = new Vector3(0, 0, Random.Range(-10, 10)); // make vector2.up point to a random direction pointing up
            }
            else if (position.x == spawnPos)  // if coming from right
            {
                rotationQuaternion = new Vector3(0, 0, Random.Range(80, 100)); // make vector2.up point to a random direction pointing left
            }
            else if (position.x == -spawnPos)     // if coming from left
            {
                rotationQuaternion = new Vector3(0, 0, Random.Range(260, 280)); // make vector2.up point to a random direction pointing right
            }
            else
            {
                Debug.LogError("triangle or pentagon position wrong in CalculateRotation");
            }

        }
        else if (type == 3) // if spawning a hexagon, rotate it to face player position
        {
            float angle = Mathf.Atan2(_playerPos.position.y - position.y, _playerPos.position.x - position.x) * Mathf.Rad2Deg;
            rotationQuaternion = new Vector3(0, 0, angle);
        }
        else
        {
            Debug.LogError("type wrong in CalculateRotation");
            
        }

        return rotationQuaternion;
    }

    void SpawnDangerSign(Vector3 position)
    {
        float spawnPos = Camera.main.orthographicSize;

        if (position.x > spawnPos)  // if on the right, move inside screen
        {
            position.x = Camera.main.orthographicSize - 0.25f;
        }
        else if (position.x < -spawnPos)     // if on the left, move inside screen
        {
            position.x = -Camera.main.orthographicSize + 0.25f;
        }
        else if (position.y > spawnPos) // if on the top, move inside screen
        {
            position.y = Camera.main.orthographicSize - 0.25f;
        }
        else if (position.y < -spawnPos) // if on the bottom, move inside screen
        {
            position.y = -Camera.main.orthographicSize + 0.25f;
        }
        position.z = 0;
        Instantiate(_dangerSign, position, Quaternion.identity, this.transform);
    }
}

/*
 Helper class that allow serialization of a List<List<float>
 This class countains a List<float>
 Each list should have 3 positions
 list[0] indicates which type of figure the pattern spawns
    0 - triangle
    1 - square
    2 - pentagon
    3 - hexagon
 list[1] indicates the amount of instances the pattern spawns
 list[2] indicates the amount of time between the spawning of each unit
 */
[System.Serializable]
public struct PatternList
{
    public List<float> pattern;
}