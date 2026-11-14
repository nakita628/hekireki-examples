class TriggerSent < ApplicationRecord
  self.table_name = "TriggerSent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :traceId, uniqueness: { scope: :triggerId, allow_nil: true }
  validates :openIncidentKey, uniqueness: { allow_nil: true }

  belongs_to :custom_graph, foreign_key: "customGraphId", optional: true, inverse_of: :trigger_sent
  belongs_to :project, foreign_key: "projectId", inverse_of: :trigger_sent
  belongs_to :trigger, foreign_key: "triggerId", inverse_of: :trigger_sent
end
