class BulkInboxActionThread < ApplicationRecord
  self.table_name = "BulkInboxActionThread"
  self.primary_key = %w[jobId threadId]

  belongs_to :job, class_name: "BulkInboxActionJob", foreign_key: "jobId", inverse_of: :handled_threads
end
