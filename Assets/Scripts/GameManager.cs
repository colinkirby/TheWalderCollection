using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetCursorVisible(false);
    }
    public void SetCursorVisible(bool vis) {
        Cursor.visible = vis;
    }

}
