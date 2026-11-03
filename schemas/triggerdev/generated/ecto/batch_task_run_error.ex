defmodule Trigger.BatchTaskRunError do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          index: integer(),
          task_identifier: String.t(),
          payload: String.t() | nil,
          options: map() | nil,
          error: String.t(),
          error_code: String.t() | nil,
          inserted_at: DateTime.t(),
          batch_task_run: Trigger.BatchTaskRun.t() | nil
        }

  schema "BatchTaskRunError" do
    field(:id, :string, primary_key: true)
    field(:index, :integer)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:payload, :string)
    field(:options, :map)
    field(:error, :string)
    field(:error_code, :string, source: :errorCode)
    field(:batch_task_run_id, :string, source: :batchTaskRunId)
    belongs_to(:batch_task_run, Trigger.BatchTaskRun, foreign_key: :batch_task_run_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end