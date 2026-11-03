class EvaluationRule < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :time_scope, default: -> { %w[NEW] }

  enum :status, { active: "ACTIVE", inactive: "INACTIVE" }, default: :active, validate: true

  validates :name, presence: true
  validates :target_object, presence: true
  validates :filter, exclusion: { in: [ nil ] }
  validates :sampling, presence: true
  validates :delay, presence: true

  belongs_to :project, inverse_of: :evaluation_rule
  belongs_to :created_by_user, class_name: "User", optional: true, inverse_of: :evaluation_rules_created
  has_many :assignments, class_name: "EvaluationRuleEvaluatorAssignment", inverse_of: :evaluation_rule,
                         dependent: :destroy
end
