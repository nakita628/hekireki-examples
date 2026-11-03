defmodule Documenso.BackgroundJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: atom(),
          payload: map() | nil,
          retried: integer(),
          max_retries: integer(),
          job_id: String.t(),
          name: String.t(),
          version: String.t(),
          submitted_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          last_retried_at: DateTime.t() | nil,
          updated_at: DateTime.t(),
          tasks: [Documenso.BackgroundJobTask.t()]
        }

  schema "BackgroundJob" do
    field(:id, :string, primary_key: true)
    field(:status, Ecto.Enum, values: [:PENDING, :PROCESSING, :COMPLETED, :FAILED], default: :PENDING)
    field(:payload, :map)
    field(:retried, :integer, default: 0)
    field(:max_retries, :integer, default: 3, source: :maxRetries)
    field(:job_id, :string, source: :jobId)
    field(:name, :string)
    field(:version, :string)
    field(:submitted_at, Documenso.PrismaDateTime, autogenerate: true, source: :submittedAt)
    field(:completed_at, Documenso.PrismaDateTime, source: :completedAt)
    field(:last_retried_at, Documenso.PrismaDateTime, source: :lastRetriedAt)
    has_many(:tasks, Documenso.BackgroundJobTask, foreign_key: :job_id)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end