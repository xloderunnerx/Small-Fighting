using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public interface IUnitFactory
    {
        BaseUnit Create(BaseUnit baseUnit);
    }
}