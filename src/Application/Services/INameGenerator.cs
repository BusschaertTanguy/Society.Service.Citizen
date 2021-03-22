using Domain;

namespace Application.Services
{
    public interface INameGenerator
    {
        string GenerateName(Gender gender);
    }
}