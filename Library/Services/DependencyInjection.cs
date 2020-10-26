using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class DependencyInjection
    {
        public interface IServiceCollection : IList<ServiceDescriptor>, ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>, IEnumerable
        {
        }


        //
        // Summary:
        //     /// Specifies the lifetime of a service in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
        //     ///
        public enum ServiceLifetime
        {
            //
            // Summary:
            //     /// Specifies that a single instance of the service will be created. ///
            Singleton,
            //
            // Summary:
            //     /// Specifies that a new instance of the service will be created for each scope.
            //     ///
            //
            // Remarks:
            //     /// In ASP.NET Core applications a scope is created around each server request.
            //     ///
            Scoped,
            //
            // Summary:
            //     /// Specifies that a new instance of the service will be created every time it
            //     is requested. ///
            Transient
        }



        //
        // Summary:
        //     /// Describes a service with its service type, implementation, and lifetime.
        //     ///
        [DebuggerDisplay("Lifetime = {Lifetime}, ServiceType = {ServiceType}, ImplementationType = {ImplementationType}")]
        public class ServiceDescriptor
        {
            //
            public ServiceLifetime Lifetime
            {
                get;
            }

            //
            public Type ServiceType
            {
                get;
            }

            //
            public Type ImplementationType
            {
                get;
            }

            //
            public object ImplementationInstance
            {
                get;
            }

            //
            public Func<IServiceProvider, object> ImplementationFactory
            {
                get;
            }

            //
            // Summary:
            //     /// Initializes a new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified implementationType. ///
            //
            // Parameters:
            //   serviceType:
            //     The System.Type of the service.
            //
            //   implementationType:
            //     The System.Type implementing the service.
            //
            //   lifetime:
            //     The Microsoft.Extensions.DependencyInjection.ServiceLifetime of the service.
            public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
                : this(serviceType, lifetime)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }
                if (implementationType == null)
                {
                    throw new ArgumentNullException("implementationType");
                }
                ImplementationType = implementationType;
            }

            //
            // Summary:
            //     /// Initializes a new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified instance /// as a Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton.
            //     ///
            //
            // Parameters:
            //   serviceType:
            //     The System.Type of the service.
            //
            //   instance:
            //     The instance implementing the service.
            public ServiceDescriptor(Type serviceType, object instance)
                : this(serviceType, ServiceLifetime.Singleton)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }
                if (instance == null)
                {
                    throw new ArgumentNullException("instance");
                }
                ImplementationInstance = instance;
            }

            //
            // Summary:
            //     /// Initializes a new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified factory. ///
            //
            // Parameters:
            //   serviceType:
            //     The System.Type of the service.
            //
            //   factory:
            //     A factory used for creating service instances.
            //
            //   lifetime:
            //     The Microsoft.Extensions.DependencyInjection.ServiceLifetime of the service.
            public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime)
                : this(serviceType, lifetime)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }
                if (factory == null)
                {
                    throw new ArgumentNullException("factory");
                }
                ImplementationFactory = factory;
            }

            private ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
            {
                Lifetime = lifetime;
                ServiceType = serviceType;
            }

            internal Type GetImplementationType()
            {
                if (ImplementationType != null)
                {
                    return ImplementationType;
                }
                if (ImplementationInstance != null)
                {
                    return ImplementationInstance.GetType();
                }
                if (ImplementationFactory != null)
                {
                    return ImplementationFactory.GetType().GenericTypeArguments[1];
                }
                return null;
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, TImplementation, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient
            //     lifetime. ///
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            //   TImplementation:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Transient<TService, TImplementation>() where TService : class where TImplementation : class, TService
            {
                return Describe<TService, TImplementation>(ServiceLifetime.Transient);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// service and implementationType /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient
            //     lifetime. ///
            //
            // Parameters:
            //   service:
            //     The type of the service.
            //
            //   implementationType:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Transient(Type service, Type implementationType)
            {
                if (service == null)
                {
                    throw new ArgumentNullException("service");
                }
                if (implementationType == null)
                {
                    throw new ArgumentNullException("implementationType");
                }
                return Describe(service, implementationType, ServiceLifetime.Transient);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, TImplementation, /// implementationFactory,
            //     /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient
            //     lifetime. ///
            //
            // Parameters:
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            //   TImplementation:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Transient<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
            {
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, implementationFactory, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient
            //     lifetime. ///
            //
            // Parameters:
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Transient<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
            {
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// service, implementationFactory, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient
            //     lifetime. ///
            //
            // Parameters:
            //   service:
            //     The type of the service.
            //
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Transient(Type service, Func<IServiceProvider, object> implementationFactory)
            {
                if (service == null)
                {
                    throw new ArgumentNullException("service");
                }
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(service, implementationFactory, ServiceLifetime.Transient);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, TImplementation, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped
            //     lifetime. ///
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            //   TImplementation:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Scoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
            {
                return Describe<TService, TImplementation>(ServiceLifetime.Scoped);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// service and implementationType /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped
            //     lifetime. ///
            //
            // Parameters:
            //   service:
            //     The type of the service.
            //
            //   implementationType:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Scoped(Type service, Type implementationType)
            {
                return Describe(service, implementationType, ServiceLifetime.Scoped);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, TImplementation, /// implementationFactory,
            //     /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped lifetime.
            //     ///
            //
            // Parameters:
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            //   TImplementation:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Scoped<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
            {
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, implementationFactory, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped
            //     lifetime. ///
            //
            // Parameters:
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Scoped<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
            {
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// service, implementationFactory, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped
            //     lifetime. ///
            //
            // Parameters:
            //   service:
            //     The type of the service.
            //
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Scoped(Type service, Func<IServiceProvider, object> implementationFactory)
            {
                if (service == null)
                {
                    throw new ArgumentNullException("service");
                }
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(service, implementationFactory, ServiceLifetime.Scoped);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, TImplementation, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton
            //     lifetime. ///
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            //   TImplementation:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
            {
                return Describe<TService, TImplementation>(ServiceLifetime.Singleton);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// service and implementationType /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton
            //     lifetime. ///
            //
            // Parameters:
            //   service:
            //     The type of the service.
            //
            //   implementationType:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton(Type service, Type implementationType)
            {
                if (service == null)
                {
                    throw new ArgumentNullException("service");
                }
                if (implementationType == null)
                {
                    throw new ArgumentNullException("implementationType");
                }
                return Describe(service, implementationType, ServiceLifetime.Singleton);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, TImplementation, /// implementationFactory,
            //     /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton
            //     lifetime. ///
            //
            // Parameters:
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            //   TImplementation:
            //     The type of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
            {
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, implementationFactory, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton
            //     lifetime. ///
            //
            // Parameters:
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
            {
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// serviceType, implementationFactory, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton
            //     lifetime. ///
            //
            // Parameters:
            //   serviceType:
            //     The type of the service.
            //
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }
                if (implementationFactory == null)
                {
                    throw new ArgumentNullException("implementationFactory");
                }
                return Describe(serviceType, implementationFactory, ServiceLifetime.Singleton);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// TService, implementationInstance, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped
            //     lifetime. ///
            //
            // Parameters:
            //   implementationInstance:
            //     The instance of the implementation.
            //
            // Type parameters:
            //   TService:
            //     The type of the service.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton<TService>(TService implementationInstance) where TService : class
            {
                if (implementationInstance == null)
                {
                    throw new ArgumentNullException("implementationInstance");
                }
                return Singleton(typeof(TService), implementationInstance);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// serviceType, implementationInstance, /// and the Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped
            //     lifetime. ///
            //
            // Parameters:
            //   serviceType:
            //     The type of the service.
            //
            //   implementationInstance:
            //     The instance of the implementation.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Singleton(Type serviceType, object implementationInstance)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }
                if (implementationInstance == null)
                {
                    throw new ArgumentNullException("implementationInstance");
                }
                return new ServiceDescriptor(serviceType, implementationInstance);
            }

            private static ServiceDescriptor Describe<TService, TImplementation>(ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
            {
                return Describe(typeof(TService), typeof(TImplementation), lifetime);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// serviceType, implementationType, /// and lifetime. ///
            //
            // Parameters:
            //   serviceType:
            //     The type of the service.
            //
            //   implementationType:
            //     The type of the implementation.
            //
            //   lifetime:
            //     The lifetime of the service.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Describe(Type serviceType, Type implementationType, ServiceLifetime lifetime)
            {
                return new ServiceDescriptor(serviceType, implementationType, lifetime);
            }

            //
            // Summary:
            //     /// Creates an instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //     with the specified /// serviceType, implementationFactory, /// and lifetime.
            //     ///
            //
            // Parameters:
            //   serviceType:
            //     The type of the service.
            //
            //   implementationFactory:
            //     A factory to create new instances of the service implementation.
            //
            //   lifetime:
            //     The lifetime of the service.
            //
            // Returns:
            //     A new instance of Microsoft.Extensions.DependencyInjection.ServiceDescriptor.
            public static ServiceDescriptor Describe(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
            {
                return new ServiceDescriptor(serviceType, implementationFactory, lifetime);
            }
        }
    }

}
