using AEAssist;

namespace AE_ACR.utils;

internal class AELoggerUtils
{
    public static AeLogger _AeLogger = new AeLogger();

    public static void init()
    {
        AeLogger.WritePluginLog = true;
    }
}