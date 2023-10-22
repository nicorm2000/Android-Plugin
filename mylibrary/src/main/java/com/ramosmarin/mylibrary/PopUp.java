package com.ramosmarin.mylibrary;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.util.Log;

public class PopUp
{
    public static final PopUp _instance = new PopUp();

    public static PopUp GetInstance()
    {
        return _instance;
    }

    public static Activity mainAct;

    public static final String LOGTAG = "Hello User";

    public interface AlertViewCallBack
    {
        public void OnButtonTap(int id);
    }

    public void ShowAlertView(String[] stringArray, final AlertViewCallBack callBack)
    {
        if (stringArray.length < 3)
        {
            Log.i(LOGTAG, "Error - expected at least 3 strings, got " + stringArray.length);
        }

        DialogInterface.OnClickListener MyClickListener = new DialogInterface.OnClickListener()
        {
            @Override
            public void onClick(DialogInterface dialog, int id)
            {
                dialog.dismiss();
                Log.i(LOGTAG, "Tapped: " + id);
                callBack.OnButtonTap(id);
            }
        };

        AlertDialog alertDialog = new AlertDialog.Builder(mainAct).setTitle(stringArray[0]).setMessage(stringArray[1]).setCancelable(false).create();

        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, stringArray[2], MyClickListener);

        if (stringArray.length > 3)
        {
            alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, stringArray[3], MyClickListener);
        }

        if (stringArray.length > 4)
        {
            alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, stringArray[4], MyClickListener);
        }

        alertDialog.show();
    }
}
