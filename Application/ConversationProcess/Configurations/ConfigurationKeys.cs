namespace Application.ConversationProcess.Configurations
{
    using System.IO;
    //TODO: move this configs to the rigth place
    public static class ConfigurationKeys
    {
        public static string GetListOfMatchingWords() { return "Gracias,Buena Atencion,Muchas Gracias"; }
        public static string GetConversationStartIdentifier() { return "CONVERSACION"; }
        public static string GetUrgenMatch() { return "URGENTE"; }
        public static string GetExcelentMatch() { return "EXCELENTE SERVICIO"; }
        public static string GetRouteToFile() { return Path.Combine(Directory.GetCurrentDirectory(),"Resources", "historial_de_conversaciones.txt"); }
    }
}
