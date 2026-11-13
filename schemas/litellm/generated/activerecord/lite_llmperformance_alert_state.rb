class LiteLLMPerformanceAlertState < ApplicationRecord
  self.table_name = "LiteLLM_PerformanceAlertState"

  attribute :enabled, default: true
  attribute :resume_after, default: ""
  attribute :wrapped, default: false
  attribute :checked, default: 0
  attribute :skipped, default: 0
  attribute :start_after, default: ""

  validates :owner, presence: true, length: { maximum: 32 }
  validates :next_run_at, presence: true
  validates :day, length: { maximum: 10 }
  validates :scope, length: { maximum: 16 }
  validates :resume_after, length: { maximum: 1024 }
  validates :start_scope, length: { maximum: 16 }
  validates :start_after, length: { maximum: 1024 }
end
