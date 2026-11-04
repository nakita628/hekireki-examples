defmodule Teable.Task do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          status: String.t(),
          snapshot: String.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          runs: [Teable.TaskRun.t()]
        }

  schema "task" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:status, :string)
    field(:snapshot, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    has_many(:runs, Teable.TaskRun, foreign_key: :task_id, references: :id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end