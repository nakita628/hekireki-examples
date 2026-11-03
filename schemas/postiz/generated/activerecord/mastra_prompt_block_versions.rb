class MastraPromptBlockVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :blockId, presence: true, uniqueness: { scope: :versionNumber }
  validates :versionNumber, presence: true
  validates :name, presence: true
  validates :content, presence: true
end
