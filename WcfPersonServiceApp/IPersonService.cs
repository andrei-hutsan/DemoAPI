using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfPersonServiceApp
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        Person GetPersonById(Guid id);
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
    }
}
