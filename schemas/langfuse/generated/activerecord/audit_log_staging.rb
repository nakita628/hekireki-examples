class AuditLogStaging < ApplicationRecord
  self.table_name = "audit_log_staging"

  validates :payload, exclusion: { in: [ nil ] }
end
