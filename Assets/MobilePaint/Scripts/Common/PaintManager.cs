using UnityEngine;

namespace unitycoder_MobilePaint {

    public class PaintManager : MonoBehaviour {
        public MobilePaint mobilePaintReference;
       // public MobilePaint mobilePaintReference1;

        public static MobilePaint mobilePaint;

        void Awake()
        {
            if (mobilePaintReference == null) Debug.LogError("No MobilePaint assigned at " + transform.name, gameObject);
            mobilePaint = mobilePaintReference;
        }

        //public void Update()
        //{
        //    if (Input.GetKeyDown("a"))
        //    {
        //        mobilePaint = mobilePaintReference1;
        //        Debug.Log(mobilePaint);
        //    }
        //}
    }
}