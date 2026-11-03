defmodule Langfuse.AnnotationQueue do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          score_config_ids: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          annotation_queue_item: [Langfuse.AnnotationQueueItem.t()],
          annotation_queue_assignment: [Langfuse.AnnotationQueueAssignment.t()]
        }

  schema "annotation_queues" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:score_config_ids, {:array, :string})
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:annotation_queue_item, Langfuse.AnnotationQueueItem, foreign_key: :queue_id)
    has_many(:annotation_queue_assignment, Langfuse.AnnotationQueueAssignment, foreign_key: :queue_id)
    timestamps(inserted_at_source: :created_at)
  end
end