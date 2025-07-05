using UnityEngine;

public interface IRandomEvent
{
    void Apply();
    string GetAlertDescription();
    float probability { get; }
    int roundNr { get; set; }
}
