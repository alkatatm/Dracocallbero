#pragma strict

var bodyMaterial		: ProceduralMaterial;				// The Material assigned to the body
var undersideMaterial	: ProceduralMaterial;				// The Material assigned to the underside
var wingMaterial		: ProceduralMaterial;				// The Material assigned to the wing

var loadingPanel		: GameObject;						// Canvas Panel that shows Loading Message

function Start () {

}

function Update () {

}

function CheckLoading(){
	print ("checkLoading: " + bodyMaterial.isProcessing);
	if (!bodyMaterial.isProcessing)
	{
		loadingPanel.SetActive(false);
		CancelInvoke("CheckLoading");
	}
}

function BodyHue(newValue : float){
	print ("BodyHue: " + newValue);
	bodyMaterial.SetProceduralFloat("BodyHue", newValue);
}

function BodySaturation(newValue : float){
	bodyMaterial.SetProceduralFloat("BodySaturation", newValue);
}

function BodyLightness(newValue : float){
	bodyMaterial.SetProceduralFloat("BodyLightness", newValue);
}

function BodyContrast(newValue : float){
	bodyMaterial.SetProceduralFloat("BodyContrast", newValue);
}

function WingHue(newValue : float){
	bodyMaterial.SetProceduralFloat("WingHue", newValue);
}

function WingSaturation(newValue : float){
	bodyMaterial.SetProceduralFloat("WingSaturation", newValue);
}

function WingLightness(newValue : float){
	bodyMaterial.SetProceduralFloat("WingLightness", newValue);
}

function WingContrast(newValue : float){
	bodyMaterial.SetProceduralFloat("WingContrast", newValue);
}

function UndersideHue(newValue : float){
	bodyMaterial.SetProceduralFloat("UndersideHue", newValue);
}

function UndersideSaturation(newValue : float){
	bodyMaterial.SetProceduralFloat("UndersideSaturation", newValue);
}

function UndersideLightness(newValue : float){
	bodyMaterial.SetProceduralFloat("UndersideLightness", newValue);
}

function UndersideContrast(newValue : float){
	bodyMaterial.SetProceduralFloat("UndersideContrast", newValue);
}

function RebuildTexturesBody(){
	print ("Rebuild Textures Body");
	bodyMaterial.RebuildTextures();
	print ("checkLoading: " + bodyMaterial.isProcessing);
	//loadingPanel.SetActive(true);
	//InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesWing(){
	wingMaterial.RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesUnderside(){
	undersideMaterial.RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}