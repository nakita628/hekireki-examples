defmodule InboxZero.DraftSendLog do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          sent_message_id: String.t(),
          similarity_score: float(),
          body_similarity_score: float() | nil,
          body_similarity_status: String.t() | nil,
          similarity_metadata: map() | nil,
          sent_text: String.t() | nil,
          reply_memory_sent_text: String.t() | nil,
          reply_memory_attempt_count: integer(),
          reply_memory_processed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          executed_action: InboxZero.ExecutedAction.t() | nil,
          reply_memory_sources: [InboxZero.ReplyMemorySource.t()]
        }

  schema "DraftSendLog" do
    field(:id, :string, primary_key: true)
    field(:sent_message_id, :string, source: :sentMessageId)
    field(:similarity_score, :float, source: :similarityScore)
    field(:body_similarity_score, :float, source: :bodySimilarityScore)
    field(:body_similarity_status, :string, source: :bodySimilarityStatus)
    field(:similarity_metadata, :map, source: :similarityMetadata)
    field(:sent_text, :string, source: :sentText)
    field(:reply_memory_sent_text, :string, source: :replyMemorySentText)
    field(:reply_memory_attempt_count, :integer, default: 0, source: :replyMemoryAttemptCount)
    field(:reply_memory_processed_at, InboxZero.PrismaDateTime, source: :replyMemoryProcessedAt)
    field(:executed_action_id, :string, source: :executedActionId)
    belongs_to(:executed_action, InboxZero.ExecutedAction, foreign_key: :executed_action_id, define_field: false, type: :string)
    has_many(:reply_memory_sources, InboxZero.ReplyMemorySource, foreign_key: :draft_send_log_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end