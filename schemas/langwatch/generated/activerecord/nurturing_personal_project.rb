class NurturingPersonalProject < ApplicationRecord
  self.table_name = "NurturingPersonalProject"
  self.primary_key = "projectId"

  validates :organizationId, presence: true
  validates :ownerUserId, presence: true
end
