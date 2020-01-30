using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RotateCamera : ScriptableObject
{
    public enum Wall { wallOne, wallTwo, wallThree };

    public Wall currentWall;
    [Tooltip("How much to rotate left by. ")]
    public float rotateLeft;
    public RotateCamera leftWall;
    public float rotateRight;
    public RotateCamera rightWall;

}
