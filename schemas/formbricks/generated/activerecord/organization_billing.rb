class OrganizationBilling < ApplicationRecord
  self.table_name = "OrganizationBilling"
  self.primary_key = "organization_id"

  validates :stripe_customer_id, uniqueness: { allow_nil: true }
  validates :limits, exclusion: { in: [ nil ] }

  belongs_to :organization, inverse_of: :billing
end
