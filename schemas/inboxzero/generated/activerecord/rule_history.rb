class RuleHistory < ApplicationRecord
  self.table_name = "RuleHistory"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :version, presence: true, uniqueness: { scope: :ruleId }
  validates :triggerType, presence: true
  validates :name, presence: true
  validates :enabled, inclusion: { in: [ true, false ] }
  validates :automate, inclusion: { in: [ true, false ] }
  validates :runOnThreads, inclusion: { in: [ true, false ] }
  validates :conditionalOperator, presence: true
  validates :actions, exclusion: { in: [ nil ] }

  belongs_to :rule, foreign_key: "ruleId", inverse_of: :history
end
