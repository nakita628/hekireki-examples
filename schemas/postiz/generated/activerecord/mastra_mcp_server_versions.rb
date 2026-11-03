class MastraMcpServerVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :mcpServerId, presence: true, uniqueness: { scope: :versionNumber }
  validates :versionNumber, presence: true
  validates :name, presence: true
  validates :version, presence: true
end
