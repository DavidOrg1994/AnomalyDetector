using AnomalyDetector.Services.GitSuspicionDetectors;

namespace AnomalyDetector.Services.ValidationsFactory;

public interface IValidationFactory
{
    ISuspicionDetector GetValidator(string validationType);
}
