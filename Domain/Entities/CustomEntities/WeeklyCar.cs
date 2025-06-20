namespace Domain.Entities.CustomEntities;

public class WeeklyCar
{
    public WeeklyCar(string markId, string modelId, string modelName, string markName, string generationId)
    {
        MarkId = markId;
        GenerationId = generationId;
        ModelId = modelId;
        MarkName = markName;
        ModelName = modelName;
    }

    
    public string MarkId { get; set; }
    public string ModelId { get; set; }
    public string ModelName { get; set; }
    public string MarkName { get; set; }
    public string GenerationId { get; set; }
}