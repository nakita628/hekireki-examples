defmodule LangWatch.OrganizationInvite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          invite_code: String.t(),
          expiration: DateTime.t() | nil,
          status: atom(),
          team_ids: String.t(),
          team_assignments: map() | nil,
          role: atom(),
          subscription_id: String.t() | nil,
          accepted_by_user_id: String.t() | nil,
          accepted_via_identifier_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          requested_by_user: LangWatch.User.t() | nil
        }

  schema "OrganizationInvite" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:invite_code, :string, source: :inviteCode)
    field(:expiration, LangWatch.PrismaDateTime)
    field(:status, Ecto.Enum, values: [:PENDING, :ACCEPTED, :WAITING_APPROVAL, :PAYMENT_PENDING, :REVOKED], default: :PENDING)
    field(:team_ids, :string, source: :teamIds)
    field(:team_assignments, :map, source: :teamAssignments)
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER, :EXTERNAL, :DEVELOPER])
    field(:subscription_id, :string, source: :subscriptionId)
    field(:accepted_by_user_id, :string, source: :acceptedByUserId)
    field(:accepted_via_identifier_id, :string, source: :acceptedViaIdentifierId)
    field(:organization_id, :string, source: :organizationId)
    field(:requested_by, :string, source: :requestedBy)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:requested_by_user, LangWatch.User, foreign_key: :requested_by, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end