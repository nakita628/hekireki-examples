defmodule LangWatch.AnnotationQueue do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          description: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          members: [LangWatch.AnnotationQueueMembers.t()],
          annotation_queue_scores: [LangWatch.AnnotationQueueScores.t()],
          annotation_queue_items: [LangWatch.AnnotationQueueItem.t()]
        }

  schema "AnnotationQueue" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:description, :string)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:members, LangWatch.AnnotationQueueMembers, foreign_key: :annotation_queue_id, references: :id)
    has_many(:annotation_queue_scores, LangWatch.AnnotationQueueScores, foreign_key: :annotation_queue_id, references: :id)
    has_many(:annotation_queue_items, LangWatch.AnnotationQueueItem, foreign_key: :annotation_queue_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end