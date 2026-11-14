class GatewayGuardrail < ApplicationRecord
  self.table_name = "GatewayGuardrail"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :direction, { pre: "PRE", post: "POST", stream_chunk: "STREAM_CHUNK" }, validate: true
  enum :failureMode, { fail_open: "FAIL_OPEN", fail_closed: "FAIL_CLOSED" }, default: :fail_closed, validate: true

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :gateway_guardrails
  belongs_to :evaluator, foreign_key: "evaluatorId", inverse_of: :gateway_guardrails
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :gateway_guardrails_created
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :gateway_guardrails_updated
end
