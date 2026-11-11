class LiteLLMWorkflowMessage < ApplicationRecord
  self.table_name = "LiteLLM_WorkflowMessage"
  self.primary_key = "message_id"
  self.implicit_order_column = "created_at"

  attribute :message_id, default: -> { SecureRandom.uuid }

  validates :role, presence: true
  validates :content, presence: true
  validates :sequence_number, presence: true, uniqueness: { scope: :run_id }

  belongs_to :run, class_name: "LiteLLMWorkflowRun", inverse_of: :messages
end
