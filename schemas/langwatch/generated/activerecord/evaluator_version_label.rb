class EvaluatorVersionLabel < ApplicationRecord
  self.table_name = "EvaluatorVersionLabel"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, presence: true
  validates :evaluatorId, presence: true, uniqueness: { scope: :label }
  validates :label, presence: true
  validates :versionId, presence: true
end
