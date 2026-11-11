class LiteLLMWorkflowEvent < ApplicationRecord
  self.table_name = "LiteLLM_WorkflowEvent"
  self.primary_key = "event_id"
  self.implicit_order_column = "created_at"

  attribute :event_id, default: -> { SecureRandom.uuid }

  validates :event_type, presence: true
  validates :step_name, presence: true
  validates :sequence_number, presence: true, uniqueness: { scope: :run_id }

  belongs_to :run, class_name: "LiteLLMWorkflowRun", inverse_of: :events
end
