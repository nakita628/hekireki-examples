defmodule InboxZero.BulkInboxActionJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: atom(),
          search: map(),
          action: String.t(),
          label_id: String.t() | nil,
          label_name: String.t() | nil,
          instruction: String.t() | nil,
          page_token: String.t() | nil,
          rate_limited_until: DateTime.t() | nil,
          lease_expires_at: DateTime.t() | nil,
          chunk_token: String.t() | nil,
          recovery_count: integer(),
          processed_count: integer(),
          matched_count: integer(),
          acted_count: integer(),
          skipped_count: integer(),
          failed_count: integer(),
          limit_reached: boolean(),
          error: String.t() | nil,
          completed_at: DateTime.t() | nil,
          confirmation_key: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          chat: InboxZero.Chat.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil,
          handled_threads: [InboxZero.BulkInboxActionThread.t()]
        }

  schema "BulkInboxActionJob" do
    field(:id, :string, primary_key: true)
    field(:status, Ecto.Enum, values: [:RUNNING, :COMPLETED, :FAILED, :CANCELLED], default: :RUNNING)
    field(:search, :map)
    field(:action, :string)
    field(:label_id, :string, source: :labelId)
    field(:label_name, :string, source: :labelName)
    field(:instruction, :string)
    field(:page_token, :string, source: :pageToken)
    field(:rate_limited_until, InboxZero.PrismaDateTime, source: :rateLimitedUntil)
    field(:lease_expires_at, InboxZero.PrismaDateTime, source: :leaseExpiresAt)
    field(:chunk_token, :string, source: :chunkToken)
    field(:recovery_count, :integer, default: 0, source: :recoveryCount)
    field(:processed_count, :integer, default: 0, source: :processedCount)
    field(:matched_count, :integer, default: 0, source: :matchedCount)
    field(:acted_count, :integer, default: 0, source: :actedCount)
    field(:skipped_count, :integer, default: 0, source: :skippedCount)
    field(:failed_count, :integer, default: 0, source: :failedCount)
    field(:limit_reached, :boolean, default: false, source: :limitReached)
    field(:error, :string)
    field(:completed_at, InboxZero.PrismaDateTime, source: :completedAt)
    field(:confirmation_key, :string, source: :confirmationKey)
    field(:chat_id, :string, source: :chatId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:chat, InboxZero.Chat, foreign_key: :chat_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:handled_threads, InboxZero.BulkInboxActionThread, foreign_key: :job_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end