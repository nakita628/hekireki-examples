defmodule Jitsu.ReprocessingJobs do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          config: map(),
          status: String.t(),
          created_at: DateTime.t(),
          started_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          k8s_job_name: String.t() | nil,
          total_files: integer() | nil,
          total_workers: integer() | nil,
          error: String.t() | nil,
          reprocessing_workers: [Jitsu.ReprocessingWorkers.t()]
        }

  schema "reprocessing_jobs" do
    field(:id, :string, primary_key: true)
    field(:config, :map)
    field(:status, :string)
    field(:created_at, Jitsu.PrismaDateTime)
    field(:started_at, Jitsu.PrismaDateTime)
    field(:completed_at, Jitsu.PrismaDateTime)
    field(:k8s_job_name, :string)
    field(:total_files, :integer, default: 0)
    field(:total_workers, :integer, default: 0)
    field(:error, :string)
    has_many(:reprocessing_workers, Jitsu.ReprocessingWorkers, foreign_key: :job_id, references: :id)
  end
end