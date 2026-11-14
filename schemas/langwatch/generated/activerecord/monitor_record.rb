class MonitorRecord < ApplicationRecord
  self.table_name = "Monitor"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :enabled, default: true
  attribute :sample, default: 1
  attribute :level, default: "trace"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :executionMode, { on_message: "ON_MESSAGE", as_guardrail: "AS_GUARDRAIL", manually: "MANUALLY" },
       default: :on_message, validate: true

  validates :experimentId, uniqueness: { allow_nil: true }
  validates :checkType, presence: true
  validates :name, presence: true
  validates :slug, presence: true, uniqueness: { scope: :projectId }
  validates :preconditions, exclusion: { in: [ nil ] }
  validates :parameters, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :checks
  belongs_to :experiment, foreign_key: "experimentId", optional: true, inverse_of: :monitor
  belongs_to :evaluator, foreign_key: "evaluatorId", optional: true, inverse_of: :monitors
end
