defmodule Postiz.MastraMcpServerVersions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          mcp_server_id: String.t(),
          version_number: integer(),
          name: String.t(),
          version: String.t(),
          description: String.t() | nil,
          instructions: String.t() | nil,
          repository: map() | nil,
          release_date: String.t() | nil,
          is_latest: boolean() | nil,
          package_canonical: String.t() | nil,
          tools: map() | nil,
          agents: map() | nil,
          workflows: map() | nil,
          changed_fields: map() | nil,
          change_message: String.t() | nil,
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_mcp_server_versions" do
    field(:id, :string, primary_key: true)
    field(:mcp_server_id, :string, source: :mcpServerId)
    field(:version_number, :integer, source: :versionNumber)
    field(:name, :string)
    field(:version, :string)
    field(:description, :string)
    field(:instructions, :string)
    field(:repository, :map)
    field(:release_date, :string, source: :releaseDate)
    field(:is_latest, :boolean, source: :isLatest)
    field(:package_canonical, :string, source: :packageCanonical)
    field(:tools, :map)
    field(:agents, :map)
    field(:workflows, :map)
    field(:changed_fields, :map, source: :changedFields)
    field(:change_message, :string, source: :changeMessage)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end