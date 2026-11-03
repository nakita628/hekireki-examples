defmodule Formbricks.OauthClientResource do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          metadata: map() | nil,
          created_at: DateTime.t() | nil,
          client: Formbricks.OauthClient.t() | nil,
          resource: Formbricks.OauthResource.t() | nil
        }

  schema "oauthClientResource" do
    field(:id, :string, primary_key: true)
    field(:metadata, :map)
    field(:created_at, Formbricks.PrismaDateTime, source: :createdAt)
    field(:client_id, :string, source: :clientId)
    field(:resource_id, :string, source: :resourceId)
    belongs_to(:client, Formbricks.OauthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:resource, Formbricks.OauthResource, foreign_key: :resource_id, define_field: false, type: :string, references: :identifier)
  end
end