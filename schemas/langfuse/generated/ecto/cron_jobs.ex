defmodule Langfuse.CronJobs do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          name: String.t(),
          last_run: DateTime.t() | nil,
          job_started_at: DateTime.t() | nil,
          state: String.t() | nil
        }

  schema "cron_jobs" do
    field(:name, :string, primary_key: true)
    field(:last_run, Langfuse.PrismaDateTime)
    field(:job_started_at, Langfuse.PrismaDateTime)
    field(:state, :string)
  end
end