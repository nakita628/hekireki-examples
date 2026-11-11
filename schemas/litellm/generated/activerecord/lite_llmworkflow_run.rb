class LiteLLMWorkflowRun < ApplicationRecord
  self.table_name = "LiteLLM_WorkflowRun"
  self.primary_key = "run_id"
  self.implicit_order_column = "created_at"

  attribute :run_id, default: -> { SecureRandom.uuid }
  attribute :session_id, default: -> { SecureRandom.uuid }
  attribute :status, default: "pending"

  validates :session_id, uniqueness: true
  validates :workflow_type, presence: true

  has_many :events, class_name: "LiteLLMWorkflowEvent", foreign_key: "run_id", inverse_of: :run,
                    dependent: :restrict_with_error
  has_many :messages, class_name: "LiteLLMWorkflowMessage", foreign_key: "run_id", inverse_of: :run,
                      dependent: :restrict_with_error
end
