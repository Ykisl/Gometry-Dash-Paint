using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindLibrary : MonoBehaviour {

    public BindingBlockID binding;

    void LoadBlocks()
    {
        binding = new BindingBlockID();
        binding.Bind(1887, typeof(Bush01));
    }

    public List<int> GetFilter()
    {
        List<int> filter = new List<int>();
        filter.Add(1887);
        return filter;
    }
    public BindingBlockID GetBindingLB()
    {
        LoadBlocks();
        return binding;
    }
}

class Bush01 : DetailBlock
{
    public override Layer Default_ZLayer { get; protected set; } = Layer.B2;
    public override short Default_ZOrder { get; protected set; } = 9;

    public Bush01() : base(1887)
    {
    }

    public Bush01(string[] data) : base(data)
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

