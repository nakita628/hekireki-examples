class NurturingTracedProject < ApplicationRecord
  self.table_name = "NurturingTracedProject"
  self.primary_key = "projectId"

  validates :organizationId, presence: true
end
