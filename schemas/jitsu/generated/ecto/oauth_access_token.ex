defmodule Jitsu.OAuthAccessToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          last_used: DateTime.t() | nil,
          hash: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          refresh_token: Jitsu.UserApiToken.t() | nil
        }

  schema "OAuthAccessToken" do
    field(:id, :string, primary_key: true)
    field(:last_used, Jitsu.PrismaDateTime, source: :lastUsed)
    field(:hash, :string)
    field(:expires_at, Jitsu.PrismaDateTime, source: :expiresAt)
    field(:refresh_token_id, :string, source: :refreshTokenId)
    belongs_to(:refresh_token, Jitsu.UserApiToken, foreign_key: :refresh_token_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end