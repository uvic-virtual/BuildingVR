using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : MonoBehaviour {
    //Customizable variables
    public float ChargeTime = 0.5f;
    public float OverChargeTime = 6f;
    public float CooldownTime = 4f;
    public float SwingThreshold = 0.45f;
    public float TiltOffset = 0.7f;
    public float ProjectileSpeed = 500f;

    //public Image array;
    public GameObject array;
    //temporary displays, replace with SFX later
    public Text Display;

    public GameObject FireBallProjectile;

    private Transform Controller;

    private bool Charged;
    private bool OverCharged;
    private bool CoolDown;

    private float ChargeTimer;
    private float CoolDownTimer;

    private Vector3 LastLocation;

    // Use this for initialization
    void Start () {
        Controller = GameObject.Find("LeftHand").transform;
        Charged = false;
        OverCharged = false;
        CoolDown = false;
        ChargeTimer = 0f;
        CoolDownTimer = 0f;
        Display.text = "Fire Ball Status: Not activated";
    }

    // Update is called once per frame
    void Update () {
        //Check FireBallControl only when charged to reduce drag from getspeed
        if (Charged)
        {
            FireBallControl();
        }
        ChargeControl();
    }

    private void FireBallControl()
    {
        //If the fire ball is charged and the player swings the controller
        if (Swing())
        {
            //Reset variables
            Charged = false;
            OverCharged = false;
            ChargeTimer = 0f;
            //Start cooldown
            CoolDown = true;
            CoolDownTimer = CooldownTime;
            Display.text = "Fire Ball Status: Fired!";
            StartCoroutine(ShootFireBall());
        }
    }

    //Fire a projectile
    private IEnumerator ShootFireBall()
    {
        //TODO:
        //array SFX
        GameObject arrayClone = Instantiate(array, Controller.transform);

        //Quaternion arrayRotation = Controller.transform.rotation;
        while (arrayClone.transform.localEulerAngles.z < 180)
        {
            arrayClone.transform.Rotate(0f, 0f, 2f);
            yield return new WaitForFixedUpdate();
        }
        Destroy(arrayClone);
        //Clone a projectile object
        GameObject Clone = Instantiate(FireBallProjectile, Controller.transform.position, Quaternion.identity) as GameObject;
        //resize the projectile according to the charge timer
        Clone.transform.localScale *= (ChargeTimer - ChargeTime);
        Rigidbody Rigi = Clone.GetComponent<Rigidbody>();
        //Shoot in the direction the controller is facing
        Vector3 shootDirection = Quaternion.AngleAxis(5, Vector3.up) * Controller.transform.forward;
        Rigi.AddForce(shootDirection * ProjectileSpeed);
        Destroy(Clone.gameObject, 6);
    }

    //return if the fireball is charged
    private void ChargeControl()
    {
        //When cooling down, the fire ball will not charge, but it won't reset the timer
        if (CoolDown)
        {
            //Give .5 seconds for the firing effect/display
            if (CoolDownTimer<2.5)
                Display.text = "Fire Ball Status: Cooling Down";
            CoolDownTimer -= Time.deltaTime;
            if(CoolDownTimer <= 0)
            {
                CoolDown = false;
            }
            //Nothing else is enabled while cooling down
            return;
        }
        //Conditions for the fireball to charge
        if ((Flipped() || Charged) && !OverCharged)
        {
            ChargeTimer += Time.deltaTime;
            if (ChargeTimer < ChargeTime)
            {
                Display.text = "Fire Ball Status: Charging";
                return;
            }
            if ((ChargeTimer > ChargeTime) && (ChargeTimer < OverChargeTime))
            {
                Display.text = "Fire Ball Status: Charged";
                Charged = true;
                return;
            }
            if (ChargeTimer > OverChargeTime)
            {
                Display.text = "Fire Ball Status: Over Charged";
                Charged = false;
                OverCharged = true;
                return;
            }
        }
        //If the player is not flipping(charing) the controller, reset everything
        if (!Flipped())
        {
            ChargeTimer = 0f;
            Charged = false;
            OverCharged = false;
            Display.text = "Fire Ball Status: Resting";
            return;
        }
    }

    //Check if the player is swinging
    private bool Swing()
    {
        if (GetSpeed() > SwingThreshold)
        {
            return true;
        }
        return false;
    }

    //Update Controller speed
    private float GetSpeed()
    {
        //Because the way the GetSpeed function is called, this if loop make sure the last location is updated correctly
        if(ChargeTimer < ChargeTime + 0.1f)
        {
            LastLocation = Controller.transform.position;
            return 0;
        }
        float Speed = (Controller.transform.position - LastLocation).magnitude/Time.deltaTime;
        LastLocation = Controller.transform.position;
        return Speed;
    }

    //find the absolute value
    private float Abs(float x)
    {
        if (x < 0)
        {
            return x *= -1;
        }
        return x;
    }

    //Return if the controller is flipped or not
    private bool Flipped()
    {
        //Check flipping angle
        Quaternion Rotation = Controller.transform.rotation;        
        float Angle = Abs(Rotation.x);
        if(Abs(Rotation.z) > Angle)
        {
            Angle = Abs(Rotation.z);
        }

        //Tile from 0 to 1, bigger tile = 'more' upside down
        if(Angle > TiltOffset)
        {
            return true;
        }
        return false;
    }
}
