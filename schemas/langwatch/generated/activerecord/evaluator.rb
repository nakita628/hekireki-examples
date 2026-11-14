class Evaluator < ApplicationRecord
  self.table_name = "Evaluator"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, uniqueness: { scope: :projectId, allow_nil: true }
  validates :type, presence: true
  validates :config, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :evaluators
  belongs_to :workflow, foreign_key: "workflowId", optional: true, inverse_of: :evaluators
  belongs_to :copied_from, class_name: "Evaluator", foreign_key: "copiedFromEvaluatorId", optional: true,
                           inverse_of: :copied_evaluators
  has_many :copied_evaluators, class_name: "Evaluator", foreign_key: "copiedFromEvaluatorId", inverse_of: :copied_from,
                               dependent: :restrict_with_error
  has_many :monitors, class_name: "MonitorRecord", foreign_key: "evaluatorId", inverse_of: :evaluator,
                      dependent: :nullify
  has_many :gateway_guardrails, foreign_key: "evaluatorId", inverse_of: :evaluator, dependent: :restrict_with_error
end
