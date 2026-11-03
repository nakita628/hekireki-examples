class MastraKnowledgeNodes < ApplicationRecord
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :type, presence: true, uniqueness: { scope: [ :scopeKey, :canonicalName ] }
  validates :name, presence: true
  validates :canonicalName, presence: true
  validates :scope, exclusion: { in: [ nil ] }
  validates :scopeKey, presence: true
  validates :version, presence: true
end
