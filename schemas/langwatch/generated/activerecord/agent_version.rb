class AgentVersion < ApplicationRecord
  self.table_name = "AgentVersion"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt

  validates :projectId, presence: true
  validates :agentId, presence: true, uniqueness: { scope: :version }
  validates :version, presence: true
  validates :name, presence: true
  validates :type, presence: true
  validates :config, exclusion: { in: [ nil ] }
end
