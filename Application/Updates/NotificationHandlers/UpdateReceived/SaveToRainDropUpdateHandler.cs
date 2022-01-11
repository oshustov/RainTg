﻿using Application.Common.Interfaces;
using Application.Updates.NotificationHandlers.UpdateReceived.Abstract;
using Domain.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Application.Updates.NotificationHandlers.UpdateReceived
{
    public class SaveToRainDropUpdateHandler : BaseUpdateHandler
    {
        private readonly IRaindropService _raindropService;
        private readonly IBotService _bot;

        public SaveToRainDropUpdateHandler(IBotService bot, IRaindropService raindropService, IEnumerable<IExceptionHandler> handlers) : base(handlers)
        {
            _bot = bot;
            _raindropService = raindropService;
        }

        protected override bool ShouldHandle(Update update) => update.Type == UpdateType.Message;

        protected override async Task HandleUpdate(Update update, CancellationToken cancellationToken)
        {
            var raindrop = new Raindrop()
            {
                Link = update.Message.Text,
                PleaseParse = new { },
                Title = update.Message.Text,
                Type = "link",
                Tags = "test"
            };

            await _raindropService.Post(raindrop);

            var chatId = update!.Message!.Chat.Id;
            await _bot.Send("Raindrop saved!", chatId);
        }
    }
}
