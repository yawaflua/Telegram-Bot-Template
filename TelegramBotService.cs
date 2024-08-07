﻿using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace $safeprojectname$
{
    public class TelegramBotService(IConfiguration conf, ILogger<TelegramBotService> logger) : TelegramBotClient(conf.GetValue<string>("tg-token")), IHostedService
{
    private static Dictionary<long, string> _userStates = new Dictionary<long, string>();

    // For add any property, u should to add it in StartAsync func, like this:
    // _PROPERTY_NAME = PROPERTY_NAME_FROM_CLASS

    private static ILogger<TelegramBotService> _logger { get; set; }


    private static async Task MainHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.Log(
            LogLevel.Information,
            $"Received an update: {update.Type}, Caller ID: {update.Message?.From?.Id ?? update.CallbackQuery?.From.Id ?? update.InlineQuery?.From.Id}"
            );
        var handler = update switch
        {

            { Message: { } message } => HandleUpdateAsync(botClient, message),
            //{ EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
            { CallbackQuery: { } callbackQuery } => QueryUpdateHandler(botClient, callbackQuery),
            { InlineQuery: { } inlineQuery } => InlineUpdateHandler(botClient, inlineQuery),
            //{ ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
            _ => Task.CompletedTask
        };

        await handler;


    }
    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Message message)
    {
        try
        {
            string messageText = message.Text ?? message.Caption;
            switch (messageText)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(message.From.Id, "Start");
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}");
            _logger.LogError($"Stack: {ex.StackTrace}");
        }
    }

    private static async Task QueryUpdateHandler(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        try
        {
            string queryData = callbackQuery.Data;
            switch (queryData)
            {
                case "test":
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Tested");
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}");
            _logger.LogError($"Stack: {ex.StackTrace}");
        }
    }

    private static async Task InlineUpdateHandler(ITelegramBotClient botClient, InlineQuery inlineQuery)
    {
        try
        {
            string inlineData = inlineQuery.Query;
            switch (inlineData)
            {
                case "test":
                    await botClient.AnswerInlineQueryAsync(inlineQuery.Id, new InlineQueryResult[] { });
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}");
            _logger.LogError($"Stack: {ex.StackTrace}");
        }
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.Log(LogLevel.Information, $"Starting build {this.GetType().Name}");
        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery, UpdateType.InlineQuery] // receive all update types except ChatMember related updates
        };

        _logger = logger;

        this.StartReceiving(
            updateHandler: MainHandler,
            pollingErrorHandler: (k, ex, ctx) =>
            {
                logger.LogError(ex.StackTrace);

                return Task.CompletedTask;
            },
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );
        var me = await this.GetMeAsync(cancellationToken: cancellationToken);
        logger.Log(LogLevel.Information, $"Start listening bot @{me.Username}");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.Log(LogLevel.Information, $"Stopping service");
    }

}
}
