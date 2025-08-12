namespace Shop.Domain
{
    public static class Rules
    {
        public const string Administrator = "1";
        public const string Systemuser = "2";
        public const string ContentUploader = "3";

        public static string GetRuleBy(long id)
        {
            switch (id)
            {
                case 1:
                    return "Administrator";
                case 3:
                    return "ContentUploader";
                default:
                    return "";
            }
        }
    }
}