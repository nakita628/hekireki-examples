class JobExecution < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :status, { completed: "COMPLETED", error: "ERROR", pending: "PENDING", cancelled: "CANCELLED", delayed: "DELAYED" },
       validate: true

  validates :job_configuration_id, presence: true

  belongs_to :project, inverse_of: :job_execution
end
