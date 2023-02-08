using Actio.Common.Events.Shared;

namespace Actio.Common.Events;
public class ActivityCreated : IAuthenticatedEvent
{
    protected ActivityCreated()
    {
    }

    public ActivityCreated(Guid id, Guid userId, string category, string description, DateTime createAt)
    {
        Id = id;
        UserId = userId;
        Category = category;
        Description = description;
        CreateAt = createAt;
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string Category { get; private set; }

    public string Description { get; private set; }

    public DateTime CreateAt { get; private set; }
}
