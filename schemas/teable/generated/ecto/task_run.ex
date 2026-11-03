defmodule Teable.TaskRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t(),
          status: String.t(),
          snapshot: String.t(),
          depends_on_run_ids: [String.t()],
          spent: integer() | nil,
          log: String.t() | nil,
          error_msg: String.t() | nil,
          started_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil,
          task: Teable.Task.t() | nil
        }

  schema "task_run" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:status, :string)
    field(:snapshot, :string)
    field(:depends_on_run_ids, {:array, :string})
    field(:spent, :integer)
    field(:log, :string)
    field(:error_msg, :string)
    field(:started_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    belongs_to(:task, Teable.Task, foreign_key: :task_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end