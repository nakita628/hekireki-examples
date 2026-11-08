defmodule TypeHero.Vote do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          root_type: atom(),
          user: TypeHero.User.t() | nil,
          challenge: TypeHero.Challenge.t() | nil,
          comment: TypeHero.Comment.t() | nil,
          submission: TypeHero.Submission.t() | nil,
          shared_solution: TypeHero.SharedSolution.t() | nil
        }

  schema "Vote" do
    field(:root_type, Ecto.Enum, values: [:CHALLENGE, :COMMENT, :SUBMISSION, :SHAREDSOLUTION], source: :rootType)
    field(:user_id, :binary_id, source: :userId)
    field(:challenge_id, :id, source: :challengeId)
    field(:comment_id, :id, source: :commentId)
    field(:submission_id, :id, source: :submissionId)
    field(:shared_solution_id, :id, source: :sharedSolutionId)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    belongs_to(:challenge, TypeHero.Challenge, foreign_key: :challenge_id, define_field: false)
    belongs_to(:comment, TypeHero.Comment, foreign_key: :comment_id, define_field: false)
    belongs_to(:submission, TypeHero.Submission, foreign_key: :submission_id, define_field: false)
    belongs_to(:shared_solution, TypeHero.SharedSolution, foreign_key: :shared_solution_id, define_field: false)
  end
end