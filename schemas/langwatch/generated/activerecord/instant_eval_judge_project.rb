class InstantEvalJudgeProject < ApplicationRecord
  self.table_name = "InstantEvalJudgeProject"
  self.primary_key = "projectId"

  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true
end
