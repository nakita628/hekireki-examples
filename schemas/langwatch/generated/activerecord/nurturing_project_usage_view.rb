class NurturingProjectUsageView < ApplicationRecord
  self.table_name = "NurturingProjectUsageView"
  self.primary_key = %w[projectId owner]

  attribute :appliedEventIds, default: -> { [] }
  alias_attribute :updated_at, :updatedAt

  validates :folded, exclusion: { in: [ nil ] }
  validates :projectionVersion, presence: true
end
