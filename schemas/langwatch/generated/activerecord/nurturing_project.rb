class NurturingProject < ApplicationRecord
  self.table_name = "NurturingProject"
  self.primary_key = "projectId"

  validates :organizationId, presence: true
end
