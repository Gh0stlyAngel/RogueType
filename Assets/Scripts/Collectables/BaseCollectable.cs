using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectable : MonoBehaviour
{
    protected CircleCollider2D circleCollider;

    [SerializeField] protected GameObject player;
    [SerializeField] private bool moveTowardPlayer = false;
    protected float speed;
    private float moveDirection;
    [SerializeField] private AudioClip collect;
    public float Weight;

    private Coroutine moveRoutine;

    public Vector2Int coordinate;
    private void Start() => CollectableStart();
    //private void Update() => CollectableUpdate();

    private void OnDestroy() => CollectableOnDestroy();

    

    protected virtual void CollectableStart()
    {
        /*circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = 0.0001f;
        circleCollider.isTrigger = true;
        //player = GameObject.FindWithTag("Player");*/
        speed = 4f;
        moveDirection = -1;

        coordinate = CollectableManager.Instance.RegisterCollectable(this);
    }

    protected virtual void CollectableUpdate()
    {
        /*if (moveTowardPlayer)
        {

        }*/ 

        Vector3 targetPosition = player.transform.position;
        Vector3 distance = (targetPosition - transform.position);
        //Vector3 radius = Random.insideUnitCircle.normalized;
        Debug.Log(distance);
        if (distance.sqrMagnitude < 1)
        {
            moveTowardPlayer = true;
            StartCoroutine(MoveTowardPlayer());
        }

    }

    IEnumerator MoveTowardPlayer()
    {
        while (true)
        {
            if (moveTowardPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime * moveDirection);

                if ((transform.position - player.transform.position).sqrMagnitude < 0.01f)
                {
                    moveTowardPlayer = false;
                    //SFXManager.instance.PlaySoundFXClipWithoutStopping(collect, gameObject.transform, 1);
                    SFXManager.instance.PlayCollectSoundFXClip(collect, gameObject.transform, 1);
                    Destroy(gameObject);
                    yield break;
                }

                moveDirection += (Time.deltaTime * 4);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return null;
            }
        }
        
        

    }

    public virtual void CollectableEffect(GameObject playerObject)
    {
        if (moveRoutine != null)
        {
            return;
        }
        player = playerObject;
        moveTowardPlayer = true;
        moveRoutine = StartCoroutine(MoveTowardPlayer());

    }

    protected virtual void CollectableOnDestroy()
    {

    }

}
