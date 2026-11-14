defmodule LangWatch.AnnotationQueueScores do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          annotation_score_id: String.t(),
          annotation_queue_id: String.t(),
          annotation_score: LangWatch.AnnotationScore.t() | nil,
          annotation_queue: LangWatch.AnnotationQueue.t() | nil
        }

  schema "AnnotationQueueScores" do
    field(:annotation_score_id, :string, primary_key: true, source: :annotationScoreId)
    field(:annotation_queue_id, :string, primary_key: true, source: :annotationQueueId)
    belongs_to(:annotation_score, LangWatch.AnnotationScore, foreign_key: :annotation_score_id, define_field: false, type: :string)
    belongs_to(:annotation_queue, LangWatch.AnnotationQueue, foreign_key: :annotation_queue_id, define_field: false, type: :string)
  end
end