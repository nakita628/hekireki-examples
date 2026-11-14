defmodule LangWatch.ScimSyncState do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          organization_id: String.t(),
          state: String.t(),
          last_pushed_at: DateTime.t() | nil,
          last_failure: map() | nil,
          dead_letters: map(),
          revoked_cause: String.t() | nil,
          occurred_at: DateTime.t(),
          last_event_id: String.t(),
          accepted_at: DateTime.t(),
          projection_version: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ScimSyncState" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:organization_id, :string, source: :organizationId)
    field(:state, :string)
    field(:last_pushed_at, LangWatch.PrismaDateTime, source: :lastPushedAt)
    field(:last_failure, :map, source: :lastFailure)
    field(:dead_letters, :map, source: :deadLetters)
    field(:revoked_cause, :string, source: :revokedCause)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:accepted_at, LangWatch.PrismaDateTime, source: :acceptedAt)
    field(:projection_version, :string, source: :projectionVersion)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
  end
end