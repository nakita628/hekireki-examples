defmodule TypeHero.SharedSolution do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          is_pinned: boolean(),
          title: String.t(),
          description: String.t(),
          inserted_at: DateTime.t(),
          user: TypeHero.User.t() | nil,
          challenge: TypeHero.Challenge.t() | nil,
          vote: [TypeHero.Vote.t()],
          solution_comment: [TypeHero.Comment.t()],
          report: [TypeHero.Report.t()],
          notification: [TypeHero.Notification.t()]
        }

  schema "SharedSolution" do
    field(:is_pinned, :boolean, default: false, source: :isPinned)
    field(:title, :string)
    field(:description, :string)
    field(:user_id, :binary_id, source: :userId)
    field(:challenge_id, :id, source: :challengeId)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    belongs_to(:challenge, TypeHero.Challenge, foreign_key: :challenge_id, define_field: false)
    has_many(:vote, TypeHero.Vote, foreign_key: :shared_solution_id)
    has_many(:solution_comment, TypeHero.Comment, foreign_key: :root_solution_id)
    has_many(:report, TypeHero.Report, foreign_key: :solution_id)
    has_many(:notification, TypeHero.Notification, foreign_key: :shared_solution_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end