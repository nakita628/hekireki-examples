defmodule Trigger.SupportAccessRequest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          reason: String.t(),
          status: atom(),
          approved_at: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          cancelled_by_id: String.t() | nil,
          cancelled_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil,
          requested_by: Trigger.User.t() | nil,
          approved_by: Trigger.User.t() | nil
        }

  schema "SupportAccessRequest" do
    field(:id, :string, primary_key: true)
    field(:reason, :string)
    field(:status, Ecto.Enum, values: [:PENDING, :APPROVED, :CANCELLED], default: :PENDING)
    field(:approved_at, Trigger.PrismaDateTime, source: :approvedAt)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
    field(:cancelled_by_id, :string, source: :cancelledById)
    field(:cancelled_at, Trigger.PrismaDateTime, source: :cancelledAt)
    field(:organization_id, :string, source: :organizationId)
    field(:requested_by_id, :string, source: :requestedById)
    field(:approved_by_id, :string, source: :approvedById)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:requested_by, Trigger.User, foreign_key: :requested_by_id, define_field: false, type: :string)
    belongs_to(:approved_by, Trigger.User, foreign_key: :approved_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end