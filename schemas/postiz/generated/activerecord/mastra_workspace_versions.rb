class MastraWorkspaceVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :workspaceId, presence: true, uniqueness: { scope: :versionNumber }
  validates :versionNumber, presence: true
  validates :name, presence: true
end
