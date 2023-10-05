package com.ramosmarin.mylibrary;

public class RamosMarinLogger
{
    static final String LOGTAG = "RMLog";

    static RamosMarinLogger _instance = null;
    public static RamosMarinLogger getInstance()
    {
        if (_instance == null)
            _instance = new RamosMarinLogger();
        return _instance;
    }

    public String getLogTag()
    {
        return LOGTAG;
    }
}
