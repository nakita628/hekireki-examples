class WorkflowTransition < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :priority, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :from_step, class_name: "WorkflowStep", foreign_key: "fromStepId", inverse_of: :outgoing_transitions
  belongs_to :to_step, class_name: "WorkflowStep", foreign_key: "toStepId", inverse_of: :incoming_transitions
end
