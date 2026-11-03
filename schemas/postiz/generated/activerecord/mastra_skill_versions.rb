class MastraSkillVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :skillId, presence: true, uniqueness: { scope: :versionNumber }
  validates :versionNumber, presence: true
  validates :name, presence: true
  validates :description, presence: true
  validates :instructions, presence: true
end
