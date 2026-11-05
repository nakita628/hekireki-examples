defmodule InboxZero.McpIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          registered_server_url: String.t() | nil,
          registered_authorization_url: String.t() | nil,
          registered_token_url: String.t() | nil,
          oauth_client_id: String.t() | nil,
          oauth_client_secret: String.t() | nil,
          display_name: String.t() | nil,
          server_url: String.t() | nil,
          auth_type: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          connections: [InboxZero.McpConnection.t()]
        }

  schema "McpIntegration" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:registered_server_url, :string, source: :registeredServerUrl)
    field(:registered_authorization_url, :string, source: :registeredAuthorizationUrl)
    field(:registered_token_url, :string, source: :registeredTokenUrl)
    field(:oauth_client_id, :string, source: :oauthClientId)
    field(:oauth_client_secret, :string, source: :oauthClientSecret)
    field(:display_name, :string, source: :displayName)
    field(:server_url, :string, source: :serverUrl)
    field(:auth_type, Ecto.Enum, values: [:OAUTH, :API_TOKEN, :NONE], source: :authType)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:connections, InboxZero.McpConnection, foreign_key: :integration_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end