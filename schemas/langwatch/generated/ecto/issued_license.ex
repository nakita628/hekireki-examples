defmodule LangWatch.IssuedLicense do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          license_id: String.t(),
          token_hash: String.t(),
          organization_name: String.t(),
          email: String.t(),
          plan_type: String.t(),
          max_members: integer(),
          max_members_lite: integer() | nil,
          lite_seats_unlimited: boolean(),
          max_members_developer: integer() | nil,
          issued_at: DateTime.t(),
          expires_at: DateTime.t(),
          source: atom(),
          issued_by_id: String.t() | nil,
          revoked_at: DateTime.t() | nil,
          revoked_by_id: String.t() | nil,
          revoked_reason: String.t() | nil,
          superseded_at: DateTime.t() | nil,
          replaces_id: String.t() | nil,
          pending_delivery_license: String.t() | nil,
          services: [String.t()],
          seat_rate_cents: integer() | nil,
          seat_currency: atom() | nil,
          commit_usd_cents: integer(),
          overage_enabled: boolean(),
          overage_max_usd_cents: integer() | nil,
          instance_id: String.t() | nil,
          instance_bound_at: DateTime.t() | nil,
          last_sync_at: DateTime.t() | nil,
          last_sync_version: String.t() | nil,
          reported_members: integer() | nil,
          reported_members_lite: integer() | nil,
          virtual_key_id: String.t() | nil,
          seats_raised_from: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "IssuedLicense" do
    field(:id, :string, primary_key: true)
    field(:license_id, :string, source: :licenseId)
    field(:token_hash, :string, source: :tokenHash)
    field(:organization_name, :string, source: :organizationName)
    field(:email, :string)
    field(:plan_type, :string, source: :planType)
    field(:max_members, :integer, source: :maxMembers)
    field(:max_members_lite, :integer, source: :maxMembersLite)
    field(:lite_seats_unlimited, :boolean, default: false, source: :liteSeatsUnlimited)
    field(:max_members_developer, :integer, source: :maxMembersDeveloper)
    field(:issued_at, LangWatch.PrismaDateTime, source: :issuedAt)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:source, Ecto.Enum, values: [:BACKOFFICE, :PURCHASE, :SCRIPT, :LEGACY_IMPORT])
    field(:issued_by_id, :string, source: :issuedById)
    field(:revoked_at, LangWatch.PrismaDateTime, source: :revokedAt)
    field(:revoked_by_id, :string, source: :revokedById)
    field(:revoked_reason, :string, source: :revokedReason)
    field(:superseded_at, LangWatch.PrismaDateTime, source: :supersededAt)
    field(:replaces_id, :string, source: :replacesId)
    field(:pending_delivery_license, :string, source: :pendingDeliveryLicense)
    field(:services, {:array, :string})
    field(:seat_rate_cents, :integer, source: :seatRateCents)
    field(:seat_currency, Ecto.Enum, values: [:USD, :EUR], source: :seatCurrency)
    field(:commit_usd_cents, :integer, default: 0, source: :commitUsdCents)
    field(:overage_enabled, :boolean, default: false, source: :overageEnabled)
    field(:overage_max_usd_cents, :integer, source: :overageMaxUsdCents)
    field(:instance_id, :string, source: :instanceId)
    field(:instance_bound_at, LangWatch.PrismaDateTime, source: :instanceBoundAt)
    field(:last_sync_at, LangWatch.PrismaDateTime, source: :lastSyncAt)
    field(:last_sync_version, :string, source: :lastSyncVersion)
    field(:reported_members, :integer, source: :reportedMembers)
    field(:reported_members_lite, :integer, source: :reportedMembersLite)
    field(:virtual_key_id, :string, source: :virtualKeyId)
    field(:seats_raised_from, :integer, source: :seatsRaisedFrom)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end