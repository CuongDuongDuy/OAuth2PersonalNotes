﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OAuth2PersonalNotes.Web.Helpers
{
    // code adjusted from Thinktecture's client model (thinktecture.github.com)
    public static class TokenHelper
    {
        public static void DecodeAndWrite(string token)
        {
            try
            {
                var parts = token.Split('.');

                var partToConvert = parts[1];
                partToConvert = partToConvert.Replace('-', '+');
                partToConvert = partToConvert.Replace('_', '/');
                switch (partToConvert.Length % 4)
                {
                    case 0:
                        break;
                    case 2:
                        partToConvert += "==";
                        break;
                    case 3:
                        partToConvert += "=";
                        break;
                    default:
                        break;
                }

                var partAsBytes = Convert.FromBase64String(partToConvert);
                var partAsUtf8String = Encoding.UTF8.GetString(partAsBytes, 0, partAsBytes.Count());

                // Json .NET
                var jwt = JObject.Parse(partAsUtf8String);

                // Write to output
                Debug.Write(jwt.ToString());
            }
            catch (Exception ex)
            {
                // something went wrong
                Debug.Write(ex.Message);
            }
        }
    }
}

