/// <summary>
/// write by 52cwalk,if you have some question ,please contract lycwalk@gmail.com
/// </summary>
/// 
/// 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class QREncodeTest : MonoBehaviour {
	public QRCodeEncodeController e_qrController;
	public RawImage qrCodeImage;
	public InputField m_inputfield;
    int i = 1;
	// Use this for initialization
	void Start () {
		if (e_qrController != null) {
			e_qrController.e_QREncodeFinished += qrEncodeFinished;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void qrEncodeFinished(Texture2D tex)
	{
       
		if (tex != null && tex != null) {
            i++;
            byte[] textureArray = tex.EncodeToPNG();
            string path = Application.dataPath + "/TexturesFol";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllBytes(path+"/"+i+".Png",textureArray);
		} else {

		}
	}

	public void Encode()
	{
		if (e_qrController != null) {
			string valueStr = m_inputfield.text;
			e_qrController.Encode(valueStr);
		}
	}

	public void ClearCode()
	{
		qrCodeImage.texture = null;
		m_inputfield.text = "";
	}

}
