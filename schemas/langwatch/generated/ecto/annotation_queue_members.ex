defmodule LangWatch.AnnotationQueueMembers do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: String.t(),
          annotation_queue_id: String.t(),
          user: LangWatch.User.t() | nil,
          annotation_queue: LangWatch.AnnotationQueue.t() | nil
        }

  schema "AnnotationQueueMembers" do
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:annotation_queue_id, :string, primary_key: true, source: :annotationQueueId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:annotation_queue, LangWatch.AnnotationQueue, foreign_key: :annotation_queue_id, define_field: false, type: :string)
  end
end