using UnityEngine;

public static class GlobalValues
{
    public static int level;
    public static int lives;

    [RuntimeInitializeOnLoadMethod]
    static void OnGameStart()
    {
        level = 1;
        lives = 3;
        Debug.Log("Game started, global values initialized");
    }
}
