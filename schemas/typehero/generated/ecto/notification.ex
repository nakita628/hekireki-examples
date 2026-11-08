defmodule TypeHero.Notification do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          is_read: boolean(),
          type: atom(),
          content: String.t() | nil,
          blurb: String.t() | nil,
          url: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          to_user: TypeHero.User.t() | nil,
          from_user: TypeHero.User.t() | nil,
          comment: TypeHero.Comment.t() | nil,
          challenge: TypeHero.Challenge.t() | nil,
          shared_solution: TypeHero.SharedSolution.t() | nil
        }

  schema "Notification" do
    field(:is_read, :boolean, default: false, source: :isRead)
    field(:type, Ecto.Enum, values: [:LIKE, :REPLY, :MENTION, :MISC, :SYSTEM])
    field(:content, :string)
    field(:blurb, :string)
    field(:url, :string)
    field(:to_user_id, :binary_id, source: :toUserId)
    field(:from_user_id, :binary_id, source: :fromUserId)
    field(:comment_id, :id, source: :commentId)
    field(:challenge_id, :id, source: :challengeId)
    field(:shared_solution_id, :id, source: :sharedSolutionId)
    belongs_to(:to_user, TypeHero.User, foreign_key: :to_user_id, define_field: false, type: :binary_id)
    belongs_to(:from_user, TypeHero.User, foreign_key: :from_user_id, define_field: false, type: :binary_id)
    belongs_to(:comment, TypeHero.Comment, foreign_key: :comment_id, define_field: false)
    belongs_to(:challenge, TypeHero.Challenge, foreign_key: :challenge_id, define_field: false)
    belongs_to(:shared_solution, TypeHero.SharedSolution, foreign_key: :shared_solution_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end