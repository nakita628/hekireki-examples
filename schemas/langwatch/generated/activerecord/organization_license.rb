class OrganizationLicense < ApplicationRecord
  self.table_name = "OrganizationLicense"
  self.primary_key = "organizationId"

  alias_attribute :updated_at, :updatedAt
end
