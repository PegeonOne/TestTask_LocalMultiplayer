using UnityEngine;

[CreateAssetMenu(fileName = "AppConfig", menuName = "Configs/AppConfig")]
public class AppConfig : ScriptableObject
{
    [Header("Move Settings")]
    public float MouseSensitivity;
    public float MoveSpeed;
    [Space]
    [Header("Spawn Settings")]
    //x its min value, y its max value
    public Vector2 SpawnBoxArea;
    public int MaxEnemyCount;
}
