defmodule Jitsu.ConfigurationObjectLink do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          deleted: boolean() | nil,
          type: String.t() | nil,
          data: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil,
          from: Jitsu.ConfigurationObject.t() | nil,
          to: Jitsu.ConfigurationObject.t() | nil
        }

  schema "ConfigurationObjectLink" do
    field(:id, :string, primary_key: true)
    field(:deleted, :boolean, default: false)
    field(:type, :string, default: "push")
    field(:data, :map)
    field(:workspace_id, :string, source: :workspaceId)
    field(:from_id, :string, source: :fromId)
    field(:to_id, :string, source: :toId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:from, Jitsu.ConfigurationObject, foreign_key: :from_id, define_field: false, type: :string)
    belongs_to(:to, Jitsu.ConfigurationObject, foreign_key: :to_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end