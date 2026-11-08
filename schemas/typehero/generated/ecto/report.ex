defmodule TypeHero.Report do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          text: String.t(),
          type: atom(),
          status: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          reporter: TypeHero.User.t() | nil,
          user: TypeHero.User.t() | nil,
          challenge: TypeHero.Challenge.t() | nil,
          comment: TypeHero.Comment.t() | nil,
          solution: TypeHero.SharedSolution.t() | nil,
          moderator: TypeHero.User.t() | nil,
          issues: [TypeHero.ReportIssue.t()]
        }

  schema "Report" do
    field(:text, :string)
    field(:type, Ecto.Enum, values: [:CHALLENGE, :USER, :COMMENT, :SOLUTION])
    field(:status, Ecto.Enum, values: [:PENDING, :CLEARED, :DISMISSED])
    field(:reporter_id, :binary_id, source: :reporterId)
    field(:user_id, :binary_id, source: :userId)
    field(:challenge_id, :id, source: :challengeId)
    field(:comment_id, :id, source: :commentId)
    field(:solution_id, :id, source: :solutionId)
    field(:moderator_id, :binary_id, source: :moderatorId)
    belongs_to(:reporter, TypeHero.User, foreign_key: :reporter_id, define_field: false, type: :binary_id)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    belongs_to(:challenge, TypeHero.Challenge, foreign_key: :challenge_id, define_field: false)
    belongs_to(:comment, TypeHero.Comment, foreign_key: :comment_id, define_field: false)
    belongs_to(:solution, TypeHero.SharedSolution, foreign_key: :solution_id, define_field: false)
    belongs_to(:moderator, TypeHero.User, foreign_key: :moderator_id, define_field: false, type: :binary_id)
    has_many(:issues, TypeHero.ReportIssue, foreign_key: :report_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end