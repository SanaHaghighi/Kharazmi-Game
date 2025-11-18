using DG.Tweening;
using SFXSystem;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    public FixedJoystick joystick;
    public float threshold = 0.5f;
    [SerializeField] SpriteRenderer Head;
    [SerializeField] List<TailBehaviour> currentSegments;
    [SerializeField] TailBehaviour SegmentPrefab;
    public float MoveSpeed = 5f;
    public int InitialSize = 4;
    Vector2 direction;
    Vector2 prevDirection;
    public List<TailBehaviour> segments;
    bool canMove;
    [HideInInspector]
    public bool IsMoving;
    public string KeyString;
    int score;
    Tween rotationTween;
    [HideInInspector]
    public bool HasLost;

    
    
    public void Init(string key)
    {
        score = 0;
        SetString(key);
        canMove = true;
        segments = new List<TailBehaviour>();
        foreach(var tail in currentSegments)
        {
            segments.Add(tail);
            tail.Init();
        }
        for (int i = 1; i < InitialSize; i++)
        {
            Grow();
        }
        PlayerEdgeMovement.Instance.Init();
    }

    void Update()
    {
        if (!canMove) return;
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
/*
        // Check for upward movement
        if (vertical > threshold && prevDirection != Vector2.down)
        {
            direction = Vector2.up;
            prevDirection = direction;
            IsMoving = true;
        }
        // Check for downward movement
        else if (vertical < -threshold && prevDirection != Vector2.up)
        {
            direction = Vector2.down;
            prevDirection = direction;
            IsMoving = true;
        }
        // Check for leftward movement
        else if (horizontal < -threshold && prevDirection != Vector2.right)
        {
            direction = Vector2.left;
            prevDirection = direction;
            IsMoving = true;
        }
        // Check for rightward movement
        else if (horizontal > threshold && prevDirection != Vector2.left)
        {
            direction = Vector2.right;
            prevDirection = direction;
            IsMoving = true;
        }
        else
        {
            direction = Vector2.zero;
            IsMoving = false;
        }
*/
        direction = new Vector3(horizontal, vertical, 0);
        if (direction.magnitude > threshold)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
            direction = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        if (!IsMoving) return;
        for (int i = segments.Count - 1; i > 0; i--)
        {
            if (segments[i - 1].positionHistory.Count > 0)
            {
                var targetPos= segments[i - 1].positionHistory.Peek();
                Vector3 dir = targetPos - segments[i].transform.position;
                segments[i].transform.position = targetPos;
                LookToDirection(segments[i].transform, dir);
            }
        }
        LookToDirection(transform, direction);
        transform.position = new Vector3(
            transform.position.x + direction.x * MoveSpeed * Time.fixedDeltaTime,
            transform.position.y + direction.y * MoveSpeed * Time.fixedDeltaTime,
            0.0f
        );
    }
    void LookToDirection(Transform obj,Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
        rotationTween = obj.transform.DORotateQuaternion(rotation, .25f);
    }
    public void Grow(string _preText="",string _keyText="")
    {
        var segment = Instantiate(SegmentPrefab);
        segment.Init();
        if (segment.positionHistory.Count > 0)
        {
            segment.transform.position = segments[segments.Count - 1].positionHistory.Peek();
        }
        else
        {
            segment.transform.position = segments[segments.Count - 1].transform.position;
        }
        segments.Add(segment);
        segment.SetText(_preText,_keyText);
    }
    void SetString(string s)
    {   
        currentSegments[1].SetText("",s);
        KeyString = s;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            var collectible = other.gameObject.GetComponent<CollectibleManager>();
            if (!collectible.KeyString.Equals(KeyString))
            {
                SoundSystemManager.Instance.PlaySFX("Wrong");
                SoundSystemManager.Instance.PlaySFX("Lost");
                StartCoroutine(GameOver());
                return;
            }
            SoundSystemManager.Instance.PlaySFX("Eating");
            Grow(collectible.GetPreText(), collectible.GetKeyText());
            score++;
            if (score >= 10)
            {
                StartCoroutine(GameOver(true));
                SoundSystemManager.Instance.PlaySFX("Win");
            }
            SpawnCollectibles.Instance.CollectibleEaten(collectible);
            Destroy(other.gameObject);
        }/*
        if (other.CompareTag("Tail"))
        {
            GameOver();
            return;
        }*/
    }
    public IEnumerator GameOver(bool button=false)
    {
        if (!HasLost)
        {
            HasLost = true;
            Debug.LogError("GAME OVER");
            Debug.Log("Score is : " + score);
            PlayerPrefs.SetInt("Level2", score);
            var kharazmi = score >= 10 ? 1 : 0;
            PlayerPrefs.SetInt("Level2KH", kharazmi);
            if (!button)
            {
                //Head.sprite = DatabaseHolder.Instance.LostHeadSprite;
                Head.transform.DOShakeScale(2f, 0.1f);
            }
            canMove = false;
            SoundSystemManager.Instance.StopBGM();
            SoundSystemManager.Instance.ChangeBGMVolumn(0);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene("Score 2");
        }
    }
}
