class NurturingProjectTraceDay < ApplicationRecord
  self.table_name = "NurturingProjectTraceDay"
  self.primary_key = %w[projectId day]

  attribute :traceCount, default: 0
  attribute :errorCount, default: 0
end
