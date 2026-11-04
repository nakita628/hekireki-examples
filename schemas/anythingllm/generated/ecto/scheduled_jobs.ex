defmodule AnythingLLM.ScheduledJobs do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          prompt: String.t(),
          tools: String.t() | nil,
          schedule: String.t(),
          enabled: boolean(),
          last_run_at: DateTime.t() | nil,
          next_run_at: DateTime.t() | nil,
          updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          runs: [AnythingLLM.ScheduledJobRuns.t()]
        }

  schema "scheduled_jobs" do
    field(:name, :string)
    field(:prompt, :string)
    field(:tools, :string)
    field(:schedule, :string)
    field(:enabled, :boolean, default: true)
    field(:last_run_at, AnythingLLM.PrismaDateTime, source: :lastRunAt)
    field(:next_run_at, AnythingLLM.PrismaDateTime, source: :nextRunAt)
    field(:updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :updatedAt)
    has_many(:runs, AnythingLLM.ScheduledJobRuns, foreign_key: :job_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end