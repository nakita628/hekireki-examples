defmodule InboxZero.McpConnection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          is_active: boolean(),
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          api_key: String.t() | nil,
          expires_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          integration: InboxZero.McpIntegration.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil,
          tools: [InboxZero.McpTool.t()]
        }

  schema "McpConnection" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:api_key, :string, source: :apiKey)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:integration_id, :string, source: :integrationId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:integration, InboxZero.McpIntegration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:tools, InboxZero.McpTool, foreign_key: :connection_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end