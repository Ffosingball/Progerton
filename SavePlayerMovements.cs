using UnityEngine;

public class SavePlayerMovements : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftShift) )
            Debug.Log("xyz: "+player.position);
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftShift) )
            Debug.Log("xyz: "+player.position);
    }
}
