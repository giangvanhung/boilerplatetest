using kamrj.Debugging;

namespace kamrj
{
    public class kamrjConsts
    {
        public const string LocalizationSourceName = "kamrj";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ee3aa9f1417c4304950aed4cc43af8cc";
    }
}
