using System;
using System.Linq;
using System.Text;

namespace RaidGroupFinder.Helper
{
    public static class EncodeHelper
    {
        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }
        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
            string returnValue = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;
        }

        static public (string pokemonGoName, string trainerCode) DismemberTrainerTitle(string user)
        {
            var splits = user.Split(" | ");
            return (splits.First(), splits.Last());
        }

        static public string CreateTrainerTitle(string pokemonGoNickname, string trainerCode)
        {
            return $"{ pokemonGoNickname} | {trainerCode}";
        }
    }
}
