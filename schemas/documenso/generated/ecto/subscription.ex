defmodule Documenso.Subscription do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          status: atom(),
          plan_id: String.t(),
          price_id: String.t(),
          period_end: DateTime.t() | nil,
          cancel_at_period_end: boolean(),
          customer_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organisation: Documenso.Organisation.t() | nil
        }

  schema "Subscription" do
    field(:status, Ecto.Enum, values: [:ACTIVE, :PAST_DUE, :INACTIVE], default: :INACTIVE)
    field(:plan_id, :string, source: :planId)
    field(:price_id, :string, source: :priceId)
    field(:period_end, Documenso.PrismaDateTime, source: :periodEnd)
    field(:cancel_at_period_end, :boolean, default: false, source: :cancelAtPeriodEnd)
    field(:customer_id, :string, source: :customerId)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end