class AuthzOrganizationPolicy < ApplicationRecord
  self.table_name = "AuthzOrganizationPolicy"
  self.primary_key = "organizationId"

  attribute :braidsOrganizationAdminsOnly, default: false
  alias_attribute :updated_at, :updatedAt

  validates :updatedByUserId, presence: true
end
