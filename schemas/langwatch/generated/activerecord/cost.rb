class Cost < ApplicationRecord
  self.table_name = "Cost"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :costType, { trace_check: "TRACE_CHECK", guardrail: "GUARDRAIL", clustering: "CLUSTERING", batch_evaluation: "BATCH_EVALUATION" },
       validate: true
  enum :referenceType, { check: "CHECK", trace: "TRACE", project: "PROJECT", batch: "BATCH" }, validate: true

  validates :referenceId, presence: true
  validates :amount, presence: true
  validates :currency, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :costs
end
