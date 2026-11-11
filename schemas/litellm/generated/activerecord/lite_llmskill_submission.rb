class LiteLLMSkillSubmission < ApplicationRecord
  self.table_name = "LiteLLM_SkillSubmission"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :status, default: "pending_review"

  validates :plugin_name, presence: true
  validates :operation, presence: true
  validates :proposal_json, presence: true
  validates :submitted_by, presence: true
end
