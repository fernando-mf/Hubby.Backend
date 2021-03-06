﻿using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupCommandHandler: INotificationHandler<JoinGroupCommand>
    {
        private readonly IRepository _repository;

        public JoinGroupCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(JoinGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            buddy.JoinGroup(notification.GroupId, notification.Type);
            await _repository.Save(buddy);
        }
    }
}
