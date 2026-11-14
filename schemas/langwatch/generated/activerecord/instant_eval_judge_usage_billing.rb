class InstantEvalJudgeUsageBilling < ApplicationRecord
  self.table_name = "InstantEvalJudgeUsageBilling"
  self.primary_key = "organizationId"

  validates :usageBilled, inclusion: { in: [ true, false ] }
  validates :occurredAt, presence: true
  validates :fromCatchUp, inclusion: { in: [ true, false ] }
end
