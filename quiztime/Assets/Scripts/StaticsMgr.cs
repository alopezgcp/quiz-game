using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticsMgr {
    private static int score;
    public static int GetScore() { return score; }
    public static void SetScore(int s) { score = s; }
    public static void UpdateScore(int k) { score += k; }
}
