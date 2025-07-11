namespace BOT.Domain.Interfaces;
public record SampleDto(Guid Id, string Name);
public interface ISampleRepository
{
    public Task<SampleDto> SampleOperation(string jwt);
}