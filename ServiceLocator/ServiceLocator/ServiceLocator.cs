using ServiceLocator.Exceptions;

namespace ServiceLocator;

public class ServiceLocator
{
    private Dictionary<Type, ServiceDescriptor>
        _dict = new();

    public bool Register<TInterface, TImplementation>(ServiceLifeTime lifetime)
    {
        if (!typeof(TImplementation).IsAssignableTo(typeof(TInterface)))
            throw new DifferentOriginException();
        if (_dict.ContainsKey(typeof(TInterface))) throw new TwoSameServicesException();
        if(typeof(TImplementation).GetConstructors().Length != 1) throw new MultipleConstructorsException();
        
        ServiceDescriptor descriptor = new ServiceDescriptor(typeof(TImplementation), lifetime);
        _dict.Add(typeof(TInterface), descriptor);
        return true;
    }
    
    public TInterface Get<TInterface>()
    {
        return (TInterface) GetService(typeof(TInterface));
    }

    private object CreateService(Type implementationType)
    {
        var constructor = implementationType.GetConstructors().First();
        var parameters = constructor.GetParameters()
            .Select(param => GetService(param.ParameterType))
            .ToArray();

        return constructor.Invoke(parameters);
    }

    private object GetService(Type type)
    {
        if (!_dict.ContainsKey(type)) throw new NotRegisteredServiceException();
        
        var descriptor = _dict[type];
        
        if (descriptor.LifeTime == ServiceLifeTime.Singleton)
        {
            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            var implementation = CreateService(descriptor.ImplementationType);
            descriptor.Implementation = implementation;
            return implementation;
        }

        return CreateService(descriptor.ImplementationType);
    }

}