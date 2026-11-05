defmodule InboxZero.MessagingChannel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: atom(),
          is_connected: boolean(),
          team_id: String.t(),
          team_name: String.t() | nil,
          provider_user_id: String.t() | nil,
          bot_user_id: String.t() | nil,
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          expires_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          routes: [InboxZero.MessagingRoute.t()],
          executed_actions: [InboxZero.ExecutedAction.t()]
        }

  schema "MessagingChannel" do
    field(:id, :string, primary_key: true)
    field(:provider, Ecto.Enum, values: [:SLACK, :TEAMS, :TELEGRAM])
    field(:is_connected, :boolean, default: true, source: :isConnected)
    field(:team_id, :string, source: :teamId)
    field(:team_name, :string, source: :teamName)
    field(:provider_user_id, :string, source: :providerUserId)
    field(:bot_user_id, :string, source: :botUserId)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:routes, InboxZero.MessagingRoute, foreign_key: :messaging_channel_id, references: :id)
    has_many(:executed_actions, InboxZero.ExecutedAction, foreign_key: :messaging_channel_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end