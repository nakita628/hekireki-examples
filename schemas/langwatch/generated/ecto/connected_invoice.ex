defmodule LangWatch.ConnectedInvoice do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          account_id: String.t(),
          stripe_invoice_id: String.t(),
          kind: atom(),
          currency: atom(),
          amount_cents: integer(),
          status: String.t(),
          paid_out_of_band_at: DateTime.t() | nil,
          term_starts_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ConnectedInvoice" do
    field(:id, :string, primary_key: true)
    field(:account_id, :string, source: :accountId)
    field(:stripe_invoice_id, :string, source: :stripeInvoiceId)
    field(:kind, Ecto.Enum, values: [:annual, :seat_change, :usage])
    field(:currency, Ecto.Enum, values: [:USD, :EUR])
    field(:amount_cents, :integer, source: :amountCents)
    field(:status, :string)
    field(:paid_out_of_band_at, LangWatch.PrismaDateTime, source: :paidOutOfBandAt)
    field(:term_starts_at, LangWatch.PrismaDateTime, source: :termStartsAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end