defmodule Calcom.SeatChangeLog do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          change_type: atom(),
          seat_count: integer(),
          user_id: integer() | nil,
          triggered_by: integer() | nil,
          change_date: DateTime.t(),
          month_key: String.t(),
          operation_id: String.t() | nil,
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          proration: Calcom.MonthlyProration.t() | nil,
          team_billing: Calcom.TeamBilling.t() | nil,
          organization_billing: Calcom.OrganizationBilling.t() | nil
        }

  schema "SeatChangeLog" do
    field(:change_type, Ecto.Enum, values: [:ADDITION, :REMOVAL], source: :changeType)
    field(:seat_count, :integer, source: :seatCount)
    field(:user_id, :integer, source: :userId)
    field(:triggered_by, :integer, source: :triggeredBy)
    field(:change_date, Calcom.PrismaDateTime, autogenerate: true, source: :changeDate)
    field(:month_key, :string, source: :monthKey)
    field(:operation_id, :string, source: :operationId)
    field(:metadata, :map)
    field(:team_id, :id, source: :teamId)
    field(:processed_in_proration_id, :binary_id, source: :processedInProrationId)
    field(:team_billing_id, :binary_id, source: :teamBillingId)
    field(:organization_billing_id, :binary_id, source: :organizationBillingId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    belongs_to(:proration, Calcom.MonthlyProration, foreign_key: :processed_in_proration_id, define_field: false)
    belongs_to(:team_billing, Calcom.TeamBilling, foreign_key: :team_billing_id, define_field: false)
    belongs_to(:organization_billing, Calcom.OrganizationBilling, foreign_key: :organization_billing_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end