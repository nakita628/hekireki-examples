defmodule LangWatch.AccountCredential do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          password: String.t() | nil,
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          id_token: String.t() | nil,
          access_token_expires_at: DateTime.t() | nil,
          refresh_token_expires_at: DateTime.t() | nil,
          scope: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil
        }

  schema "AccountCredential" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:password, :string)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:id_token, :string, source: :idToken)
    field(:access_token_expires_at, LangWatch.PrismaDateTime, source: :accessTokenExpiresAt)
    field(:refresh_token_expires_at, LangWatch.PrismaDateTime, source: :refreshTokenExpiresAt)
    field(:scope, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end