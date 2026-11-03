defmodule Postiz.MastraKnowledgeNodes do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          name: String.t(),
          canonical_name: String.t(),
          kind: String.t() | nil,
          content: String.t() | nil,
          description: String.t() | nil,
          scope: map(),
          scope_key: String.t(),
          version: integer(),
          merged_into: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_knowledge_nodes" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:name, :string)
    field(:canonical_name, :string, source: :canonicalName)
    field(:kind, :string)
    field(:content, :string)
    field(:description, :string)
    field(:scope, :map)
    field(:scope_key, :string, source: :scopeKey)
    field(:version, :integer)
    field(:merged_into, :string, source: :mergedInto)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end