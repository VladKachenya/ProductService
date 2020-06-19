namespace ProductServices.Notifier
{
    internal static class Constants
    {

        public static string ConnectionString => nameof(ConnectionString);
        public static string ExchangeName => nameof(ExchangeName);

        public static string AllowAnyCrosPolicy=> nameof(AllowAnyCrosPolicy);

        /// <summary>
        /// Name of connection invoke method
        /// </summary>
        public static string product_changes => nameof(product_changes);

    }
}