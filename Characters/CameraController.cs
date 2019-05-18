using Godot;
using System;

public class CameraController : Spatial
{

    private float mouseSens = .5f;
    private float xRotation, yRotation;
    private float upAngle = 90;
    private float feetAngle = -70;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetAsToplevel(true);
    }


    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventMouseMotion eventMouseMotion)
        {
            yRotation = RotationDegrees.y - (mouseSens * eventMouseMotion.Relative.x);
            xRotation = RotationDegrees.x - (mouseSens * eventMouseMotion.Relative.y);

            if (xRotation > upAngle)
                xRotation = upAngle;

            if (xRotation < feetAngle)
                xRotation = feetAngle;

            RotationDegrees = new Vector3(xRotation, yRotation, RotationDegrees.z);
        }

    }

}
