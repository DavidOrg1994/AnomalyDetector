using AnomalyDetector.constants;
using AnomalyDetector.Services.GitSuspicionDetectors;

namespace AnomalyDetector.Services.ValidationsFactory;

public class ValidationFactory : IValidationFactory
{
    readonly ISuspicionDetector _pushValidator;
    readonly ISuspicionDetector _repositoryValidator;
    readonly ISuspicionDetector _teamsValidator;

    public ValidationFactory(
        [FromKeyedServices(KeyedServices.GitPushesDetector)] ISuspicionDetector pushValidator,
        [FromKeyedServices(KeyedServices.GitRepositoryDetector)] ISuspicionDetector repositoryValidator,
        [FromKeyedServices(KeyedServices.GitTeamsDetector)] ISuspicionDetector teamsValidator)
    {
        _pushValidator = pushValidator;
        _repositoryValidator = repositoryValidator;
        _teamsValidator = teamsValidator;
    }

    public ISuspicionDetector? GetValidator(string validationType)
    {
        return validationType switch
        {
            "push" => _pushValidator,
            "repository_deleted" => _repositoryValidator,
            "team_created" => _teamsValidator,
            _ => null
        };
    }
}
