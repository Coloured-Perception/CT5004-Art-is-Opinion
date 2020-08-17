using UnityEngine;

public class MaterialScript : MonoBehaviour {
	public GameObject PCanvas;
	public GameObject Painting;

	Material PMaterial;
	Material CMaterial;

	public void ChangeM() {
		PMaterial = PCanvas.GetComponent<Renderer>().material;
		CMaterial = Painting.GetComponent<Renderer>().material;

		PCanvas.GetComponent<Renderer>().material = CMaterial;
		Painting.GetComponent<Renderer>().material = PMaterial;
	}
}