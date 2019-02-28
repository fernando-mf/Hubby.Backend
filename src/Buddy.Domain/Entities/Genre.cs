﻿using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Genre: IEntity
    {
        public Genre(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
