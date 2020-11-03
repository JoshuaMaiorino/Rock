﻿// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//

using System.Diagnostics;
using MassTransit;
using Rock.Bus.Message;
using Rock.Bus.Queue;
using Rock.Model;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// Abstract Debug Consumer
    /// </summary>
    public abstract class RockDebugConsumer<TQueue, TMessage> : RockConsumer<TQueue, TMessage>
        where TQueue : IRockQueue, new()
        where TMessage : class, IRockMessage<TQueue>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void Consume( TMessage message )
        {
            var logString = RockMessage.GetLogString( message );
            var consumerName = GetType().FullName;

            var expiration = ConsumeContext.ExpirationTime.HasValue ?
                ConsumeContext.ExpirationTime.Value.ToLocalTime().ToString() :
                "None";

            Debug.WriteLine( $"==================\nConsumer: {consumerName}\nExpiration: {expiration}\n{logString}" );
        }
    }

    /// <summary>
    /// Person Was Updated
    /// </summary>
    public class FirstPersonWasUpdatedConsumer : RockDebugConsumer<EntityUpdateQueue, EntityWasUpdatedMessage<Person>>
    {
    }

    /// <summary>
    /// Person Was Updated
    /// </summary>
    public class SecondPersonWasUpdatedConsumer : RockDebugConsumer<EntityUpdateQueue, EntityWasUpdatedMessage<Person>>
    {
    }

    /// <summary>
    /// Start Task
    /// </summary>
    public class FirstStartTaskConsumer : RockDebugConsumer<StartTaskQueue, StartTaskMessage>
    {
    }

    /// <summary>
    /// Start Task
    /// </summary>
    public class SecondStartTaskConsumer : RockDebugConsumer<StartTaskQueue, StartTaskMessage>
    {
    }
}
