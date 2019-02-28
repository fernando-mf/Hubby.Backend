﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Infrastructure.Security;
using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountCommandHandler: INotificationHandler<RegisterAccountCommand>
    {
        private readonly IRepository<Account.Domain.Account> _accountRepository;
        private readonly IRepository<Buddy.Domain.Entities.Buddy> _buddyRepository;
        private readonly IPasswordComputer _passwordComputer;
        
        public RegisterAccountCommandHandler(
            IRepository<Account.Domain.Account> accountRepository,
            IRepository<Buddy.Domain.Entities.Buddy> buddyRepository,
            IPasswordComputer passwordComputer)
        {
            _accountRepository = accountRepository;
            _buddyRepository = buddyRepository;
            _passwordComputer = passwordComputer;
        }

        public async Task Handle(RegisterAccountCommand notification, CancellationToken cancellationToken)
        {
            // Create account
            var hashedPassword = _passwordComputer.Hash(notification.Password);
            var account = await _accountRepository.GetById(notification.Id);
            account.Register(notification.Id, hashedPassword);

            // Create buddy
            var buddyId = Guid.NewGuid().ToString();
            var buddy = await _buddyRepository.GetById(buddyId);
            buddy.Create(buddyId);

            // Link buddy to account
            account.LinkBuddy(buddyId);

            // Persist changes
            await _accountRepository.Save(account);
            await _buddyRepository.Save(buddy);
        }
    }
}
