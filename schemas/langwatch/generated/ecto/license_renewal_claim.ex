defmodule LangWatch.LicenseRenewalClaim do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          stripe_invoice_id: String.t(),
          stripe_subscription_id: String.t(),
          claim_token: String.t(),
          claimed_at: DateTime.t(),
          issued_license: map() | nil,
          delivered_at: DateTime.t() | nil,
          license_id: String.t() | nil
        }

  schema "LicenseRenewalClaim" do
    field(:stripe_invoice_id, :string, primary_key: true, source: :stripeInvoiceId)
    field(:stripe_subscription_id, :string, source: :stripeSubscriptionId)
    field(:claim_token, :string, source: :claimToken)
    field(:claimed_at, LangWatch.PrismaDateTime, source: :claimedAt)
    field(:issued_license, :map, source: :issuedLicense)
    field(:delivered_at, LangWatch.PrismaDateTime, source: :deliveredAt)
    field(:license_id, :string, source: :licenseId)
  end
end