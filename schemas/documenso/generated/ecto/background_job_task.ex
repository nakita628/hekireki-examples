defmodule Documenso.BackgroundJobTask do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          status: atom(),
          result: map() | nil,
          retried: integer(),
          max_retries: integer(),
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          background_job: Documenso.BackgroundJob.t() | nil
        }

  schema "BackgroundJobTask" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:status, Ecto.Enum, values: [:PENDING, :COMPLETED, :FAILED], default: :PENDING)
    field(:result, :map)
    field(:retried, :integer, default: 0)
    field(:max_retries, :integer, default: 3, source: :maxRetries)
    field(:completed_at, Documenso.PrismaDateTime, source: :completedAt)
    field(:job_id, :string, source: :jobId)
    belongs_to(:background_job, Documenso.BackgroundJob, foreign_key: :job_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end