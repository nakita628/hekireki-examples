defmodule Langfuse.AnnotationQueueAssignment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          user: Langfuse.User.t() | nil,
          queue: Langfuse.AnnotationQueue.t() | nil
        }

  schema "annotation_queue_assignments" do
    field(:id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    belongs_to(:queue, Langfuse.AnnotationQueue, foreign_key: :queue_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end