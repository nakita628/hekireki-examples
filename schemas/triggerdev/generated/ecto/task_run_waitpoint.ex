defmodule Trigger.TaskRunWaitpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          span_id_to_complete: String.t() | nil,
          batch_index: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          task_run: Trigger.TaskRun.t() | nil,
          waitpoint: Trigger.Waitpoint.t() | nil,
          project: Trigger.Project.t() | nil,
          batch: Trigger.BatchTaskRun.t() | nil
        }

  schema "TaskRunWaitpoint" do
    field(:id, :string, primary_key: true)
    field(:span_id_to_complete, :string, source: :spanIdToComplete)
    field(:batch_index, :integer, source: :batchIndex)
    field(:task_run_id, :string, source: :taskRunId)
    field(:waitpoint_id, :string, source: :waitpointId)
    field(:project_id, :string, source: :projectId)
    field(:batch_id, :string, source: :batchId)
    belongs_to(:task_run, Trigger.TaskRun, foreign_key: :task_run_id, define_field: false, type: :string)
    belongs_to(:waitpoint, Trigger.Waitpoint, foreign_key: :waitpoint_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:batch, Trigger.BatchTaskRun, foreign_key: :batch_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end