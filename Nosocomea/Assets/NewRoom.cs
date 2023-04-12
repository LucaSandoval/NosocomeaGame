using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoom : MonoBehaviour
{
    public int y, x;
    public NewRoomType type;
    //Rooms are 18x18 tiles

    [Header("Materials")]
    public Material solidMat;
    public Material transMat;
    [Header("Objects")]
    //0 top, 1 left, 2, right, 3, bottom
    public GameObject[] doors;
    public MeshRenderer[] walls;

    [Header("Neighbor Info Info")]
    [SerializeField] private bool topLocked;
    [SerializeField] private bool leftLocked;
    [SerializeField] private bool rightLocked;
    [SerializeField] private bool bottomLocked;

    private bool topLockMem;
    private bool leftLockMem;
    private bool rightLockMem;
    private bool bottomLockMem;

    [Space(10)]
    [SerializeField] private bool topVisible;
    [SerializeField] private bool leftVisible;
    [SerializeField] private bool rightVisible;
    [SerializeField] private bool bottomVisible;

    [Space(15)]
    public bool playerEntered;
    private bool roomLocked;
    private bool tryingToUnlock;

    private float enteredDistance = 6f;

    private GameObject player;
    private IsoCamera isoCamera;
    [HideInInspector]
    public bool playerInRoom;

    [Header("Layouts")]
    public GameObject[] enemyLayouts;
    public GameObject[] lootLayouts;


    public List<GameObject> currentEnemies;

    private void Awake()
    {
        isoCamera = Camera.main.GetComponent<IsoCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerEntered = false;
        roomLocked = false;
        tryingToUnlock = false;
    }

    public void Start()
    {
        currentEnemies = new List<GameObject>();
        PickRoomLayout();
    }

    public void Lock()
    {
        roomLocked = true;

        topLocked = true;
        leftLocked = true;
        rightLocked = true;
        bottomLocked = true;
        isoCamera.target = transform;

        StartCoroutine(lockDelay());
    }

    private IEnumerator lockDelay()
    {
        yield return new WaitForSecondsRealtime(2);
        tryingToUnlock = true;
    }

    public void UnLock()
    {
        isoCamera.ResumeControl();
        roomLocked = false;
        tryingToUnlock = false;
        SetLockInfo(topLockMem, leftLockMem, rightLockMem, bottomLockMem);
    }

    private void Update()
    {

        UpdateVisuals();

        if (playerEntered == false)
        {           
            if (Vector3.Distance(player.transform.position, transform.position) <= enteredDistance)
            {
                playerEntered = true;                
                Lock();
            }
        } 

        if (tryingToUnlock)
        {
            //Conditions to unlock room
            if (currentEnemies.Count == 0)
            {
                UnLock();
            }            
        }

        playerInRoom = Vector3.Distance(player.transform.position, transform.position) <= enteredDistance;
    }

    private void UpdateVisuals()
    {
        doors[0].SetActive(topLocked);
        doors[1].SetActive(leftLocked);
        doors[2].SetActive(rightLocked);
        doors[3].SetActive(bottomLocked);

        walls[0].material = getMatFromBool(topVisible);
        walls[1].material = getMatFromBool(leftVisible);
        walls[2].material = getMatFromBool(rightVisible);
        walls[3].material = getMatFromBool(bottomVisible);
    }

    private Material getMatFromBool(bool boolean)
    {
        if (boolean)
        {
            return solidMat;
        }

        return transMat;
    }

    public void SetLockInfo(bool top, bool left, bool right, bool bottom)
    {
        topLockMem = top;
        leftLockMem = left;
        rightLockMem = right;
        bottomLockMem = bottom;

        topLocked = topLockMem;
        leftLocked = leftLockMem;
        rightLocked = rightLockMem;
        bottomLocked = bottomLockMem;
    }

    public void SetVisibilityInfo(bool top, bool left, bool right, bool bottom)
    {
        topVisible = top;
        leftVisible = left;
        rightVisible = right;
        bottomVisible = bottom;
    }

    private void PickRoomLayout()
    {
        int rand = 0;
        switch(type)
        {
            case NewRoomType.enemyRoom:
                rand = Random.Range(0, enemyLayouts.Length);
                enemyLayouts[rand].SetActive(true);
                break;
            case NewRoomType.lootRoom:
                rand = Random.Range(0, lootLayouts.Length);
                lootLayouts[rand].SetActive(true);
                break;
        }
    }
}

[System.Serializable]
public enum NewRoomType
{
    startRoom,
    lootRoom,
    enemyRoom,
    endRoom
}
