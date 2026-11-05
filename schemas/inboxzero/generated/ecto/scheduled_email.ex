defmodule InboxZero.ScheduledEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          client_mutation_id: String.t(),
          payload_hash: String.t(),
          payload: map(),
          thread_id: String.t() | nil,
          send_at: DateTime.t(),
          status: atom(),
          processing_started_at: DateTime.t() | nil,
          execution_queued_at: DateTime.t() | nil,
          sent_at: DateTime.t() | nil,
          error: String.t() | nil,
          remind_at: DateTime.t() | nil,
          reminder_status: atom(),
          reminder_started_at: DateTime.t() | nil,
          held_for_undo: boolean(),
          draft_message_ids: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ScheduledEmail" do
    field(:id, :string, primary_key: true)
    field(:client_mutation_id, :string, source: :clientMutationId)
    field(:payload_hash, :string, source: :payloadHash)
    field(:payload, :map)
    field(:thread_id, :string, source: :threadId)
    field(:send_at, InboxZero.PrismaDateTime, source: :sendAt)
    field(:status, Ecto.Enum, values: [:PENDING, :PROCESSING, :SENT, :BLOCKED_AUTH, :UNCERTAIN, :FAILED, :CANCELLED], default: :PENDING)
    field(:processing_started_at, InboxZero.PrismaDateTime, source: :processingStartedAt)
    field(:execution_queued_at, InboxZero.PrismaDateTime, source: :executionQueuedAt)
    field(:sent_at, InboxZero.PrismaDateTime, source: :sentAt)
    field(:error, :string)
    field(:remind_at, InboxZero.PrismaDateTime, source: :remindAt)
    field(:reminder_status, Ecto.Enum, values: [:NONE, :PENDING, :PROCESSING, :COMPLETED, :CANCELLED], default: :NONE, source: :reminderStatus)
    field(:reminder_started_at, InboxZero.PrismaDateTime, source: :reminderStartedAt)
    field(:held_for_undo, :boolean, default: false, source: :heldForUndo)
    field(:draft_message_ids, {:array, :string}, source: :draftMessageIds)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end