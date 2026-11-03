class TaskRunNumberCounter < ApplicationRecord
  self.table_name = "TaskRunNumberCounter"

  attribute :id, default: -> { Cuid.generate }
  attribute :lastNumber, default: 0

  validates :taskIdentifier, presence: true, uniqueness: { scope: :environmentId }

  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId",
                           inverse_of: :task_run_number_counter
end
