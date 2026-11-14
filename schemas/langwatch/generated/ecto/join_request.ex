defmodule LangWatch.JoinRequest do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          organization_id: String.t(),
          domain: String.t(),
          state: String.t(),
          matched_via: String.t(),
          origin: String.t(),
          expires_at: DateTime.t() | nil,
          resolved_at: DateTime.t() | nil,
          resolved_by_type: String.t() | nil,
          resolved_by_id: String.t() | nil,
          withdrawal_cause: String.t() | nil,
          connection_id: String.t() | nil,
          occurred_at: DateTime.t(),
          last_event_id: String.t(),
          accepted_at: DateTime.t(),
          projection_version: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "JoinRequest" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:organization_id, :string, source: :organizationId)
    field(:domain, :string)
    field(:state, :string)
    field(:matched_via, :string, source: :matchedVia)
    field(:origin, :string, default: "web")
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:resolved_at, LangWatch.PrismaDateTime, source: :resolvedAt)
    field(:resolved_by_type, :string, source: :resolvedByType)
    field(:resolved_by_id, :string, source: :resolvedById)
    field(:withdrawal_cause, :string, source: :withdrawalCause)
    field(:connection_id, :string, source: :connectionId)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:accepted_at, LangWatch.PrismaDateTime, source: :acceptedAt)
    field(:projection_version, :string, source: :projectionVersion)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
  end
end