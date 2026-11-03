defmodule Postiz.MastraToolProviderConnections do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          author_id: String.t(),
          provider_id: String.t(),
          connection_id: String.t(),
          toolkit: String.t(),
          label: String.t() | nil,
          scope: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_tool_provider_connections" do
    field(:author_id, :string, primary_key: true, source: :authorId)
    field(:provider_id, :string, primary_key: true, source: :providerId)
    field(:connection_id, :string, primary_key: true, source: :connectionId)
    field(:toolkit, :string)
    field(:label, :string)
    field(:scope, :string)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end