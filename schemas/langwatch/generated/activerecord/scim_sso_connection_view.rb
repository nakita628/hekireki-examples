class ScimSsoConnectionView < ApplicationRecord
  self.table_name = "ScimSsoConnectionView"

  attribute :appliedEventIds, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :folded, exclusion: { in: [ nil ] }
  validates :projectionVersion, presence: true
end
