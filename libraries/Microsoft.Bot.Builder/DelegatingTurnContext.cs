﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Schema;

namespace Microsoft.Bot.Builder
{
    /// <summary>
    /// A TurnContext with a strongly typed Activity property that wraps an untyped inner TurnContext.
    /// </summary>
    /// <typeparam name="T">An IActivity derived type, that is one of IMessageActivity, IConversationUpdateActivity etc.</typeparam>
    public class DelegatingTurnContext<T> : ITurnContext<T>
        where T : IActivity
    {
        private ITurnContext _innerTurnContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatingTurnContext{T}"/> class.
        /// </summary>
        /// <param name="innerTurnContext">The inner turn context.</param>
        public DelegatingTurnContext(ITurnContext innerTurnContext)
        {
            _innerTurnContext = innerTurnContext;
        }

        /// <summary>
        /// Gets the inner  context's activity, cast to the type parameter of this <see cref="DelegatingTurnContext{T}"/>.
        /// </summary>
        /// <value>The inner context's activity.</value>
        T ITurnContext<T>.Activity => (T)(IActivity)_innerTurnContext.Activity;

        public BotAdapter Adapter => _innerTurnContext.Adapter;

        public TurnContextStateCollection TurnState => _innerTurnContext.TurnState;

        public Activity Activity => _innerTurnContext.Activity;

        public bool Responded => _innerTurnContext.Responded;

        public Task DeleteActivityAsync(string activityId, CancellationToken cancellationToken = default(CancellationToken))
            => _innerTurnContext.DeleteActivityAsync(activityId, cancellationToken);

        public Task DeleteActivityAsync(ConversationReference conversationReference, CancellationToken cancellationToken = default(CancellationToken))
            => _innerTurnContext.DeleteActivityAsync(conversationReference, cancellationToken);

        public ITurnContext OnDeleteActivity(DeleteActivityHandler handler)
            => _innerTurnContext.OnDeleteActivity(handler);

        public ITurnContext OnSendActivities(SendActivitiesHandler handler)
            => _innerTurnContext.OnSendActivities(handler);

        public ITurnContext OnUpdateActivity(UpdateActivityHandler handler)
            => _innerTurnContext.OnUpdateActivity(handler);

        public Task<ResourceResponse[]> SendActivitiesAsync(IActivity[] activities, CancellationToken cancellationToken = default(CancellationToken))
            => _innerTurnContext.SendActivitiesAsync(activities, cancellationToken);

        public Task<ResourceResponse> SendActivityAsync(string textReplyToSend, string speak = null, string inputHint = InputHints.AcceptingInput, CancellationToken cancellationToken = default(CancellationToken))
            => _innerTurnContext.SendActivityAsync(textReplyToSend, speak, inputHint, cancellationToken);

        public Task<ResourceResponse> SendActivityAsync(IActivity activity, CancellationToken cancellationToken = default(CancellationToken))
            => _innerTurnContext.SendActivityAsync(activity, cancellationToken);

        public Task<ResourceResponse> UpdateActivityAsync(IActivity activity, CancellationToken cancellationToken = default(CancellationToken))
            => _innerTurnContext.UpdateActivityAsync(activity, cancellationToken);
    }
}
