defmodule LangWatch.IdempotencyReceipt do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope_id: String.t(),
          key: String.t(),
          claim_id: String.t(),
          request_fingerprint: String.t(),
          response_status: integer() | nil,
          response_body: String.t() | nil,
          heartbeat_at: DateTime.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "IdempotencyReceipt" do
    field(:id, :string, primary_key: true)
    field(:scope_id, :string, source: :scopeId)
    field(:key, :string)
    field(:claim_id, :string, source: :claimId)
    field(:request_fingerprint, :string, source: :requestFingerprint)
    field(:response_status, :integer, source: :responseStatus)
    field(:response_body, :string, source: :responseBody)
    field(:heartbeat_at, LangWatch.PrismaDateTime, source: :heartbeatAt)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end