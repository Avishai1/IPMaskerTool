using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace IPMasker
{
    public class IPMasker
    {
        private static Random Random = new Random();
        /// <summary>
        /// Replaces the original IP addresses in the new random addresses
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string ReplaceIPAddresses(string text)
        {
            try
            {
                HashSet<IPAddress> uniqeIPAddresses= GetIpsHashSet(text);
                HashSet<string> cClassAddresses = GetCClassHash(uniqeIPAddresses);
                Dictionary<string, string> RandomizedCClassIPs = GetRandomizedCClass(cClassAddresses);
                Dictionary<IPAddress, string> RandomizedIPAddresses = GetRandomizedIPs(uniqeIPAddresses,RandomizedCClassIPs);

                //removing hashset here
                //removing hashset here1
                //removing hashset here2



                string newText = text;
                for (int i = 0; i < RandomizedIPAddresses.Count; i++)
                {
                    newText = newText.Replace(RandomizedIPAddresses.ElementAt(i).Key.ToString(), RandomizedIPAddresses.ElementAt(i).Value.ToString());
                }
                return newText;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Dictionary<string, string> GetRandomizedCClass(HashSet<string> cClassAddresses)
        {
            try
            {
                Dictionary<string, string> CClassDictionary = cClassAddresses.ToDictionary(originalCClass => originalCClass, originalCClass => RandomizeCClass());
                return CClassDictionary;
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        private HashSet<string> GetCClassHash(HashSet<IPAddress> iPAddressesHash)
        {
            HashSet<string> ipCClass = new HashSet<string>();
            for (int i = 0; i < iPAddressesHash.Count; i++)
            {
                string ip = iPAddressesHash.ElementAt(i).ToString();
                string ipC = ip.Substring(0, ip.LastIndexOf('.'));
                ipCClass.Add(ipC);
            }
            return ipCClass;
        }
        private string RandomizeCClass()
        {
            try
            {
                int firstPortion = Random.Next(256);
                int secondPortion = Random.Next(256);
                int thirdPortion = Random.Next(256);
                StringBuilder sb = new StringBuilder();
                sb.Append(firstPortion);
                sb.Append(".");
                sb.Append(secondPortion);
                sb.Append(".");
                sb.Append(thirdPortion);
                sb.Append(".");
                string cClassString = sb.ToString();
                return cClassString;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Returns all of the IP's distinctivley
        /// </summary>
        /// <param name="text">The text from the file or the user input</param>
        /// <returns></returns>
        private HashSet<IPAddress> GetIpsHashSet(string text)
        {
            try
            {
                //the regex representaion of IP address without spaces
                var match = Regex.Matches(text, @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");
                //using HashSet to distinctivley  gather the addresses
                HashSet<IPAddress> IPsHashSet = new HashSet<IPAddress>();
                Random = new Random();
                for (int i = 0; i < match.Count; i++)
                {
                    IPAddress originalIP;
                    //Ensuring that the matched IP can be parsed
                    if (IPAddress.TryParse(match[i].Value, out originalIP))
                    {
                        //adding the matched IP to the hashset
                        IPsHashSet.Add(originalIP);
                    }
                }
 
                return IPsHashSet;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// /// <summary>
        /// Generates random IP address keeping the IP
        /// </summary>
        /// <param name="originalIP"></param>
        /// <returns>Random IP address</returns>
        private Dictionary<IPAddress, string> GetRandomizedIPs(HashSet<IPAddress> IPsHashSet, Dictionary<string,string> randomizedCClassIPs)
        {
            try
            {
                Dictionary<IPAddress, string> randomizedIPAddresses = new Dictionary<IPAddress, string>();
                for (int i = 0; i < IPsHashSet.Count; i++)
                {
                    string ipAddress = IPsHashSet.ElementAt(i).ToString();
                    string cClassAddress = ipAddress.Substring(0, ipAddress.LastIndexOf('.'));
                    string cClassRandom = randomizedCClassIPs[cClassAddress]+Random.Next(256).ToString();
                    randomizedIPAddresses.Add(IPsHashSet.ElementAt(i), cClassRandom);
                }
                
                return randomizedIPAddresses;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        
    }
}