class OrganizationSeatLease < ApplicationRecord
  self.table_name = "OrganizationSeatLease"
  self.primary_key = "organizationId"

  alias_attribute :created_at, :createdAt

  validates :holder, presence: true
  validates :expiresAt, presence: true
end
