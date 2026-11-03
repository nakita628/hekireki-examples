defmodule Langfuse.Action do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          config: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          automations: [Langfuse.Automation.t()],
          automation_executions: [Langfuse.AutomationExecution.t()]
        }

  schema "actions" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:WEBHOOK, :SLACK, :GITHUB_DISPATCH, :PAGERDUTY])
    field(:config, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:automations, Langfuse.Automation, foreign_key: :action_id)
    has_many(:automation_executions, Langfuse.AutomationExecution, foreign_key: :action_id)
    timestamps(inserted_at_source: :created_at)
  end
end