using UnityEngine;

public class staticZ : MonoBehaviour
{
	public float fixedRotation = 0;
 	void Update ()
	{
        //freezes the lens on the z axis so that the view doesn't rotate with the mag glass.
        
		Vector3 eulerAngles = transform.eulerAngles;
		transform.eulerAngles = new Vector3(eulerAngles.x , eulerAngles.y , fixedRotation);
	}
}