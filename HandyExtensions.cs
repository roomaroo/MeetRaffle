namespace MeetRaffle
{
    static class HandyExtensions
    {
        public static bool NullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
