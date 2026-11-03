defmodule Papermark.DocumentRedaction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          page_number: integer(),
          x: float(),
          y: float(),
          width: float(),
          height: float(),
          text_preview: String.t() | nil,
          category: String.t() | nil,
          confidence: String.t() | nil,
          reason: String.t() | nil,
          source: String.t(),
          status: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          job: Papermark.DocumentRedactionJob.t() | nil
        }

  schema "DocumentRedaction" do
    field(:id, :string, primary_key: true)
    field(:page_number, :integer, source: :pageNumber)
    field(:x, :float)
    field(:y, :float)
    field(:width, :float)
    field(:height, :float)
    field(:text_preview, :string, source: :textPreview)
    field(:category, :string)
    field(:confidence, :string)
    field(:reason, :string)
    field(:source, :string, default: "AI")
    field(:status, :string, default: "PENDING")
    field(:job_id, :string, source: :jobId)
    belongs_to(:job, Papermark.DocumentRedactionJob, foreign_key: :job_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end