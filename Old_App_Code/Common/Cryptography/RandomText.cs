namespace App_Code.Cryptography
{
    /// <summary>
    /// Provides methods for generating random texts.
    /// </summary>
    public static class RandomText
    {
        /// <summary>
        /// Generates an 4 letter random text.
        /// </summary>
        public static string Generate()
        {
            // Generate random text
            string s = "";
            //char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            char[] chars = "123456789".ToCharArray();
            int lenght = RNG.Next(3, 4);
            for (int i = 0; i < lenght; i++)
            {
                int index = RNG.Next(chars.Length - 1);
                s += chars[index].ToString();
            }
            return s;
        }
    }
}