using System;

using NServiceBus.Saga;

namespace ServerEndpoint.ChildPoolService
{
    public class RegisteredChildData : ISagaEntity
    {
        /// <summary>
        /// Gets or sets the Id of the process. Do NOT generate this value in your code.
        /// The value of the Id will be generated automatically to provide the
        /// best performance for saving in a database.
        /// </summary>
        /// <remarks>
        /// The reason Guid is used for process Id is that messages containing this Id need
        /// to be sent by the process even before it is persisted.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the return address of the endpoint that caused the process to run.
        /// </summary>
        public string Originator { get; set; }

        /// <summary>
        /// Gets or sets the Id of the message which caused the saga to start.
        /// This is needed so that when we reply to the Originator, any
        /// registered callbacks will be fired correctly.
        /// </summary>
        public string OriginalMessageId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Child.
        /// </summary>
        public Guid ChildId { get; set; }
        
        /// <summary>
        /// Gets or sets the Childs Name.
        /// </summary>
        public string ChildName { get; set; }

        /// <summary>
        /// Gets or sets the Childs Age
        /// </summary>
        public int ChildAge { get; set; }

        /// <summary>
        /// Gets or sets the Country that the child is registered in.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the type of project the child is registered in
        /// </summary>
        public string ProgramType { get; set; }

        /// <summary>
        /// Gets or sets a link to the location of the child's photo.
        /// </summary>
        public Uri ChildPhotoLink { get; set; }

        /// <summary>
        /// Gets or sets the Session Id of the client that has placed a request lock on this child.
        /// </summary>
        public Guid RequestLockId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this child as being marked for deleted due to being de-registered.
        /// </summary>
        public bool MarkAsDeleted { get; set; }
    }
}