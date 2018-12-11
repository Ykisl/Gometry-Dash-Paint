using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLibrary : MonoBehaviour {
    public GameObject TestGameObj;
    public GameObject BushCursor;
    public GameObject bush01;

    void Start () {
	}

    public GameObject SetGameObj(int id)
    {
        return GameObjLib(id);
    }

    public GameObject GameObjLib(int id)
    {
        GameObject LibSet;
        switch (id)
        {
              case -1:
                LibSet = TestGameObj;
                break;
            case -2:
                LibSet = BushCursor;
                break;
            case 1887:
                LibSet = bush01;
                break;
            default:
                LibSet = null;
                break;
        }
        return LibSet;
    }
}
