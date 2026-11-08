defmodule Plunk.Workflow do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          description: String.t() | nil,
          enabled: boolean(),
          trigger_type: atom(),
          trigger_config: map() | nil,
          allow_reentry: boolean(),
          throttle_window_seconds: integer() | nil,
          throttle_key: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Plunk.Project.t() | nil,
          steps: [Plunk.WorkflowStep.t()],
          executions: [Plunk.WorkflowExecution.t()],
          throttle_claims: [Plunk.ThrottleClaim.t()]
        }

  schema "workflows" do
    field(:name, :string)
    field(:description, :string)
    field(:enabled, :boolean, default: false)
    field(:trigger_type, Ecto.Enum, values: [:EVENT, :MANUAL, :SCHEDULE], source: :triggerType)
    field(:trigger_config, :map, source: :triggerConfig)
    field(:allow_reentry, :boolean, default: false, source: :allowReentry)
    field(:throttle_window_seconds, :integer, source: :throttleWindowSeconds)
    field(:throttle_key, :string, source: :throttleKey)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    has_many(:steps, Plunk.WorkflowStep, foreign_key: :workflow_id)
    has_many(:executions, Plunk.WorkflowExecution, foreign_key: :workflow_id)
    has_many(:throttle_claims, Plunk.ThrottleClaim, foreign_key: :workflow_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end