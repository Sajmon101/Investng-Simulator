using System;
using System.Collections.Generic;

[Serializable]
public class RumorEffect
{
    public int companyId;
    public string companyName;
    public string text;
    public float change;
}

[Serializable]
public class Rumor
{
    public string rumor;
    public List<RumorEffect> effects;

}