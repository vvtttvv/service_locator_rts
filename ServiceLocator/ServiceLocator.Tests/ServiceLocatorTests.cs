using ServiceLocator.Exceptions;
using ServiceLocator.Mock;
namespace ServiceLocator.Tests;

public class Tests
{
    private ServiceLocator _slTest;

    public interface IMultipleConstructorHandler { };
    public class MultipleConstructorHandler : IMultipleConstructorHandler
    {
        public MultipleConstructorHandler() { }
        public MultipleConstructorHandler(ICar car, IPassenger pas) { }
    }
    
    [SetUp]
    public void Setup()
    {
        _slTest = new ServiceLocator();
    }

    [Test]
    public void Register_DifferentRefInHeap_True()
    {
        _slTest.Register<IPassenger, Passenger>(ServiceLifeTime.Transient);
        IPassenger passenger = _slTest.Get<IPassenger>();
        IPassenger passenger2 = _slTest.Get<IPassenger>();
        Assert.That(passenger, Is.Not.SameAs(passenger2));
    }
    
    [Test]
    public void Register_SameRefInHeap_True()
    {
        _slTest.Register<IPassenger, Passenger>(ServiceLifeTime.Singleton);
        IPassenger passenger = _slTest.Get<IPassenger>();
        IPassenger passenger2 = _slTest.Get<IPassenger>();
        Assert.That(passenger, Is.SameAs(passenger2));
    }
    
    [Test]
    public void Register_ServiceAInServiceBCreated_True()
    {
        _slTest.Register<ICar, Car>(ServiceLifeTime.Singleton);
        _slTest.Register<IPassenger, Passenger>(ServiceLifeTime.Singleton);
        ICar car = _slTest.Get<ICar>();
        Assert.That(car.Passenger, Is.Not.Null);
    }
    
    [Test]
    public void Register_TwoSameServices_TwoSameServicesException()
    {
        _slTest.Register<ICar, Car>(ServiceLifeTime.Singleton);
        Assert.Throws<TwoSameServicesException>(() => 
            _slTest.Register<ICar, Car>(ServiceLifeTime.Singleton)
        );
    }
    
    [Test]
    public void Register_MultipleConstructors_MultipleConstructorsException()
    {
        Assert.Throws<MultipleConstructorsException>(() => 
            _slTest.Register<IMultipleConstructorHandler, MultipleConstructorHandler>(ServiceLifeTime.Transient)
        );
    }
    
    [Test]
    public void Register_GetNotRegistered_NotRegisteredServiceException()
    {
        Assert.Throws<NotRegisteredServiceException>(() => 
            _slTest.Get<ICar>()
        );
    }
}