class WorkspacePlatform < ApplicationRecord
  self.table_name = "WorkspacePlatform"

  attribute :enabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :slug, presence: true, uniqueness: true
  validates :slug, uniqueness: true
  validates :name, presence: true
  validates :description, presence: true
  validates :defaultServiceAccountKey, exclusion: { in: [ nil ] }

  has_many :delegation_credentials, foreign_key: "workspacePlatformId", inverse_of: :workspace_platform,
                                    dependent: :destroy
end
