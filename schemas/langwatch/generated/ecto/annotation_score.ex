defmodule LangWatch.AnnotationScore do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          deleted_at: DateTime.t() | nil,
          description: String.t() | nil,
          active: boolean(),
          data_type: atom() | nil,
          options: map() | nil,
          default_value: map() | nil,
          global: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          annotation_queue_scores: [LangWatch.AnnotationQueueScores.t()]
        }

  schema "AnnotationScore" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:deleted_at, LangWatch.PrismaDateTime, source: :deletedAt)
    field(:description, :string)
    field(:active, :boolean, default: true)
    field(:data_type, Ecto.Enum, values: [:CATEGORICAL, :BOOLEAN, :LIKERT, :OPTION, :CHECKBOX], source: :dataType)
    field(:options, :map)
    field(:default_value, :map, source: :defaultValue)
    field(:global, :boolean, default: false)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:annotation_queue_scores, LangWatch.AnnotationQueueScores, foreign_key: :annotation_score_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end