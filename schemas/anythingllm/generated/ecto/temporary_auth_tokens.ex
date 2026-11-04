defmodule AnythingLLM.TemporaryAuthTokens do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          token: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil
        }

  schema "temporary_auth_tokens" do
    field(:token, :string)
    field(:expires_at, AnythingLLM.PrismaDateTime, source: :expiresAt)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end