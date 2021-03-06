﻿using FormsGestures;

[assembly: Xamarin.Forms.Dependency(typeof(FormsGestures.Droid.GestureService))]
namespace FormsGestures.Droid
{
    /// <summary>
    /// FormsGesture.Droid Gesture service.
    /// </summary>
    public class GestureService : IGestureService
    {
        public GestureService()
        {
            Settings.Init();
        }


        #region IGestureService implementation

        public void For(Listener listener)
        {
            NativeGestureHandler.GetInstanceForListener(listener);
        }

        /*
        public void Cancel()
        {
            NativeGestureListener.Cancel();
        }
        */
        #endregion

    }
}

