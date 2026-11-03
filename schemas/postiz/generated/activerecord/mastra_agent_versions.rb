class MastraAgentVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :agentId, presence: true
  validates :versionNumber, presence: true
  validates :name, presence: true
  validates :instructions, presence: true
  validates :model, exclusion: { in: [ nil ] }
end
