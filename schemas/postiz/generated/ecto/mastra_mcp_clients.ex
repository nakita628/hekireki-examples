defmodule Postiz.MastraMcpClients do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          status: String.t(),
          active_version_id: String.t() | nil,
          author_id: String.t() | nil,
          metadata: map() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_mcp_clients" do
    field(:id, :string, primary_key: true)
    field(:status, :string)
    field(:active_version_id, :string, source: :activeVersionId)
    field(:author_id, :string, source: :authorId)
    field(:metadata, :map)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end