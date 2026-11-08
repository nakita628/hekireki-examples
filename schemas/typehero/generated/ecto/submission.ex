defmodule TypeHero.Submission do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          code: String.t(),
          is_successful: boolean(),
          inserted_at: DateTime.t(),
          user: TypeHero.User.t() | nil,
          challenge: TypeHero.Challenge.t() | nil,
          vote: [TypeHero.Vote.t()]
        }

  schema "Submission" do
    field(:code, :string)
    field(:is_successful, :boolean, source: :isSuccessful)
    field(:user_id, :binary_id, source: :userId)
    field(:challenge_id, :id, source: :challengeId)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    belongs_to(:challenge, TypeHero.Challenge, foreign_key: :challenge_id, define_field: false)
    has_many(:vote, TypeHero.Vote, foreign_key: :submission_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end