defmodule LangWatch.ConnectedBillingAccount do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          stripe_customer_id: String.t(),
          usage_subscription_id: String.t() | nil,
          usage_subscription_item_id: String.t() | nil,
          term_starts_at: DateTime.t(),
          term_ends_at: DateTime.t(),
          commit_usd_cents: integer(),
          seat_currency: atom(),
          seat_rate_cents: integer(),
          seats: integer(),
          bank_transfer_type: String.t() | nil,
          bank_transfer_country: String.t() | nil,
          billing_email: String.t(),
          pending_renewal: map() | nil,
          created_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "ConnectedBillingAccount" do
    field(:id, :string, primary_key: true)
    field(:stripe_customer_id, :string, source: :stripeCustomerId)
    field(:usage_subscription_id, :string, source: :usageSubscriptionId)
    field(:usage_subscription_item_id, :string, source: :usageSubscriptionItemId)
    field(:term_starts_at, LangWatch.PrismaDateTime, source: :termStartsAt)
    field(:term_ends_at, LangWatch.PrismaDateTime, source: :termEndsAt)
    field(:commit_usd_cents, :integer, default: 0, source: :commitUsdCents)
    field(:seat_currency, Ecto.Enum, values: [:USD, :EUR], source: :seatCurrency)
    field(:seat_rate_cents, :integer, source: :seatRateCents)
    field(:seats, :integer)
    field(:bank_transfer_type, :string, source: :bankTransferType)
    field(:bank_transfer_country, :string, source: :bankTransferCountry)
    field(:billing_email, :string, source: :billingEmail)
    field(:pending_renewal, :map, source: :pendingRenewal)
    field(:created_by_id, :string, source: :createdById)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end