defmodule Papermark.WorkflowStep do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          step_order: integer(),
          step_type: atom(),
          conditions: map(),
          actions: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workflow: Papermark.Workflow.t() | nil,
          execution_logs: [Papermark.WorkflowStepLog.t()]
        }

  schema "WorkflowStep" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:step_order, :integer, source: :stepOrder)
    field(:step_type, Ecto.Enum, values: [:ROUTER], default: :ROUTER, source: :stepType)
    field(:conditions, :map)
    field(:actions, :map)
    field(:workflow_id, :string, source: :workflowId)
    belongs_to(:workflow, Papermark.Workflow, foreign_key: :workflow_id, define_field: false, type: :string)
    has_many(:execution_logs, Papermark.WorkflowStepLog, foreign_key: :workflow_step_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end