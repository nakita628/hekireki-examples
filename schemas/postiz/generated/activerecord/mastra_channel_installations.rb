class MastraChannelInstallations < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :platform, presence: true
  validates :agentId, presence: true
  validates :status, presence: true
  validates :webhookId, uniqueness: { allow_nil: true }
  validates :data, exclusion: { in: [ nil ] }
end
