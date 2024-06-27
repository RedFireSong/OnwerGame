using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : Singleton<GameRoot>
{
    readonly Dictionary<StateType, StateBase> stateController = new Dictionary<StateType, StateBase>();
    private StateType currentType = StateType.None;
    void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
