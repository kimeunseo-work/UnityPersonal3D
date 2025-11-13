using System.Collections.Generic;
using UnityEngine;

public static class WaitForSecondsCash
{
    public static Dictionary<float, WaitForSeconds> WaitForSeconds { get; set; } = new(10);

    public static WaitForSeconds GetWaitForSeconds(float seconds)
    {
        if (!WaitForSeconds.ContainsKey(seconds))
        {
            var temp = new WaitForSeconds(seconds);
            WaitForSeconds.Add(seconds, temp);

            return temp;
        }

        return WaitForSeconds[seconds];
    }
}