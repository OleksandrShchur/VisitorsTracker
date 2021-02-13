﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VisitorsTracker.Core.Infrastructure
{
    public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;

        public SigningSymmetricKey(string key)
        {
            this._secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTOptions:SecretKey"));
        }

        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public SecurityKey GetKey() => this._secretKey;
    }
}
