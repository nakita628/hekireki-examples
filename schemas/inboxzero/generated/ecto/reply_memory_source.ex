defmodule InboxZero.ReplyMemorySource do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          reply_memory_id: String.t(),
          draft_send_log_id: String.t(),
          learned_writing_style_analyzed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          reply_memory: InboxZero.ReplyMemory.t() | nil,
          draft_send_log: InboxZero.DraftSendLog.t() | nil
        }

  schema "ReplyMemorySource" do
    field(:learned_writing_style_analyzed_at, InboxZero.PrismaDateTime, source: :learnedWritingStyleAnalyzedAt)
    field(:reply_memory_id, :string, primary_key: true, source: :replyMemoryId)
    field(:draft_send_log_id, :string, primary_key: true, source: :draftSendLogId)
    belongs_to(:reply_memory, InboxZero.ReplyMemory, foreign_key: :reply_memory_id, define_field: false, type: :string)
    belongs_to(:draft_send_log, InboxZero.DraftSendLog, foreign_key: :draft_send_log_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end