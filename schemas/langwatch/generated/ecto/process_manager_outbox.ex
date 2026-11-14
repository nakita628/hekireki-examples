defmodule LangWatch.ProcessManagerOutbox do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          process_name: String.t(),
          project_id: String.t(),
          process_key: String.t(),
          tenant_id: String.t(),
          user_id: String.t() | nil,
          message_key: String.t(),
          intent_type: String.t(),
          payload: map(),
          trace_carrier: map(),
          source_event_id: String.t() | nil,
          status: atom(),
          attempts: integer(),
          next_attempt_at: DateTime.t(),
          leased_until: DateTime.t() | nil,
          lease_token: String.t() | nil,
          dispatched_at: DateTime.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          attempt_log: [LangWatch.ProcessManagerOutboxAttempt.t()]
        }

  schema "ProcessManagerOutbox" do
    field(:id, :string, primary_key: true)
    field(:process_name, :string, source: :processName)
    field(:project_id, :string, source: :projectId)
    field(:process_key, :string, source: :processKey)
    field(:tenant_id, :string, source: :tenantId)
    field(:user_id, :string, source: :userId)
    field(:message_key, :string, source: :messageKey)
    field(:intent_type, :string, source: :intentType)
    field(:payload, :map)
    field(:trace_carrier, :map, source: :traceCarrier)
    field(:source_event_id, :string, source: :sourceEventId)
    field(:status, Ecto.Enum, values: [:pending, :dispatched, :dead, :discarded], default: :pending)
    field(:attempts, :integer, default: 0)
    field(:next_attempt_at, LangWatch.PrismaDateTime, source: :nextAttemptAt)
    field(:leased_until, LangWatch.PrismaDateTime, source: :leasedUntil)
    field(:lease_token, :string, source: :leaseToken)
    field(:dispatched_at, LangWatch.PrismaDateTime, source: :dispatchedAt)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
    has_many(:attempt_log, LangWatch.ProcessManagerOutboxAttempt, foreign_key: :outbox_id, references: :id)
  end
end