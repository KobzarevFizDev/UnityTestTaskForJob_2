using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ResourceSelectButton : BaseActionButton
{
    public event Action<ResourceId> ResourceSelected;

    [field: SerializeField] public ResourceId ResourceId { private set; get; }
    public override void InvokeClickEvent()
    {
        ResourceSelected?.Invoke(ResourceId);
    }
}
