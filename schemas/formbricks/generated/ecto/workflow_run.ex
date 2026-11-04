defmodule Formbricks.WorkflowRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          workflow_id: String.t(),
          status: atom(),
          trigger_type: String.t(),
          survey_id: String.t() | nil,
          is_dry_run: boolean(),
          idempotency_key: String.t() | nil,
          attempt: integer(),
          next_attempt_at: DateTime.t() | nil,
          last_error_at: DateTime.t() | nil,
          trigger_payload: map(),
          data: map(),
          error: String.t() | nil,
          dispatched_at: DateTime.t() | nil,
          started_at: DateTime.t() | nil,
          finished_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          workflow_version: Formbricks.WorkflowVersion.t() | nil,
          response: Formbricks.Response.t() | nil,
          logs: [Formbricks.WorkflowRunLog.t()]
        }

  schema "WorkflowRun" do
    field(:id, :string, primary_key: true)
    field(:workflow_id, :string, source: :workflowId)
    field(:status, Ecto.Enum, values: [:queued, :running, :completed, :failed, :canceled], default: :queued)
    field(:trigger_type, :string, source: :triggerType)
    field(:survey_id, :string, source: :surveyId)
    field(:is_dry_run, :boolean, default: false, source: :isDryRun)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:attempt, :integer, default: 0)
    field(:next_attempt_at, Formbricks.PrismaDateTime, source: :nextAttemptAt)
    field(:last_error_at, Formbricks.PrismaDateTime, source: :lastErrorAt)
    field(:trigger_payload, :map, source: :triggerPayload)
    field(:data, :map, default: %{})
    field(:error, :string)
    field(:dispatched_at, Formbricks.PrismaDateTime, source: :dispatchedAt)
    field(:started_at, Formbricks.PrismaDateTime, source: :startedAt)
    field(:finished_at, Formbricks.PrismaDateTime, source: :finishedAt)
    field(:workspace_id, :string, source: :workspaceId)
    field(:workflow_version_id, :string, source: :workflowVersionId)
    field(:response_id, :string, source: :responseId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:workflow_version, Formbricks.WorkflowVersion, foreign_key: :workflow_version_id, define_field: false, type: :string)
    belongs_to(:response, Formbricks.Response, foreign_key: :response_id, define_field: false, type: :string)
    has_many(:logs, Formbricks.WorkflowRunLog, foreign_key: :run_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end