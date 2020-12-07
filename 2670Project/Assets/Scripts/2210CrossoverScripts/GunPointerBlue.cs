using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointerBlue : MonoBehaviour
{
  private PlayerControls controls;
  private Vector2 blueMovement;
   private void Awake()
  {
    controls = new PlayerControls();
    controls.Gameplay.MoveBlue.performed += context => blueMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveBlue.canceled += context => blueMovement = Vector2.zero;

  }

  public void Update()
  {
    var input = new Vector3(blueMovement.x, 0, blueMovement.y);
    if(input != Vector3.zero)
    {
      transform.forward = input;
    }
  }
}
