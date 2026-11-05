defmodule InboxZero.OauthClientResource do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          metadata: map() | nil,
          created_at: DateTime.t() | nil,
          client: InboxZero.OauthClient.t() | nil,
          oauth_resource: InboxZero.OauthResource.t() | nil
        }

  schema "OauthClientResource" do
    field(:id, :string, primary_key: true)
    field(:metadata, :map)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:client_id, :string, source: :clientId)
    field(:resource_id, :string, source: :resourceId)
    belongs_to(:client, InboxZero.OauthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:oauth_resource, InboxZero.OauthResource, foreign_key: :resource_id, define_field: false, type: :string, references: :identifier)
  end
end