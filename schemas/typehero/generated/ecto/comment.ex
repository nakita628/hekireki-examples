defmodule TypeHero.Comment do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          text: String.t(),
          visible: boolean(),
          root_type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_comment: TypeHero.Comment.t() | nil,
          user: TypeHero.User.t() | nil,
          root_challenge: TypeHero.Challenge.t() | nil,
          root_solution: TypeHero.SharedSolution.t() | nil,
          replies: [TypeHero.Comment.t()],
          report: [TypeHero.Report.t()],
          vote: [TypeHero.Vote.t()],
          notification: [TypeHero.Notification.t()]
        }

  schema "Comment" do
    field(:text, :string)
    field(:visible, :boolean, default: true)
    field(:root_type, Ecto.Enum, values: [:SOLUTION, :CHALLENGE], default: :CHALLENGE, source: :rootType)
    field(:parent_id, :id, source: :parentId)
    field(:user_id, :binary_id, source: :userId)
    field(:root_challenge_id, :id, source: :rootChallengeId)
    field(:root_solution_id, :id, source: :rootSolutionId)
    belongs_to(:parent_comment, TypeHero.Comment, foreign_key: :parent_id, define_field: false)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    belongs_to(:root_challenge, TypeHero.Challenge, foreign_key: :root_challenge_id, define_field: false)
    belongs_to(:root_solution, TypeHero.SharedSolution, foreign_key: :root_solution_id, define_field: false)
    has_many(:replies, TypeHero.Comment, foreign_key: :parent_id)
    has_many(:report, TypeHero.Report, foreign_key: :comment_id)
    has_many(:vote, TypeHero.Vote, foreign_key: :comment_id)
    has_many(:notification, TypeHero.Notification, foreign_key: :comment_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end