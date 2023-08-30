using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadScene : Unit
{
    [DoNotSerialize]
    public ControlInput InputTrigger;
    [DoNotSerialize]
    public ControlOutput OutputTrigger;

    [DoNotSerialize]
    public ValueInput SceneToLoad;

    protected override void Definition()
    {
        InputTrigger = ControlInput("", InternalBoot);
        OutputTrigger = ControlOutput("");
        SceneToLoad = ValueInput<string>("Scene To Load", "");
    }

    private ControlOutput InternalBoot(Flow arg)
    {
        Debug.Log("LoadScene: " + arg.GetValue<string>(SceneToLoad));
        TravelSystem.Instance.SceneLoad(arg.GetValue<string>(SceneToLoad));
        return OutputTrigger;
    }
}
