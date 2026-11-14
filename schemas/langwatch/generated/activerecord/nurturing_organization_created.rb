class NurturingOrganizationCreated < ApplicationRecord
  self.table_name = "NurturingOrganizationCreated"
  self.primary_key = "organizationId"

  alias_attribute :created_at, :createdAt
end
