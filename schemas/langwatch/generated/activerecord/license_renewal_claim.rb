class LicenseRenewalClaim < ApplicationRecord
  self.table_name = "LicenseRenewalClaim"
  self.primary_key = "stripeInvoiceId"

  validates :stripeSubscriptionId, presence: true
  validates :claimToken, presence: true
  validates :claimedAt, presence: true
end
