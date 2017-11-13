using UnityEngine;
using System.Collections;
[AddComponentMenu("Character Set Up/Mouse Look")]

public class MouseLook : MonoBehaviour
{

    #region Variables
    [Header("Rotational Axis")]
    public RotationalAxis axis = RotationalAxis.MouseX;
    [Header("sensitivity")]
    public float sensitivityX = 15f;
    public float sensitivityY = 15;
    [Header("Y Rotation Clamp")]
    public float minimumY = -60f;
    public float maximumY = 60f;
    private float rotationY = 0;
    #endregion
    #region Start
    void Start()
    {
        if (this.GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    #endregion
    #region Update
    void Update()
    {
        #region Mouse X and Y
        if (axis == RotationalAxis.MouseXAndY)
        {
            //float rotation x is equal to our y axis plus the mouse input on the Mouse X times our x sensitivity
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            //our rotation Y is pulse equals  our mouse input for Mouse Y times Y sensitivity
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            //the rotation Y is clamped using Mathf and we are clamping the y rotation to the Y min and Y max
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and x rotation on the y axis
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        #endregion
        #region Mouse X
        //else if we are rotating on the X
        else if (axis == RotationalAxis.MouseX)
        {
            //transform the rotation on our game objects Y by our Mouse input Mouse X times X sensitivity
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }

        #endregion
        #region Mouse Y
        //else we are only rotation on the Y
        else
        {
            //our rotation Y is pulse equals  our mouse input for Mouse Y times Y sensitivity
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            //the rotation Y is clamped using Mathf and we are clamping the y rotation to the Y min and Y max
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and local euler angle Y on the y axis
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

        #endregion
    }
    #endregion
}
#region RotationalAxis
/*
enums are what we call state value variables 
they are comma separated lists of identifiers
we can use them to create Type or Category variables
meaning each heading of the list is a type or category element that this can be
eg weapons in an inventory are a type of inventory item
if the item is a weapon we can equipt it
if it is a consumable we can drink it
it runs different code depending on what that objects enum is set to
you can also have subtypes within those types
eg weapons are an inventory category or type
we can then have ranged, melee weapons
or daggers, short swords, long swords, mace, axe, great axe, war axe and so on
each Type or Category holds different infomation the game needs like 
what animation plays, where the hands sit on the weapon, how many hands sit on the weapon and so on
*/
public enum RotationalAxis
{
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
}
//Creating an enum out side the script and making it public means it can be asessed anywhere in any script with out reference
#endregion











