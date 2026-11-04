defmodule Papermark.WorkflowExecution do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          visitor_email: String.t() | nil,
          visitor_ip: String.t() | nil,
          status: atom(),
          started_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          result: map() | nil,
          metadata: map() | nil,
          workflow: Papermark.Workflow.t() | nil,
          step_logs: [Papermark.WorkflowStepLog.t()]
        }

  schema "WorkflowExecution" do
    field(:id, :string, primary_key: true)
    field(:visitor_email, :string, source: :visitorEmail)
    field(:visitor_ip, :string, source: :visitorIp)
    field(:status, Ecto.Enum, values: [:PENDING, :IN_PROGRESS, :COMPLETED, :FAILED, :BLOCKED])
    field(:started_at, Papermark.PrismaDateTime, autogenerate: true, source: :startedAt)
    field(:completed_at, Papermark.PrismaDateTime, source: :completedAt)
    field(:result, :map)
    field(:metadata, :map)
    field(:workflow_id, :string, source: :workflowId)
    belongs_to(:workflow, Papermark.Workflow, foreign_key: :workflow_id, define_field: false, type: :string)
    has_many(:step_logs, Papermark.WorkflowStepLog, foreign_key: :execution_id, references: :id)
  end
end