using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMvcWithAuthInBuilt.Models
{
    public sealed class JwtModel
    {
        private static JwtModel instance = null;
        private static readonly object padlock = new object();

        JwtModel()
        {
        }

        public static JwtModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new JwtModel();
                    }
                    return instance;
                }
            }
        }

        public string Token { get; set; }
    }
}