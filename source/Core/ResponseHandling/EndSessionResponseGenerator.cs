﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Linq;
using Thinktecture.IdentityServer.Core.Authentication;
using Thinktecture.IdentityServer.Core.Extensions;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Validation;

namespace Thinktecture.IdentityServer.Core.ResponseHandling
{
    public class EndSessionResponseGenerator
    {
        public SignOutMessage CreateSignoutMessage(ValidatedEndSessionRequest request)
        {
            var message = new SignOutMessage();
            
            if (request.Client != null)
            {
                message.ClientId = request.Client.ClientId;

                if (request.PostLogOutUri != null)
                {
                    message.ReturnUrl = request.PostLogOutUri.AbsoluteUri;
                }
                else
                {
                    if (request.Client.PostLogoutRedirectUris.Any())
                    {
                        message.ReturnUrl = request.Client.PostLogoutRedirectUris.First().AbsoluteUri;
                    }
                }

                if (request.State.IsPresent())
                {
                    if (message.ReturnUrl.IsPresent())
                    {
                        message.ReturnUrl = message.ReturnUrl.AddQueryString("state=" + request.State);
                    }
                }
            }

            return message;
        }
    }
}