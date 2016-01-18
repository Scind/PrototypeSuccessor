using UnityEngine;
using System;
using Spellsystem;

public interface IVisual
{
    void KillVisuals(float delay, bool stopBefore);
    void SetParentTransform(Transform transform);
    void AuthorizeCollisionDetection(ref SpellInformation spellInformation);
}

