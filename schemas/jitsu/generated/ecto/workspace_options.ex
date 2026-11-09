defmodule Jitsu.WorkspaceOptions do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          namespace: String.t(),
          value: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil
        }

  schema "WorkspaceOptions" do
    field(:id, :string, primary_key: true)
    field(:namespace, :string)
    field(:value, :map)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end