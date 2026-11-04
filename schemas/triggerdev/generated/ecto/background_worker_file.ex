defmodule Trigger.BackgroundWorkerFile do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          file_path: String.t(),
          content_hash: String.t(),
          contents: binary(),
          inserted_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          tasks: [Trigger.BackgroundWorkerTask.t()],
          background_workers: [Trigger.BackgroundWorker.t()]
        }

  schema "BackgroundWorkerFile" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:file_path, :string, source: :filePath)
    field(:content_hash, :string, source: :contentHash)
    field(:contents, :binary)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:tasks, Trigger.BackgroundWorkerTask, foreign_key: :file_id, references: :id)
    many_to_many(:background_workers, Trigger.BackgroundWorker, join_through: "_BackgroundWorkerToBackgroundWorkerFile", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end