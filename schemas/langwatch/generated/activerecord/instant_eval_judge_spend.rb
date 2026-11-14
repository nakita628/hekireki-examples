class InstantEvalJudgeSpend < ApplicationRecord
  self.table_name = "InstantEvalJudgeSpend"
  self.primary_key = %w[organizationId requestId]

  validates :spendNanoUsd, presence: true
  validates :occurredAt, presence: true
end
