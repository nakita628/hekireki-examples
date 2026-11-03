defmodule Langfuse.AnnotationQueueItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          object_id: String.t(),
          object_type: atom(),
          status: atom(),
          locked_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          queue: Langfuse.AnnotationQueue.t() | nil,
          locked_by_user: Langfuse.User.t() | nil,
          annotator_user: Langfuse.User.t() | nil,
          project: Langfuse.Project.t() | nil
        }

  schema "annotation_queue_items" do
    field(:id, :string, primary_key: true)
    field(:object_id, :string)
    field(:object_type, Ecto.Enum, values: [:TRACE, :OBSERVATION, :SESSION])
    field(:status, Ecto.Enum, values: [:PENDING, :COMPLETED], default: :PENDING)
    field(:locked_at, Langfuse.PrismaDateTime)
    field(:completed_at, Langfuse.PrismaDateTime)
    belongs_to(:queue, Langfuse.AnnotationQueue, foreign_key: :queue_id, type: :string)
    belongs_to(:locked_by_user, Langfuse.User, foreign_key: :locked_by_user_id, type: :string)
    belongs_to(:annotator_user, Langfuse.User, foreign_key: :annotator_user_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end