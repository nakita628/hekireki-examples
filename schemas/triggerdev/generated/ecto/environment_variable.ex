defmodule Trigger.EnvironmentVariable do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          key: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          values: [Trigger.EnvironmentVariableValue.t()]
        }

  schema "EnvironmentVariable" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:key, :string)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:values, Trigger.EnvironmentVariableValue, foreign_key: :variable_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end