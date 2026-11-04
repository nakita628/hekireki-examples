defmodule Langfuse.Automation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          trigger: Langfuse.Trigger.t() | nil,
          action: Langfuse.Action.t() | nil,
          project: Langfuse.Project.t() | nil,
          automation_execution: [Langfuse.AutomationExecution.t()]
        }

  schema "automations" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    belongs_to(:trigger, Langfuse.Trigger, foreign_key: :trigger_id, type: :string)
    belongs_to(:action, Langfuse.Action, foreign_key: :action_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:automation_execution, Langfuse.AutomationExecution, foreign_key: :automation_id, references: :id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end