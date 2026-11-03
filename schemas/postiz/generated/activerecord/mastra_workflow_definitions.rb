class MastraWorkflowDefinitions < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :inputSchema, exclusion: { in: [ nil ] }
  validates :outputSchema, exclusion: { in: [ nil ] }
  validates :graph, exclusion: { in: [ nil ] }
  validates :status, presence: true
  validates :source, presence: true
end
