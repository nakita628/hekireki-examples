defmodule LangWatch.SsoConnection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          type: String.t(),
          state: String.t(),
          claimed_domains: [String.t()],
          domain_claims: map(),
          approved_domains: [String.t()],
          verified_domains: [String.t()],
          lapsed_domains: [String.t()],
          domain_verifications: map(),
          pending_verification: map() | nil,
          idp_metadata: map(),
          arrival_policy: String.t(),
          allows_jit: boolean(),
          arrival_policy_decided_at: DateTime.t() | nil,
          source: String.t(),
          test_login_account_id: String.t() | nil,
          replaces_connection_id: String.t() | nil,
          migration_phase: atom() | nil,
          grace_started_at: DateTime.t() | nil,
          route_changed_at: DateTime.t() | nil,
          finalization_requested_at: DateTime.t() | nil,
          finalized_at: DateTime.t() | nil,
          rejection: map() | nil,
          created_by: String.t() | nil,
          tear_down_after: DateTime.t() | nil,
          occurred_at: DateTime.t(),
          last_event_id: String.t(),
          accepted_at: DateTime.t(),
          projection_version: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          reproof_cursor: LangWatch.SsoConnectionReproofCursor.t() | nil
        }

  schema "SsoConnection" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:type, :string)
    field(:state, :string)
    field(:claimed_domains, {:array, :string}, source: :claimedDomains)
    field(:domain_claims, :map, source: :domainClaims)
    field(:approved_domains, {:array, :string}, source: :approvedDomains)
    field(:verified_domains, {:array, :string}, source: :verifiedDomains)
    field(:lapsed_domains, {:array, :string}, source: :lapsedDomains)
    field(:domain_verifications, :map, source: :domainVerifications)
    field(:pending_verification, :map, source: :pendingVerification)
    field(:idp_metadata, :map, source: :idpMetadata)
    field(:arrival_policy, :string, default: "refuse", source: :arrivalPolicy)
    field(:allows_jit, :boolean, default: false, source: :allowsJit)
    field(:arrival_policy_decided_at, LangWatch.PrismaDateTime, source: :arrivalPolicyDecidedAt)
    field(:source, :string)
    field(:test_login_account_id, :string, source: :testLoginAccountId)
    field(:replaces_connection_id, :string, source: :replacesConnectionId)
    field(:migration_phase, Ecto.Enum, values: [:SETUP, :GRACE_LEGACY, :GRACE_DIRECT, :FINALIZING, :FINALIZED], source: :migrationPhase)
    field(:grace_started_at, LangWatch.PrismaDateTime, source: :graceStartedAt)
    field(:route_changed_at, LangWatch.PrismaDateTime, source: :routeChangedAt)
    field(:finalization_requested_at, LangWatch.PrismaDateTime, source: :finalizationRequestedAt)
    field(:finalized_at, LangWatch.PrismaDateTime, source: :finalizedAt)
    field(:rejection, :map)
    field(:created_by, :string, source: :createdBy)
    field(:tear_down_after, LangWatch.PrismaDateTime, source: :tearDownAfter)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:accepted_at, LangWatch.PrismaDateTime, source: :acceptedAt)
    field(:projection_version, :string, source: :projectionVersion)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
    has_one(:reproof_cursor, LangWatch.SsoConnectionReproofCursor, foreign_key: :connection_id, references: :id)
  end
end