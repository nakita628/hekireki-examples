defmodule Plunk.WorkflowExecution do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          status: atom(),
          exit_reason: String.t() | nil,
          context: map() | nil,
          started_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workflow: Plunk.Workflow.t() | nil,
          contact: Plunk.Contact.t() | nil,
          current_step: Plunk.WorkflowStep.t() | nil,
          step_executions: [Plunk.WorkflowStepExecution.t()],
          emails: [Plunk.Email.t()]
        }

  schema "workflow_executions" do
    field(:status, Ecto.Enum, values: [:RUNNING, :WAITING, :COMPLETED, :EXITED, :FAILED, :CANCELLED], default: :RUNNING)
    field(:exit_reason, :string, source: :exitReason)
    field(:context, :map)
    field(:started_at, Plunk.PrismaDateTime, autogenerate: true, source: :startedAt)
    field(:completed_at, Plunk.PrismaDateTime, source: :completedAt)
    field(:workflow_id, :binary_id, source: :workflowId)
    field(:contact_id, :binary_id, source: :contactId)
    field(:current_step_id, :binary_id, source: :currentStepId)
    belongs_to(:workflow, Plunk.Workflow, foreign_key: :workflow_id, define_field: false)
    belongs_to(:contact, Plunk.Contact, foreign_key: :contact_id, define_field: false)
    belongs_to(:current_step, Plunk.WorkflowStep, foreign_key: :current_step_id, define_field: false)
    has_many(:step_executions, Plunk.WorkflowStepExecution, foreign_key: :execution_id)
    has_many(:emails, Plunk.Email, foreign_key: :workflow_execution_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end