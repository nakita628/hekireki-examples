defmodule Trigger.TaskRunTag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          friendly_id: String.t(),
          inserted_at: DateTime.t(),
          project: Trigger.Project.t() | nil
        }

  schema "TaskRunTag" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:friendly_id, :string, source: :friendlyId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end