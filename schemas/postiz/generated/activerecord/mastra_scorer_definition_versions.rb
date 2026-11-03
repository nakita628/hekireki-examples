class MastraScorerDefinitionVersions < ApplicationRecord
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt

  validates :scorerDefinitionId, presence: true, uniqueness: { scope: :versionNumber }
  validates :versionNumber, presence: true
  validates :name, presence: true
  validates :type, presence: true
end
