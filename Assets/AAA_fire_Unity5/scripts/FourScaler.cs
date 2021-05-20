using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class FourScaler : MonoBehaviour {
	#if UNITY_EDITOR
	private float[] PS_parameters = new float[10];

	SerializedObject so; 
	ParticleSystem[] allChildren;
	public Vector3 Base_scale;
	public int counter = 0;
	public float scale=1f;
	public float scale_old=1f;
	public bool have_data = false;
	// Use this for initialization
	void Start () {

		GetChildren();
        scale = 4f;
	}

	void GetPSparameters(ParticleSystem _PS){


	
		PS_parameters[0] = _PS.startSize/scale_old;
		PS_parameters[1] = _PS.startSpeed/scale_old;
		PS_parameters[2] = so.FindProperty("ForceModule.y.scalar").floatValue/scale_old ;
		PS_parameters[3] = so.FindProperty("ForceModule.x.scalar").floatValue/scale_old ;
		PS_parameters[4] = so.FindProperty("ForceModule.z.scalar").floatValue/scale_old ;
		Base_scale = this.transform.localScale/scale_old; 
	}

	void GetChildren(){
		allChildren = GetComponentsInChildren<ParticleSystem>();

		if (!have_data){
		
			foreach (ParticleSystem child in allChildren) {
				so = new SerializedObject(child);
					GetPSparameters(child);
					
				
				Scaling(child);
			}
			//have_data = true;
		//}else{
		//	foreach (ParticleSystem child in allChildren) {
		//		so = new SerializedObject(child);
		//		Scaling(child);
			//}
		}

	}
	
	void Scaling(ParticleSystem _PS){

		so.FindProperty("ForceModule.y.scalar").floatValue = PS_parameters[2]*scale;
		so.FindProperty("ForceModule.x.scalar").floatValue = PS_parameters[3]*scale;
		so.FindProperty("ForceModule.z.scalar").floatValue = PS_parameters[4]*scale;
		so.ApplyModifiedProperties();
		_PS.startSize = PS_parameters[0]*scale;
		_PS.startSpeed = PS_parameters[1]*scale;
		this.transform.localScale = new Vector3(scale,scale,scale); 
	
}

	void Update(){

		if (scale !=scale_old){

			GetChildren();
			scale_old = scale;
		}

	}
#endif
}
