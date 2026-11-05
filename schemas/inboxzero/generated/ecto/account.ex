defmodule InboxZero.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          password: String.t() | nil,
          id: String.t(),
          provider: String.t(),
          type: String.t(),
          provider_account_id: String.t(),
          refresh_token: String.t() | nil,
          refresh_token_expires_at: DateTime.t() | nil,
          access_token: String.t() | nil,
          expires_at: DateTime.t() | nil,
          disconnected_at: DateTime.t() | nil,
          token_type: String.t() | nil,
          scope: String.t() | nil,
          id_token: String.t() | nil,
          session_state: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Account" do
    field(:password, :string)
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:type, :string, default: "oidc")
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:refresh_token, :string)
    field(:refresh_token_expires_at, InboxZero.PrismaDateTime, source: :refreshTokenExpiresAt)
    field(:access_token, :string)
    field(:expires_at, InboxZero.PrismaDateTime, autogenerate: true)
    field(:disconnected_at, InboxZero.PrismaDateTime, source: :disconnectedAt)
    field(:token_type, :string)
    field(:scope, :string)
    field(:id_token, :string)
    field(:session_state, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    has_one(:email_account, InboxZero.EmailAccount, foreign_key: :account_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end