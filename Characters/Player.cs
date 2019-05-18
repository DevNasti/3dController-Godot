using Godot;
using System;

public class Player : RigidBody
{
    private float moveSpeed = 5f;
    private float jumpForce = 5f;

    RayCast rayCast;

    Camera cameraRig;
    Spatial cameraPivot;

    private bool _jump;
    private bool _isGrounded;
    private float hz;
    private float vrt;
    private Transform playerTransform;
    private float rotationSpeed = 300;


    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        //cameraPivot = GetNode<Spatial>("Spatial_camera");
        //cameraRig = cameraPivot.GetChild<Camera>(0);
        _jump = false;

        //anim = GetNode<AnimationPlayer>("AnimationPlayer");
        //rayCast = GetNode<Camera>("Camera").GetChild<RayCast>(0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        hz = Input.GetActionStrength("right") - Input.GetActionStrength("left"); 
        vrt = Input.GetActionStrength("up") - Input.GetActionStrength("down");

        if(vrt != 0 || hz != 0)
            GD.Print($"{hz} - {vrt}");




        if (Input.IsActionJustPressed("Jump")) //&& _isGrounded)
        {
            _jump = true;
        }

        IsGrounded();

        //first i rotate 
        RotatePlayer(delta);

        //then i move in the direction the player is facing
        MovePlayer(delta);

        //i try to jump
        Jump();

        //    if (hz != 0 || vrt != 0)
        //    {
        //        AnimatePlayer();
        //    }
        //    else
        //    {
        //        animator.SetFloat("Forward", 0, .1f, Time.fixedDeltaTime);
        //    }
    }

    private void RotatePlayer(float fixedDeltaTime)
    {

        // i get the pad angle
        var angle = Mathf.Rad2Deg(Mathf.Atan2(hz, vrt));

        //i add it to the camera angle
        angle += cameraRig.Rotation.y;

        //i get correct angle with the rest of the division for 360
        angle %= 360;

        //i lerp it
        angle = Mathf.Lerp(angle, Rotation.y, fixedDeltaTime / rotationSpeed);

        //i rotate the character only if there is any axis input 
        if (hz != 0 || vrt != 0)
        {
            GD.PrintErr($"sto per ruotare a {angle}");
            // _isMovingHorizontally = true;
            RotateY(angle);
        }
        //else _isMovingHorizontally = false;

    }


    void MovePlayer(float deltaTime)
    {
        //if (hz != 0 || vrt != 0)
        //{
        //    //var moveVector = (transform.forward * moveSpeed * deltaTime);
        //    ////            rb.AddForce(moveVector, ForceMode.Impulse);
        //    //rb.MovePosition(transform.position + moveVector);
        //    rb.LinearVelocity = moveSpeed * deltaTime * Vector3.Forward;
        //}
        //else
        //{
        //    rb.LinearVelocity = new Vector3(0, rb.LinearVelocity.y, 0);
        //}
    }

    void Jump()
    {
        //JUMPING
        if (_jump)
        {
            ApplyImpulse(playerTransform.origin, Vector3.Up * jumpForce);
            //   animator.SetTrigger("Jump");
            _jump = false;
        }
    }
    private void IsGrounded()
    {
        var center = playerTransform.origin + Vector3.Up;

        //_isGrounded = (Physics.Raycast(center, Vector3.down, raycastDistanceForGround));
        _isGrounded = true;
    }

}



//void AnimatePlayer()
//{
//    //the animations are handled throught a blend three: higher is the speed (set with set float) closer the player would be to a running animation.
//    if (_isMovingHorizontally && _isGrounded)
//    {
//        animator.SetFloat("Forward", 1, .1f, Time.deltaTime);
//    }

//    else animator.SetFloat("Forward", 0, .1f, Time.deltaTime);
//}