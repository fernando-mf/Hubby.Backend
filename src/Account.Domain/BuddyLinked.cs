﻿using Core.Domain;

namespace Account.Domain
{
    public class BuddyLinked : IEvent
    {
        public BuddyLinked(string id, string buddyId)
        {
            Id = id;
            BuddyId = buddyId;
        }

        public string Id { get; }
        public string BuddyId { get; }
    }
}