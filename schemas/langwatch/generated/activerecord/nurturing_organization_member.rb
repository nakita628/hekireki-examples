class NurturingOrganizationMember < ApplicationRecord
  self.table_name = "NurturingOrganizationMember"
  self.primary_key = %w[organizationId userId]
end
