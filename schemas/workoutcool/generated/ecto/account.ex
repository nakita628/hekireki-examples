defmodule WorkoutCool.Account do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          account_id: String.t(),
          provider_id: String.t(),
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          id_token: String.t() | nil,
          access_token_expires_at: DateTime.t() | nil,
          refresh_token_expires_at: DateTime.t() | nil,
          scope: String.t() | nil,
          password: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: WorkoutCool.User.t() | nil
        }

  schema "account" do
    field(:id, :string, primary_key: true)
    field(:account_id, :string, source: :accountId)
    field(:provider_id, :string, source: :providerId)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:id_token, :string, source: :idToken)
    field(:access_token_expires_at, WorkoutCool.PrismaDateTime, source: :accessTokenExpiresAt)
    field(:refresh_token_expires_at, WorkoutCool.PrismaDateTime, source: :refreshTokenExpiresAt)
    field(:scope, :string)
    field(:password, :string)
    field(:created_at, WorkoutCool.PrismaDateTime, source: :createdAt)
    field(:updated_at, WorkoutCool.PrismaDateTime, source: :updatedAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end