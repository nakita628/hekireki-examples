defmodule Langfuse.AutomationExecution do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          source_id: String.t(),
          status: atom(),
          input: map(),
          output: map() | nil,
          started_at: DateTime.t() | nil,
          finished_at: DateTime.t() | nil,
          error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          automation: Langfuse.Automation.t() | nil,
          trigger: Langfuse.Trigger.t() | nil,
          action: Langfuse.Action.t() | nil,
          project: Langfuse.Project.t() | nil
        }

  schema "automation_executions" do
    field(:id, :string, primary_key: true)
    field(:source_id, :string)
    field(:status, Ecto.Enum, values: [:COMPLETED, :ERROR, :PENDING, :CANCELLED], default: :PENDING)
    field(:input, :map)
    field(:output, :map)
    field(:started_at, Langfuse.PrismaDateTime)
    field(:finished_at, Langfuse.PrismaDateTime)
    field(:error, :string)
    belongs_to(:automation, Langfuse.Automation, foreign_key: :automation_id, type: :string)
    belongs_to(:trigger, Langfuse.Trigger, foreign_key: :trigger_id, type: :string)
    belongs_to(:action, Langfuse.Action, foreign_key: :action_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end