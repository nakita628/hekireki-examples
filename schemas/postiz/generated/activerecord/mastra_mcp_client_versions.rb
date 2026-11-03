class MastraMcpClientVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :mcpClientId, presence: true, uniqueness: { scope: :versionNumber }
  validates :versionNumber, presence: true
  validates :name, presence: true
  validates :servers, exclusion: { in: [ nil ] }
end
