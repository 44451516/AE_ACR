using AEAssist;

namespace AE_ACR.utils;

internal class AELoggerUtils
{
    public static AeLogger _AeLogger = new();

    public static void init()
    {
        AeLogger.WritePluginLog = true;
    }


    public static void Log(string text)
    {
        var isOutLog = false;
#if DEBUG
        isOutLog = true;
#endif

        if (isOutLog) _AeLogger.LogError(text);
    }
}