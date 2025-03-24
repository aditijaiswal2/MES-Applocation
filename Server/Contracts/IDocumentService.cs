using System.Text.Json.Nodes;

namespace MES.Server.Contracts
{
    public interface IDocumentService
    {
        byte[] GeneratePDF(string htmlContent);
        string GenerateHTML(string htmlContent, bool display);
        string FormatResponse(JsonNode ques, JsonNode resp);
        string GetHostEnv();
    }
}
