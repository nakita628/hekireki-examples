defmodule LangWatch.ActivationCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code_hash: String.t(),
          code_hint: String.t(),
          organization_id: String.t(),
          organization_name: String.t(),
          email: String.t(),
          plan_type: String.t(),
          max_members: integer(),
          max_members_lite: integer() | nil,
          lite_seats_unlimited: boolean(),
          max_members_developer: integer() | nil,
          license_term_days: integer(),
          services: [String.t()],
          expires_at: DateTime.t(),
          reusable: boolean(),
          redeemed_at: DateTime.t() | nil,
          redeemed_by_instance_id: String.t() | nil,
          issued_license_id: String.t() | nil,
          redemption_count: integer(),
          revoked_at: DateTime.t() | nil,
          revoked_by_id: String.t() | nil,
          created_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ActivationCode" do
    field(:id, :string, primary_key: true)
    field(:code_hash, :string, source: :codeHash)
    field(:code_hint, :string, source: :codeHint)
    field(:organization_id, :string, source: :organizationId)
    field(:organization_name, :string, source: :organizationName)
    field(:email, :string)
    field(:plan_type, :string, source: :planType)
    field(:max_members, :integer, source: :maxMembers)
    field(:max_members_lite, :integer, default: 0, source: :maxMembersLite)
    field(:lite_seats_unlimited, :boolean, default: false, source: :liteSeatsUnlimited)
    field(:max_members_developer, :integer, source: :maxMembersDeveloper)
    field(:license_term_days, :integer, source: :licenseTermDays)
    field(:services, {:array, :string})
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:reusable, :boolean, default: false)
    field(:redeemed_at, LangWatch.PrismaDateTime, source: :redeemedAt)
    field(:redeemed_by_instance_id, :string, source: :redeemedByInstanceId)
    field(:issued_license_id, :string, source: :issuedLicenseId)
    field(:redemption_count, :integer, default: 0, source: :redemptionCount)
    field(:revoked_at, LangWatch.PrismaDateTime, source: :revokedAt)
    field(:revoked_by_id, :string, source: :revokedById)
    field(:created_by_id, :string, source: :createdById)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end