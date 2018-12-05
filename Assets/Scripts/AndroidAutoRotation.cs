using UnityEngine;

public class AndroidAutoRotation : MonoBehaviour
{
    void OnApplicationFocus(bool haveFocus)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (haveFocus) ToggleAutoRotation();
#endif
    }

    private static void ToggleAutoRotation()
    {
        var autoRotationOn = DeviceAutoRotationIsOn();
        Screen.autorotateToPortrait = autoRotationOn;
        Screen.autorotateToLandscapeLeft = autoRotationOn;
        Screen.autorotateToLandscapeRight = autoRotationOn;

        Screen.orientation = autoRotationOn
            ? ScreenOrientation.AutoRotation
            : ScreenOrientation.Portrait;
    }

    private static bool DeviceAutoRotationIsOn()
    {
        using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var context = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            var systemGlobal = new AndroidJavaClass("android.provider.Settings$System");
            var rotationOn = systemGlobal.CallStatic<int>("getInt",
                context.Call<AndroidJavaObject>("getContentResolver"), "accelerometer_rotation");

            return rotationOn == 1;
        }
    }
}
