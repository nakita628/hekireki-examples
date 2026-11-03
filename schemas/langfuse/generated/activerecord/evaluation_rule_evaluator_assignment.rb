class EvaluationRuleEvaluatorAssignment < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :evaluation_rule_id, uniqueness: { scope: :evaluator_id }

  belongs_to :project, inverse_of: :evaluation_rule_evaluator_assignment
  belongs_to :evaluation_rule, inverse_of: :assignments
  belongs_to :evaluator, inverse_of: :assignments
end
