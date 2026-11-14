defmodule LangWatch.GatewayRealtimeSession do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          organization_id: String.t(),
          virtual_key_id: String.t(),
          model_provider_id: String.t(),
          vendor: String.t(),
          agent_id: String.t() | nil,
          model: String.t(),
          requested_model: String.t() | nil,
          vendor_conversation_id: String.t() | nil,
          status: atom(),
          minted_at: DateTime.t(),
          closed_at: DateTime.t() | nil,
          close_reason: String.t() | nil,
          trace_id: String.t() | nil,
          vendor_cost_raw: map() | nil,
          kind: String.t() | nil,
          metering: String.t() | nil,
          credential_expires_at: DateTime.t() | nil,
          transcription_model: String.t() | nil,
          end_user_id: String.t() | nil,
          last_report_at: DateTime.t() | nil,
          reported_cost_nano_usd: integer(),
          report_count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "GatewayRealtimeSession" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    field(:virtual_key_id, :string, source: :virtualKeyId)
    field(:model_provider_id, :string, source: :modelProviderId)
    field(:vendor, :string)
    field(:agent_id, :string, source: :agentId)
    field(:model, :string)
    field(:requested_model, :string, source: :requestedModel)
    field(:vendor_conversation_id, :string, source: :vendorConversationId)
    field(:status, Ecto.Enum, values: [:OPEN, :CLOSED, :FAILED, :EXPIRED], default: :OPEN)
    field(:minted_at, LangWatch.PrismaDateTime, autogenerate: true, source: :mintedAt)
    field(:closed_at, LangWatch.PrismaDateTime, source: :closedAt)
    field(:close_reason, :string, source: :closeReason)
    field(:trace_id, :string, source: :traceId)
    field(:vendor_cost_raw, :map, source: :vendorCostRaw)
    field(:kind, :string)
    field(:metering, :string)
    field(:credential_expires_at, LangWatch.PrismaDateTime, source: :credentialExpiresAt)
    field(:transcription_model, :string, source: :transcriptionModel)
    field(:end_user_id, :string, source: :endUserId)
    field(:last_report_at, LangWatch.PrismaDateTime, source: :lastReportAt)
    field(:reported_cost_nano_usd, :integer, default: 0, source: :reportedCostNanoUsd)
    field(:report_count, :integer, default: 0, source: :reportCount)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end