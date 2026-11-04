defmodule AnythingLLM.ScheduledJobRuns do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          status: String.t(),
          result: String.t() | nil,
          error: String.t() | nil,
          started_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          read_at: DateTime.t() | nil,
          job: AnythingLLM.ScheduledJobs.t() | nil
        }

  schema "scheduled_job_runs" do
    field(:status, :string, default: "queued")
    field(:result, :string)
    field(:error, :string)
    field(:started_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :startedAt)
    field(:completed_at, AnythingLLM.PrismaDateTime, source: :completedAt)
    field(:read_at, AnythingLLM.PrismaDateTime, source: :readAt)
    field(:job_id, :id, source: :jobId)
    belongs_to(:job, AnythingLLM.ScheduledJobs, foreign_key: :job_id, define_field: false)
  end
end