defmodule OpenPanel.Import do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          job_id: String.t() | nil,
          status: atom(),
          status_message: String.t() | nil,
          error_message: String.t() | nil,
          config: map(),
          total_events: integer(),
          processed_events: integer(),
          current_step: String.t() | nil,
          current_batch: String.t() | nil,
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil
        }

  schema "imports" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:job_id, :string, source: :jobId)
    field(:status, Ecto.Enum, values: [:pending, :processing, :completed, :failed])
    field(:status_message, :string, source: :statusMessage)
    field(:error_message, :string, source: :errorMessage)
    field(:config, :map)
    field(:total_events, :integer, default: 0, source: :totalEvents)
    field(:processed_events, :integer, default: 0, source: :processedEvents)
    field(:current_step, :string, source: :currentStep)
    field(:current_batch, :string, source: :currentBatch)
    field(:completed_at, OpenPanel.PrismaDateTime, source: :completedAt)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end