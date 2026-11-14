class ScimDirectoryUser < ApplicationRecord
  self.table_name = "ScimDirectoryUser"
  self.primary_key = %w[connectionId userId]

  validates :organizationId, presence: true
end
