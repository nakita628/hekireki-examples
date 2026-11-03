defmodule Langfuse.Trigger do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          event_source: String.t(),
          event_actions: [String.t()],
          filter: map() | nil,
          status: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          automation_executions: [Langfuse.AutomationExecution.t()],
          automations: [Langfuse.Automation.t()]
        }

  schema "triggers" do
    field(:id, :string, primary_key: true)
    field(:event_source, :string, source: :eventSource)
    field(:event_actions, {:array, :string}, source: :eventActions)
    field(:filter, :map)
    field(:status, Ecto.Enum, values: [:ACTIVE, :INACTIVE], default: :ACTIVE)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:automation_executions, Langfuse.AutomationExecution, foreign_key: :trigger_id)
    has_many(:automations, Langfuse.Automation, foreign_key: :trigger_id)
    timestamps(inserted_at_source: :created_at)
  end
end