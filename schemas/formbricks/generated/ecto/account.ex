defmodule Formbricks.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t() | nil,
          provider: String.t(),
          provider_account_id: String.t(),
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          expires_at: integer() | nil,
          ext_expires_in: integer() | nil,
          token_type: String.t() | nil,
          scope: String.t() | nil,
          id_token: String.t() | nil,
          session_state: String.t() | nil,
          password: String.t() | nil,
          access_token_expires_at: DateTime.t() | nil,
          refresh_token_expires_at: DateTime.t() | nil,
          issuer: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Formbricks.User.t() | nil
        }

  schema "Account" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:provider, :string)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:access_token, :string)
    field(:refresh_token, :string)
    field(:expires_at, :integer)
    field(:ext_expires_in, :integer)
    field(:token_type, :string)
    field(:scope, :string)
    field(:id_token, :string)
    field(:session_state, :string)
    field(:password, :string)
    field(:access_token_expires_at, Formbricks.PrismaDateTime, source: :accessTokenExpiresAt)
    field(:refresh_token_expires_at, Formbricks.PrismaDateTime, source: :refreshTokenExpiresAt)
    field(:issuer, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end