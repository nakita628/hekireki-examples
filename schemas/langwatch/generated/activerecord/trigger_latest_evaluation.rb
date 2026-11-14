class TriggerLatestEvaluation < ApplicationRecord
  self.table_name = "TriggerLatestEvaluation"
  self.primary_key = "triggerId"

  alias_attribute :updated_at, :updatedAt

  validates :projectId, presence: true
  validates :evaluatedAt, presence: true
  validates :verdict, presence: true

  belongs_to :trigger, foreign_key: "triggerId", inverse_of: :latest_evaluation
end
