namespace Domain.Services.Impl
{
    using Domain.Exceptions;
    using Domain.Handlers;
    using Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class ConversationsExtractor : ErrorHandlerAbs
    {
        protected List<ConversationDataModel> ExtractConversationFromArray(string[] conversations, string coversationStartIdentifier)
        {
            List<ConversationDataModel> currentCoversations = new List<ConversationDataModel>();
            var functionToExecute = new Func<bool>(() =>
            {
                int converSationFormatVerifier = 0;
                for (int actualLine = 0; actualLine < conversations.Length; actualLine++)
                {
                    if (conversations[actualLine].Contains(coversationStartIdentifier.ToUpperInvariant()) && conversations[actualLine].Split(' ').Length == 2)
                    {
                        converSationFormatVerifier++;
                        int nextLine = actualLine + 1;
                        List<string> conversationModelContent = new List<string>();
                        nextLine = BuildConversationFromLines(conversations, nextLine, conversationModelContent);
                        currentCoversations.Add(new ConversationDataModel() { Content = conversationModelContent.ToArray(), LinesCount = conversationModelContent.Count, Duration = CalculateConversationDuration(conversationModelContent), Name = conversations[actualLine] });
                        actualLine = nextLine;
                    }
                }
                if(converSationFormatVerifier == 0)
                {
                    throw new EmptyModelServiceException($"Incorrect format file, no conversations detected", new Exception());
                }
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new EmptyModelServiceException($"ConversationsExtractor => ExtractConversationFromArray => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);            
            return currentCoversations;
        }

        private int BuildConversationFromLines(string[] conversations, int nextLine, List<string> conversationModelContent)
        {
            var functionToExecute = new Func<bool>(() =>
            {
                while (nextLine < conversations.Length && !string.IsNullOrEmpty(conversations[nextLine]))
                {
                    conversationModelContent.Add(RemoveDiacritics(conversations[nextLine]));
                    nextLine++;
                }
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new EmptyModelServiceException($"BuildConversationFromLines => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
            return nextLine;
        }

        private int CalculateConversationDuration(List<string> conversationModelContent)
        {
            DateTime conversationStartDateTime = new DateTime();
            DateTime conversationEndDateTime = new DateTime();
            var functionToExecute = new Func<bool>(() =>
            {
                var conversationStartStringTime = conversationModelContent.FirstOrDefault().Split(' ').FirstOrDefault();
                var conversationEndStringTime = conversationModelContent.LastOrDefault().Split(' ').FirstOrDefault();
                conversationStartDateTime = DateTime.Parse(conversationStartStringTime, CultureInfo.InvariantCulture);
                conversationEndDateTime = DateTime.Parse(conversationEndStringTime, CultureInfo.InvariantCulture);
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new EmptyModelServiceException($"CalculateConversationDuration => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);            
            return conversationEndDateTime.Subtract(conversationStartDateTime).Minutes;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
