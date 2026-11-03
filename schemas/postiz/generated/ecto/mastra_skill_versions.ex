defmodule Postiz.MastraSkillVersions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          skill_id: String.t(),
          version_number: integer(),
          name: String.t(),
          description: String.t(),
          instructions: String.t(),
          license: String.t() | nil,
          compatibility: map() | nil,
          source: map() | nil,
          references: map() | nil,
          scripts: map() | nil,
          assets: map() | nil,
          metadata: map() | nil,
          tree: map() | nil,
          changed_fields: map() | nil,
          change_message: String.t() | nil,
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          files: map() | nil
        }

  schema "mastra_skill_versions" do
    field(:id, :string, primary_key: true)
    field(:skill_id, :string, source: :skillId)
    field(:version_number, :integer, source: :versionNumber)
    field(:name, :string)
    field(:description, :string)
    field(:instructions, :string)
    field(:license, :string)
    field(:compatibility, :map)
    field(:source, :map)
    field(:references, :map)
    field(:scripts, :map)
    field(:assets, :map)
    field(:metadata, :map)
    field(:tree, :map)
    field(:changed_fields, :map, source: :changedFields)
    field(:change_message, :string, source: :changeMessage)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:files, :map)
  end
end