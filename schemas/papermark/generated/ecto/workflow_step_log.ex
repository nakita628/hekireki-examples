defmodule Papermark.WorkflowStepLog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          conditions_matched: boolean(),
          condition_results: map() | nil,
          actions_executed: map() | nil,
          executed_at: DateTime.t(),
          duration: integer() | nil,
          error: String.t() | nil,
          execution: Papermark.WorkflowExecution.t() | nil,
          step: Papermark.WorkflowStep.t() | nil
        }

  schema "WorkflowStepLog" do
    field(:id, :string, primary_key: true)
    field(:conditions_matched, :boolean, source: :conditionsMatched)
    field(:condition_results, :map, source: :conditionResults)
    field(:actions_executed, :map, source: :actionsExecuted)
    field(:executed_at, Papermark.PrismaDateTime, autogenerate: true, source: :executedAt)
    field(:duration, :integer)
    field(:error, :string)
    field(:execution_id, :string, source: :executionId)
    field(:workflow_step_id, :string, source: :workflowStepId)
    belongs_to(:execution, Papermark.WorkflowExecution, foreign_key: :execution_id, define_field: false, type: :string)
    belongs_to(:step, Papermark.WorkflowStep, foreign_key: :workflow_step_id, define_field: false, type: :string)
  end
end