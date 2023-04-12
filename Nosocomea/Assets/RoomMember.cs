using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMember : MonoBehaviour
{
    public NewRoom memberRoom;

    private void OnDestroy()
    {
        memberRoom.currentEnemies.Remove(transform.parent.gameObject);
    }
}
