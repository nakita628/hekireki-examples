defmodule LangWatch.AnnotationQueueItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          trace_id: String.t(),
          done_at: DateTime.t() | nil,
          marked_for_dataset_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          annotation_queue: LangWatch.AnnotationQueue.t() | nil,
          user: LangWatch.User.t() | nil,
          created_by_user: LangWatch.User.t() | nil,
          project: LangWatch.Project.t() | nil
        }

  schema "AnnotationQueueItem" do
    field(:id, :string, primary_key: true)
    field(:trace_id, :string, source: :traceId)
    field(:done_at, LangWatch.PrismaDateTime, source: :doneAt)
    field(:marked_for_dataset_at, LangWatch.PrismaDateTime, source: :markedForDatasetAt)
    field(:annotation_queue_id, :string, source: :annotationQueueId)
    field(:user_id, :string, source: :userId)
    field(:created_by_user_id, :string, source: :createdByUserId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:annotation_queue, LangWatch.AnnotationQueue, foreign_key: :annotation_queue_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:created_by_user, LangWatch.User, foreign_key: :created_by_user_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end