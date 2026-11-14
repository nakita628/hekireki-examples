defmodule LangWatch.OrganizationUser do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          organization_id: String.t(),
          role: atom(),
          department_id: String.t() | nil,
          disabled_at: DateTime.t() | nil,
          pending_sso_grant_id: String.t() | nil,
          membership_stamp: String.t(),
          full_seat_since: DateTime.t() | nil,
          seat_pending_since: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil,
          organization: LangWatch.Organization.t() | nil
        }

  schema "OrganizationUser" do
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER, :EXTERNAL, :DEVELOPER])
    field(:department_id, :string, source: :departmentId)
    field(:disabled_at, LangWatch.PrismaDateTime, source: :disabledAt)
    field(:pending_sso_grant_id, :string, source: :pendingSsoGrantId)
    field(:membership_stamp, :string, read_after_writes: true, source: :membershipStamp)
    field(:full_seat_since, LangWatch.PrismaDateTime, source: :fullSeatSince)
    field(:seat_pending_since, LangWatch.PrismaDateTime, source: :seatPendingSince)
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end