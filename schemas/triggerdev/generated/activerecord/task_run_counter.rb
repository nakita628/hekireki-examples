class TaskRunCounter < ApplicationRecord
  self.table_name = "TaskRunCounter"
  self.primary_key = "taskIdentifier"

  attribute :lastNumber, default: 0
end
