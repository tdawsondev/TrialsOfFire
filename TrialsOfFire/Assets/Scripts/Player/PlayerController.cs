using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private CharacterController mageCharacter;

    [SerializeField]
    private Camera eyes;

    [SerializeField]
    private float speed;
    public bool movementDisabled = false;
    private bool canDash;
    public float dashTime;
    public float dashSpeed;
    public float dashDelay = 0.5f;
    private Vector3 moveDirection;
    public Vector2 input;
    public float smooth;
    float _smoothValx, _smoothValZ; // used in input smoothing
    public float jumpHeight = 10f;
    public float gravity = -9.81f;
    private Vector3 playerVelocity;
    private bool grounded;

    [Header("Audio")]
    KeyValuePair<AK.Wwise.Event, GameObject> footsteps;
    bool footstepsIsPlaying;
    public float footstepsTime;
    Coroutine footstepsCall;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        grounded = true;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Is Grounded: " + mageCharacter.isGrounded);
        grounded = mageCharacter.isGrounded;
        Vector2 smoothedInput = SmoothedInput();
        input = smoothedInput;
        float horizontal = smoothedInput.x;
        float vertical = smoothedInput.y;
        float mouse_x = Input.GetAxis("Mouse X");
		float mouse_y = Input.GetAxis("Mouse Y");

		Vector3 move = transform.forward * vertical * Player.Instance.character.Speed.Value + transform.right * horizontal * Player.Instance.character.Speed.Value;

        move.y = 0f;
        moveDirection = move;

        mageCharacter.Move(move  * Time.deltaTime);

        //Jumps
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        mageCharacter.Move(playerVelocity *Time.deltaTime);


        CheckPlaySound();

        //camera
        if (!movementDisabled)
        {
            transform.Rotate(0f, mouse_x, 0f);
            eyes.transform.Rotate(-mouse_y, 0f, 0f);
        }

        //dash

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
            StartCoroutine(ReloadDash());
        }
    }

    public Vector2 SmoothedInput()
    {
        float dead = 0.001f;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        float rX, ry;

        //Horizontal
        float targetx = input.x;
        _smoothValx = Mathf.MoveTowards(_smoothValx, targetx, smooth * Time.unscaledDeltaTime);
        rX = (Mathf.Abs(_smoothValx) < dead) ? 0f : _smoothValx;

        //Vertical
        float targety = input.y;
        _smoothValZ = Mathf.MoveTowards(_smoothValZ, targety, smooth * Time.unscaledDeltaTime);
        ry = (Mathf.Abs(_smoothValZ) < dead) ? 0f : _smoothValZ;

        return new Vector2(rX, ry);
    }

    IEnumerator ReloadDash(){
        canDash = false;
        yield return new WaitForSeconds(dashDelay);
        canDash = true;

    }
    IEnumerator Dash()
    {
        float timer = 0f;
        while (timer < dashTime)
        {
            mageCharacter.Move(moveDirection.normalized * dashSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    void CheckPlaySound()
    {
        if(input.magnitude > 0f && grounded)
        {
            if (!footstepsIsPlaying)
            {
                footstepsIsPlaying = true;
                footstepsCall = StartCoroutine(FootStepLoop());
            }
            
        }
        else if(footstepsIsPlaying)
        {
            StopCoroutine(footstepsCall);
            footstepsIsPlaying = false;
        }
    }
    IEnumerator FootStepLoop()
    {
        //Debug.Log("Start");
        while (true)
        {
            if (!mageCharacter.isGrounded)
            {
                yield return new WaitForEndOfFrame();
            }
            //Debug.Log("Play");
            AudioManager.instance.Play("Foot Steps");
            yield return new WaitForSeconds(footstepsTime);
        }
        
    }

    public void FreezeCamera()
    {
        movementDisabled = true;
    }
    public void UnFreezeCamera()
    {
        movementDisabled = false;
    }
}
