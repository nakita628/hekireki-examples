#nullable enable

namespace TypeHero.Models;

public partial class Vote
{
    public int Id { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public VoteType RootType { get; set; }

    public virtual Challenge? Challenge { get; set; }

    public int? ChallengeId { get; set; }

    public virtual Comment? Comment { get; set; }

    public int? CommentId { get; set; }

    public virtual Submission? Submission { get; set; }

    public int? SubmissionId { get; set; }

    public virtual SharedSolution? SharedSolution { get; set; }

    public int? SharedSolutionId { get; set; }
}
