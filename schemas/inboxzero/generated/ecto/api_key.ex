defmodule InboxZero.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          hashed_key: String.t(),
          is_active: boolean(),
          expires_at: DateTime.t() | nil,
          last_used_at: DateTime.t() | nil,
          scopes: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ApiKey" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:hashed_key, :string, source: :hashedKey)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:last_used_at, InboxZero.PrismaDateTime, source: :lastUsedAt)
    field(:scopes, {:array, Ecto.Enum}, values: [:STATS_READ, :RULES_READ, :RULES_WRITE, :SETTINGS_READ, :SETTINGS_WRITE, :ASSISTANT_CHAT, :SENDERS_UNSUBSCRIBE])
    field(:user_id, :string, source: :userId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end