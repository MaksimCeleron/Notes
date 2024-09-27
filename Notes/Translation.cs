namespace Notes
{
    static class Translation
    {
        public static string button1(string lang)
        {
            if (lang == "en")
            {
                return "Button";
            }
            else if (lang == "ru")
            {
                return "Кнопка";
            } else
            {
                return "Not found";
            }
        }
    }
}
