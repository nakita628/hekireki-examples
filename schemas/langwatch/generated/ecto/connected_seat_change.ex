defmodule LangWatch.ConnectedSeatChange do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          license_id: String.t(),
          account_id: String.t() | nil,
          organization_id: String.t() | nil,
          changed_at: DateTime.t(),
          added_seats: integer(),
          unit_amount_cents: integer(),
          amount_cents: integer(),
          currency: atom() | nil,
          stripe_invoice_id: String.t() | nil,
          state: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ConnectedSeatChange" do
    field(:id, :string, primary_key: true)
    field(:license_id, :string, source: :licenseId)
    field(:account_id, :string, source: :accountId)
    field(:organization_id, :string, source: :organizationId)
    field(:changed_at, LangWatch.PrismaDateTime, source: :changedAt)
    field(:added_seats, :integer, source: :addedSeats)
    field(:unit_amount_cents, :integer, source: :unitAmountCents)
    field(:amount_cents, :integer, source: :amountCents)
    field(:currency, Ecto.Enum, values: [:USD, :EUR])
    field(:stripe_invoice_id, :string, source: :stripeInvoiceId)
    field(:state, Ecto.Enum, values: [:intent, :invoiced, :nothing_to_invoice, :not_onboarded])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end