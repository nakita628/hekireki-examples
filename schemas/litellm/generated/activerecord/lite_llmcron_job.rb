class LiteLLMCronJob < ApplicationRecord
  self.table_name = "LiteLLM_CronJob"
  self.primary_key = "cronjob_id"

  attribute :cronjob_id, default: -> { Cuid.generate }

  enum :status, { active: "ACTIVE", inactive: "INACTIVE" }, default: :inactive, validate: true

  validates :pod_id, presence: true
  validates :ttl, presence: true
end
