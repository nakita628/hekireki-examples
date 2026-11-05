defmodule OpenPanel.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t() | nil,
          provider: String.t(),
          provider_id: String.t() | nil,
          account_id: String.t() | nil,
          id_token: String.t() | nil,
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          access_token_expires_at: DateTime.t() | nil,
          refresh_token_expires_at: DateTime.t() | nil,
          scope: String.t() | nil,
          password: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: OpenPanel.User.t() | nil,
          reset_passwords: [OpenPanel.ResetPassword.t()]
        }

  schema "accounts" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:email, :string)
    field(:provider, :string)
    field(:provider_id, :string, source: :providerId)
    field(:account_id, :string, source: :accountId)
    field(:id_token, :string, source: :idToken)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:access_token_expires_at, OpenPanel.PrismaDateTime, source: :accessTokenExpiresAt)
    field(:refresh_token_expires_at, OpenPanel.PrismaDateTime, source: :refreshTokenExpiresAt)
    field(:scope, :string)
    field(:password, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, OpenPanel.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:reset_passwords, OpenPanel.ResetPassword, foreign_key: :account_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end