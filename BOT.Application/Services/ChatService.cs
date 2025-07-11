

using BOT.Application.Dtos;
using BOT.Application.Services.Interfaces;
using BOT.Domain.Entities;
using BOT.Domain.Interfaces;

namespace BOT.Application.Services;

public class ChatService(
    IMCPRepository mcpRepository,
    IChatRepository chatRepository)
    : IChatService
{
    public async Task<ChatResponseDto> ProcessChatAsync(ChatRequestDto request,string jwt)
        {
            try
            {
                var conversation = BuildConversation(request,jwt);
                var availableTools = await mcpRepository.GetAvailableToolsAsync();
                
                var response = await chatRepository.GetResponseAsync(
                    conversation.Messages, 
                    availableTools);

                conversation.AddMessage(new ChatMessage("assistant", response));

                return new ChatResponseDto(
                    response,
                    conversation.Messages.Select(m => new ChatMessageDto(m.Role, m.Content)).ToList()
                );
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to process chat request", ex);
            }
        }

        public async Task<IAsyncEnumerable<string>> ProcessStreamingChatAsync(ChatRequestDto request,string jwt)
        {
            var conversation = BuildConversation(request,jwt);
            var availableTools = await mcpRepository.GetAvailableToolsAsync();

            return await chatRepository.GetStreamingResponseAsync(conversation.Messages, availableTools);
        }

        private Conversation BuildConversation(ChatRequestDto request,string jwt)
        {
            var conversation = new Conversation();
            conversation.AddMessage(new ChatMessage("system", "Here is the configuration of your LLM, prompot it as you wish to get resultss for your own context"));
            //Authentication config, this prompt is mandatory if the endpoint is annoted with [Autorize]
            conversation.AddMessage(new ChatMessage("system", $"The actual connected and autorized user has this JWT : {jwt} And you have to pass it to each tool call without never showing it to user." ));
            
            if (request.ConversationHistory != null)
            {
                foreach (var msg in request.ConversationHistory)
                {
                    conversation.AddMessage(new ChatMessage(msg.Role, msg.Content));
                }
            }
            
            conversation.AddMessage(new ChatMessage("user", request.Message));
            return conversation;
        }
    }