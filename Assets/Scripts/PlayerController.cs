using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public bool cantChange = false;
    public Ramp currentRamp;
    public bool isDead = false;
    public Animator animator;
    public GroundCheck gc;
    public float leftSideLimit;
    public float rightSideLimit;
    private bool facingRight = true;
    private bool didJump = false;
    private bool didWallJump = false;
    public bool onLadder;
    public bool attachedToLadder;
    public float originalXMoveForce = 100;
    public float climbSpeed = 3f;
    public float moveForce = 100;
    public float iceMoveForce = 50f;
    public float maxHorizontalSpeed = 11f;
    public float jumpForce = 1050f;
    public float wallJumpLean = 600;
    public float wallTouchRadius = 0.35f;
    public float groundCheckRadius = 0.3f;
    private float gravityScale;
    public bool grounded = false;
    public bool touchingWall = false;
    private bool isWallJumping = false;
    private bool inputDoesntMatter = false;
    private bool isHalfWallJumping = false;
    public bool offTheIce = false;
    public bool onIce = false;
    public Transform groundCheck;
    public SpeedRamp sr;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsWall;
    public Transform ladderTransform;
    private Rigidbody2D rb;
    public Vector2 originalOffset;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -1, 0);
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        gc = GetComponentInChildren<GroundCheck>();
        originalOffset = GetComponent<PolygonCollider2D>().offset;
    }

    void Start()
    {
        transform.position = GlobalControl.Instance.playerLocation;
    }

    IEnumerator WaitToRevertOffset(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<PolygonCollider2D>().offset = originalOffset;
    }
    // INCREASE MAX SPEED FOR ICE
    void Update()
    {
        if(currentRamp!=null && Input.GetAxisRaw("Horizontal")==0 && !onIce && !offTheIce && !Input.GetKeyDown(KeyCode.Space) && !didJump)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        animator.SetBool("attachedToLadder", attachedToLadder);
        if (attachedToLadder)
        {
            if(climbSpeed == 0)
            {
                inputDoesntMatter = true;
                animator.SetFloat("Direction", 0);
            }
            else
            {
                animator.SetFloat("Direction", Mathf.Abs(Input.GetAxisRaw("Vertical")));
            }
            
        }
        if (!cantChange)
        {
            if (currentRamp != null)
            {
                transform.eulerAngles = new Vector3(0, 0, currentRamp.rotationAngle);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        if (!isDead)
        {
            if (touchingWall && !grounded && Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("wallSliding", true);
                GetComponent<PolygonCollider2D>().offset = new Vector2(originalOffset.x - 0.05f, originalOffset.y);
            }
            else
            {
                animator.SetBool("wallSliding", false);
            }
            if (onIce || offTheIce)
            {
                moveForce = iceMoveForce;
            }
            else
            {
                moveForce = originalXMoveForce;
            }
            if (touchingWall && !grounded && Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
            {
                didWallJump = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && grounded && !attachedToLadder)
            {
                didJump = true;
            }

            if (onLadder)
            {

                if ((Input.GetAxisRaw("Vertical") != 0 || attachedToLadder) && !isHalfWallJumping)
                {
                    moveForce = 0;
                    attachedToLadder = true;
                    rb.gravityScale = 0;
                }
                if (grounded && Input.GetAxis("Vertical") == -1)
                {
                    attachedToLadder = false;
                    animator.SetBool("attachedToLadder", attachedToLadder);
                }
                else if (attachedToLadder)
                {
                    animator.SetBool("jumped", false);
                    transform.position = new Vector2(ladderTransform.position.x, transform.position.y);
                    rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * climbSpeed);
                    if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
                    {
                        WallJump();
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                        {
                            animator.Play("Jump", 0, 0);
                        }
                    }
                }
            }
            else
            {
                attachedToLadder = false;
                rb.gravityScale = gravityScale;
            }

            if (transform.position.x < leftSideLimit && Input.GetAxisRaw("Horizontal") == -1)
            {
                transform.position = new Vector2(leftSideLimit, transform.position.y);
            }
            else if (transform.position.x > rightSideLimit && Input.GetAxisRaw("Horizontal") == 1)
            {
                transform.position = new Vector2(rightSideLimit, transform.position.y);
            }
        }
        Ray ray = new Ray();
        RaycastHit hit;
        Vector3 axis;
        float angle = 0;
        Debug.Log(angle);


        ray.origin = transform.position;
        ray.direction = -transform.up;

        Physics.Raycast(ray, out hit);

        axis = Vector3.Cross(-transform.up, -hit.normal);
        angle = Mathf.Atan2(Vector3.Magnitude(axis), Vector3.Dot(-transform.up, -hit.normal));
        transform.RotateAround(transform.position, axis, angle);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {

            if (onIce == true)
            {
                offTheIce = true;
            }
            else if (grounded) // on regular ground, this means that offTheIce will stay true from ice contact up untill reg ground contact
            {
                offTheIce = false;
            }

            onIce = false;
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (grounded)
            {
                animator.SetBool("grounded", true);
                animator.SetBool("jumped", false);
            }
            else
            {
                animator.SetBool("grounded", false);
            }
            if (inputDoesntMatter)
            {
                horizontalInput = 0;
            }
            if (didWallJump)
            {
                horizontalInput = 0f;
                WallJump();
            }

            else if (didJump)
            {
                Jump();
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    animator.Play("Jump", 0, 0);
                }
            }
            if (Input.GetAxisRaw("Horizontal") == 0 && !isWallJumping && !onIce && !offTheIce)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            if (horizontalInput * rb.velocity.x < maxHorizontalSpeed)
            {
                rb.AddForce(Vector2.right * horizontalInput * moveForce);
            }
            if (Mathf.Abs(rb.velocity.x) > maxHorizontalSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxHorizontalSpeed, rb.velocity.y);
            }

            if (touchingWall && !grounded)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            if (!inputDoesntMatter)
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
                {
                    Flip();
                }

                else if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
                {
                    Flip();
                }
            }
        }


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce));
        didJump = false;
        animator.SetBool("jumped", true);
    }

    void WallJump()
    {
        animator.SetBool("jumped", true);
        if (!attachedToLadder)
        {
            StartCoroutine(WaitToRevertOffset(1f));
        }
        float i;
        StartCoroutine(WaitUntilTraveled(1f));
        StartCoroutine(DontCareTimer(0.4f));
        StartCoroutine(WallJumpTimer());
        if (attachedToLadder)
        {
            i = Mathf.Sign(Input.GetAxisRaw("Horizontal")) * wallJumpLean;
            attachedToLadder = false;
        }
        else
        {
            i = Mathf.Sign(transform.localScale.x) * -wallJumpLean;
            Flip();
        }
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(new Vector2(i, jumpForce));
        didWallJump = false;

        }

    IEnumerator WallJumpTimer()
    {

        isWallJumping = true;
        while (!grounded)
        {
            yield return null;
        }
        isWallJumping = false;
        inputDoesntMatter = false;
    }

    IEnumerator WaitUntilTraveled(float distance)
    {
        isHalfWallJumping = true;
        float xPos = transform.position.x;
        while (Mathf.Abs(transform.position.x - xPos) < distance)
        {
            yield return null;
        }
        isHalfWallJumping = false;
    }

    IEnumerator DontCareTimer(float waitTime)
    {
        inputDoesntMatter = true;
        yield return new WaitForSeconds(waitTime);
        inputDoesntMatter = false;
    }
}

