defmodule Formbricks.WorkflowRunLog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          sequence: integer(),
          step_id: String.t(),
          step_type: String.t(),
          status: atom(),
          input: map(),
          output: map(),
          error: String.t() | nil,
          started_at: DateTime.t() | nil,
          finished_at: DateTime.t() | nil,
          run: Formbricks.WorkflowRun.t() | nil
        }

  schema "WorkflowRunLog" do
    field(:id, :string, primary_key: true)
    field(:sequence, :integer)
    field(:step_id, :string, source: :stepId)
    field(:step_type, :string, source: :stepType)
    field(:status, Ecto.Enum, values: [:pending, :running, :succeeded, :failed, :skipped], default: :pending)
    field(:input, :map, default: %{})
    field(:output, :map, default: %{})
    field(:error, :string)
    field(:started_at, Formbricks.PrismaDateTime, source: :startedAt)
    field(:finished_at, Formbricks.PrismaDateTime, source: :finishedAt)
    field(:run_id, :string, source: :runId)
    belongs_to(:run, Formbricks.WorkflowRun, foreign_key: :run_id, define_field: false, type: :string)
  end
end